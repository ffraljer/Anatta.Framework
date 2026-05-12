using System.Runtime.InteropServices;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.ShaderCompiler;
using Vortice.SPIRV;
using Vortice.SpirvCross;
using Compiler = Vortice.D3DCompiler.Compiler;

namespace Anatta.Framework.Graphics.Helpers;

internal static class AnattaShaderCompiler {
    internal static byte[] CompileGlslToSpirv(string glsl, Vortice.ShaderCompiler.ShaderKind kind) {
        using var compiler = new Vortice.ShaderCompiler.Compiler();
        var options = new Vortice.ShaderCompiler.CompilerOptions();
        options.ShaderStage = kind;
        var result = compiler.Compile(glsl, "shader", options);

        if (result == null)
            throw new Exception("glsl to spirv returned null");

        if (result.Status != CompilationStatus.Success)
            throw new Exception($"glsl to spirv failed: {result.ErrorMessage}");

        return result.Bytecode.ToArray();
    }

    internal static unsafe string SpirvToHlsl(byte[] spirv) {
        spvc_context ctx = default;
        spvc_parsed_ir ir = default;
        spvc_compiler compiler = default;
        spvc_compiler_options options = default;

        SpirvCrossApi.spvc_context_create(&ctx);
        SpirvCrossApi.spvc_context_parse_spirv(ctx, spirv, out ir);
        SpirvCrossApi.spvc_context_create_compiler(ctx, Backend.HLSL, ir, CaptureMode.TakeOwnership, &compiler);
        SpirvCrossApi.spvc_compiler_create_compiler_options(compiler, &options);
        SpirvCrossApi.spvc_compiler_options_set_uint(options, CompilerOption.HLSLShaderModel, 50);
        SpirvCrossApi.spvc_compiler_install_compiler_options(compiler, options);

        byte* result;
        SpirvCrossApi.spvc_compiler_compile(compiler, &result);

        string hlsl = Marshal.PtrToStringUTF8((IntPtr)result)!;
        SpirvCrossApi.spvc_context_destroy(ctx);
        return hlsl;
    }

    internal static byte[] CompileHlsl(string hlsl, string profile) {
        Compiler.Compile(hlsl, null, null, "main", null, profile,
            0, 0, out var blob, out var errors);
        if (blob == null)
            throw new Exception($"ur shader fuckin' broke ({profile}): {errors?.AsString()}");
        return blob.AsBytes();
    }

    public static unsafe (
    (string Name, int Offset, int Size)[] Uniforms,
    int CbSize,
    InputElementDescription[] Layout
) ReflectSpirv(byte[] vsSpv, byte[] fsSpv) {
        spvc_context context = default;
        SpirvCrossApi.spvc_context_create(&context);

        uint wordCount = (uint)vsSpv.Length / 4;
        spvc_parsed_ir ir = default;

        fixed (byte* ptr = vsSpv)
            SpirvCrossApi.spvc_context_parse_spirv(context, (uint*)ptr, wordCount, &ir);

        spvc_compiler compiler = default;
        SpirvCrossApi.spvc_context_create_compiler(
            context,
            Backend.None,
            ir,
            CaptureMode.TakeOwnership,
            &compiler);

        spvc_resources resources = default;
        SpirvCrossApi.spvc_compiler_create_shader_resources(compiler, &resources);

        spvc_reflected_resource* inputList = default;
        nuint inputCount = 0;
        SpirvCrossApi.spvc_resources_get_resource_list_for_type(
            resources,
            ResourceType.StageInput,
            (spvc_reflected_resource**)&inputList,
            &inputCount);

        var inputs = new List<(uint Location, uint BaseTypeId, uint Id)>();
        for (nuint i = 0; i < inputCount; i++) {
            var res = inputList[i];
            uint loc = SpirvCrossApi.spvc_compiler_get_decoration(
                compiler, res.id, SpvDecoration.Location);
            inputs.Add((loc, res.base_type_id, res.id));
        }
        inputs.Sort((a, b) => a.Location.CompareTo(b.Location));

        var layout = new List<InputElementDescription>();
        uint byteOffset = 0;
        foreach (var (loc, baseTypeId, id) in inputs) {
            spvc_type type = SpirvCrossApi.spvc_compiler_get_type_handle(compiler, baseTypeId);
            uint vecSize = SpirvCrossApi.spvc_type_get_vector_size(type);
            var (fmt, size) = vecSize switch {
                1 => (Format.R32_Float, 4u),
                2 => (Format.R32G32_Float, 8u),
                3 => (Format.R32G32B32_Float, 12u),
                4 => (Format.R32G32B32A32_Float, 16u),
                _ => throw new NotSupportedException($"Unhandled vecsize {vecSize}")
            };
            layout.Add(new InputElementDescription("TEXCOORD", loc, fmt, byteOffset, 0));
            byteOffset += size;
        }

        spvc_parsed_ir ir2 = default;
        fixed (byte* ptr = vsSpv)
            SpirvCrossApi.spvc_context_parse_spirv(context, (uint*)ptr, wordCount, &ir2);

        spvc_compiler compiler2 = default;
        SpirvCrossApi.spvc_context_create_compiler(
            context,
            Backend.None,
            ir2,
            CaptureMode.TakeOwnership,
            &compiler2);

        spvc_resources resources2 = default;
        SpirvCrossApi.spvc_compiler_create_shader_resources(compiler2, &resources2);

        spvc_reflected_resource* uboList = default;
        nuint uboCount = 0;
        SpirvCrossApi.spvc_resources_get_resource_list_for_type(
            resources2,
            ResourceType.UniformBuffer,
            (spvc_reflected_resource**)&uboList,
            &uboCount);

        var uniforms = new List<(string, int, int)>();
        int cbSize = 0;

        for (nuint i = 0; i < uboCount; i++) {
            var ubo = uboList[i];
            spvc_type structType = SpirvCrossApi.spvc_compiler_get_type_handle(compiler2, ubo.base_type_id);
            uint memberCount = SpirvCrossApi.spvc_type_get_num_member_types(structType);

            for (uint m = 0; m < memberCount; m++) {
                byte* namePtrRaw = SpirvCrossApi.spvc_compiler_get_member_name(compiler2, ubo.base_type_id, m);
                string name = Marshal.PtrToStringUTF8((IntPtr)namePtrRaw) ?? $"_m{m}";

                uint offset = 0;
                SpirvCrossApi.spvc_compiler_type_struct_member_offset(compiler2, structType, m, &offset);

                uint memberTypeId = SpirvCrossApi.spvc_type_get_member_type(structType, m);
                spvc_type memberType = SpirvCrossApi.spvc_compiler_get_type_handle(compiler2, memberTypeId);

                uint cols = SpirvCrossApi.spvc_type_get_columns(memberType);
                uint rows = SpirvCrossApi.spvc_type_get_vector_size(memberType);
                uint arrDims = SpirvCrossApi.spvc_type_get_num_array_dimensions(memberType);
                uint arrLen = arrDims > 0
                    ? SpirvCrossApi.spvc_type_get_array_dimension(memberType, 0)
                    : 1u;

                int size = (int)(cols * rows * 4 * arrLen);

                uniforms.Add((name, (int)offset, size));
                cbSize = Math.Max(cbSize, (int)offset + size);
            }
        }

        SpirvCrossApi.spvc_context_destroy(context);

        return (uniforms.ToArray(), cbSize, layout.ToArray());
    }

    private static Format ResolveFormat(spvc_type type) {
        var baseType = SpirvCrossApi.spvc_type_get_basetype(type);
        uint vecSize = SpirvCrossApi.spvc_type_get_vector_size(type);

        return (baseType, vecSize) switch {
            (Basetype.Fp32, 1) => Format.R32_Float,
            (Basetype.Fp32, 2) => Format.R32G32_Float,
            (Basetype.Fp32, 3) => Format.R32G32B32_Float,
            (Basetype.Fp32, 4) => Format.R32G32B32A32_Float,
            (Basetype.Int32, 1) => Format.R32_SInt,
            (Basetype.Uint32, 1) => Format.R32_UInt,
            _ => Format.R32G32B32A32_Float
        };
    }
}