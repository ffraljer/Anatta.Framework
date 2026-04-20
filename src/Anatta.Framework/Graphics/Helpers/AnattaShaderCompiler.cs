using System.Runtime.InteropServices;
using Vortice.ShaderCompiler;
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
}