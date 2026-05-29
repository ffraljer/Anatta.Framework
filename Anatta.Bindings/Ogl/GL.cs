// auto-generated, do not edit.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Anatta.Framework.Graphics.OpenGL
{
    /// <summary>Raw OpenGL 4.6 bindings. Load function pointers via <see cref="GLLoader"/>.</summary>
    public static unsafe class GL
    {
        // --------------------------------------------------------
        // Buffers
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindBuffer(uint target, uint buffer);
        private static d_BindBuffer _BindBuffer;

        public static void BindBuffer(uint target, uint buffer)
            => _BindBuffer(target, buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindBufferBase(uint target, uint index, uint buffer);
        private static d_BindBufferBase _BindBufferBase;

        public static void BindBufferBase(uint target, uint index, uint buffer)
            => _BindBufferBase(target, index, buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindBufferRange(uint target, uint index, uint buffer, nint offset, nint size);
        private static d_BindBufferRange _BindBufferRange;

        public static void BindBufferRange(uint target, uint index, uint buffer, nint offset, nint size)
            => _BindBufferRange(target, index, buffer, offset, size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BufferData(uint target, nint size, nint data, uint usage);
        private static d_BufferData _BufferData;

        public static void BufferData(uint target, nint size, nint data, uint usage)
            => _BufferData(target, size, data, usage);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BufferStorage(uint target, nint size, nint data, uint flags);
        private static d_BufferStorage _BufferStorage;

        public static void BufferStorage(uint target, nint size, nint data, uint flags)
            => _BufferStorage(target, size, data, flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BufferSubData(uint target, nint offset, nint size, nint data);
        private static d_BufferSubData _BufferSubData;

        public static void BufferSubData(uint target, nint offset, nint size, nint data)
            => _BufferSubData(target, offset, size, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_CopyBufferSubData(uint readTarget, uint writeTarget, nint readOffset, nint writeOffset, nint size);
        private static d_CopyBufferSubData _CopyBufferSubData;

        public static void CopyBufferSubData(uint readTarget, uint writeTarget, nint readOffset, nint writeOffset, nint size)
            => _CopyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteBuffers(int n, uint* buffers);
        private static d_DeleteBuffers _DeleteBuffers;

        public static void DeleteBuffers(int n, uint* buffers)
            => _DeleteBuffers(n, buffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenBuffers(int n, uint* buffers);
        private static d_GenBuffers _GenBuffers;

        public static void GenBuffers(int n, uint* buffers)
            => _GenBuffers(n, buffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetBufferParameteriv(uint target, uint pname, int* @params);
        private static d_GetBufferParameteriv _GetBufferParameteriv;

        public static void GetBufferParameteriv(uint target, uint pname, int* @params)
            => _GetBufferParameteriv(target, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetBufferSubData(uint target, nint offset, nint size, nint data);
        private static d_GetBufferSubData _GetBufferSubData;

        public static void GetBufferSubData(uint target, nint offset, nint size, nint data)
            => _GetBufferSubData(target, offset, size, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsBuffer(uint buffer);
        private static d_IsBuffer _IsBuffer;

        public static bool IsBuffer(uint buffer)
            => _IsBuffer(buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate nint d_MapBuffer(uint target, uint access);
        private static d_MapBuffer _MapBuffer;

        public static nint MapBuffer(uint target, uint access)
            => _MapBuffer(target, access);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate nint d_MapBufferRange(uint target, nint offset, nint length, uint access);
        private static d_MapBufferRange _MapBufferRange;

        public static nint MapBufferRange(uint target, nint offset, nint length, uint access)
            => _MapBufferRange(target, offset, length, access);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_UnmapBuffer(uint target);
        private static d_UnmapBuffer _UnmapBuffer;

        public static bool UnmapBuffer(uint target)
            => _UnmapBuffer(target);

        // --------------------------------------------------------
        // Debug
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DebugMessageCallback(nint callback, nint userParam);
        private static d_DebugMessageCallback _DebugMessageCallback;

        public static void DebugMessageCallback(nint callback, nint userParam)
            => _DebugMessageCallback(callback, userParam);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DebugMessageControl(uint source, uint @type, uint severity, int count, uint* ids, bool enabled);
        private static d_DebugMessageControl _DebugMessageControl;

        public static void DebugMessageControl(uint source, uint @type, uint severity, int count, uint* ids, bool enabled)
            => _DebugMessageControl(source, @type, severity, count, ids, enabled);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DebugMessageInsert(uint source, uint @type, uint id, uint severity, int length, nint buf);
        private static d_DebugMessageInsert _DebugMessageInsert;

        public static void DebugMessageInsert(uint source, uint @type, uint id, uint severity, int length, nint buf)
            => _DebugMessageInsert(source, @type, id, severity, length, buf);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_GetDebugMessageLog(uint count, int bufSize, uint* sources, uint* types, uint* ids, uint* severities, int* lengths, nint messageLog);
        private static d_GetDebugMessageLog _GetDebugMessageLog;

        public static uint GetDebugMessageLog(uint count, int bufSize, uint* sources, uint* types, uint* ids, uint* severities, int* lengths, nint messageLog)
            => _GetDebugMessageLog(count, bufSize, sources, types, ids, severities, lengths, messageLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetObjectLabel(uint identifier, uint name, int bufSize, int* length, nint label);
        private static d_GetObjectLabel _GetObjectLabel;

        public static void GetObjectLabel(uint identifier, uint name, int bufSize, int* length, nint label)
            => _GetObjectLabel(identifier, name, bufSize, length, label);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ObjectLabel(uint identifier, uint name, int length, nint label);
        private static d_ObjectLabel _ObjectLabel;

        public static void ObjectLabel(uint identifier, uint name, int length, nint label)
            => _ObjectLabel(identifier, name, length, label);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_PopDebugGroup();
        private static d_PopDebugGroup _PopDebugGroup;

        public static void PopDebugGroup()
            => _PopDebugGroup();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_PushDebugGroup(uint source, uint id, int length, nint message);
        private static d_PushDebugGroup _PushDebugGroup;

        public static void PushDebugGroup(uint source, uint id, int length, nint message)
            => _PushDebugGroup(source, id, length, message);

        // --------------------------------------------------------
        // Drawing
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_CopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth);
        private static d_CopyImageSubData _CopyImageSubData;

        public static void CopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth)
            => _CopyImageSubData(srcName, srcTarget, srcLevel, srcX, srcY, srcZ, dstName, dstTarget, dstLevel, dstX, dstY, dstZ, srcWidth, srcHeight, srcDepth);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z);
        private static d_DispatchCompute _DispatchCompute;

        public static void DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z)
            => _DispatchCompute(num_groups_x, num_groups_y, num_groups_z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DispatchComputeIndirect(nint indirect);
        private static d_DispatchComputeIndirect _DispatchComputeIndirect;

        public static void DispatchComputeIndirect(nint indirect)
            => _DispatchComputeIndirect(indirect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawArrays(uint mode, int first, int count);
        private static d_DrawArrays _DrawArrays;

        public static void DrawArrays(uint mode, int first, int count)
            => _DrawArrays(mode, first, count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawArraysIndirect(uint mode, nint indirect);
        private static d_DrawArraysIndirect _DrawArraysIndirect;

        public static void DrawArraysIndirect(uint mode, nint indirect)
            => _DrawArraysIndirect(mode, indirect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawArraysInstanced(uint mode, int first, int count, int instancecount);
        private static d_DrawArraysInstanced _DrawArraysInstanced;

        public static void DrawArraysInstanced(uint mode, int first, int count, int instancecount)
            => _DrawArraysInstanced(mode, first, count, instancecount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawElements(uint mode, int count, uint @type, nint indices);
        private static d_DrawElements _DrawElements;

        public static void DrawElements(uint mode, int count, uint @type, nint indices)
            => _DrawElements(mode, count, @type, indices);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawElementsIndirect(uint mode, uint @type, nint indirect);
        private static d_DrawElementsIndirect _DrawElementsIndirect;

        public static void DrawElementsIndirect(uint mode, uint @type, nint indirect)
            => _DrawElementsIndirect(mode, @type, indirect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawElementsInstanced(uint mode, int count, uint @type, nint indices, int instancecount);
        private static d_DrawElementsInstanced _DrawElementsInstanced;

        public static void DrawElementsInstanced(uint mode, int count, uint @type, nint indices, int instancecount)
            => _DrawElementsInstanced(mode, count, @type, indices, instancecount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawRangeElements(uint mode, uint start, uint end, int count, uint @type, nint indices);
        private static d_DrawRangeElements _DrawRangeElements;

        public static void DrawRangeElements(uint mode, uint start, uint end, int count, uint @type, nint indices)
            => _DrawRangeElements(mode, start, end, count, @type, indices);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_MemoryBarrier(uint barriers);
        private static d_MemoryBarrier _MemoryBarrier;

        public static void MemoryBarrier(uint barriers)
            => _MemoryBarrier(barriers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_MultiDrawArrays(uint mode, int* first, int* count, int drawcount);
        private static d_MultiDrawArrays _MultiDrawArrays;

        public static void MultiDrawArrays(uint mode, int* first, int* count, int drawcount)
            => _MultiDrawArrays(mode, first, count, drawcount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_MultiDrawElements(uint mode, int* count, uint @type, nint* indices, int drawcount);
        private static d_MultiDrawElements _MultiDrawElements;

        public static void MultiDrawElements(uint mode, int* count, uint @type, nint* indices, int drawcount)
            => _MultiDrawElements(mode, count, @type, indices, drawcount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ReadPixels(int x, int y, int width, int height, uint format, uint @type, nint pixels);
        private static d_ReadPixels _ReadPixels;

        public static void ReadPixels(int x, int y, int width, int height, uint format, uint @type, nint pixels)
            => _ReadPixels(x, y, width, height, format, @type, pixels);

        // --------------------------------------------------------
        // Framebuffers
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindFramebuffer(uint target, uint framebuffer);
        private static d_BindFramebuffer _BindFramebuffer;

        public static void BindFramebuffer(uint target, uint framebuffer)
            => _BindFramebuffer(target, framebuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);
        private static d_BlitFramebuffer _BlitFramebuffer;

        public static void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter)
            => _BlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_CheckFramebufferStatus(uint target);
        private static d_CheckFramebufferStatus _CheckFramebufferStatus;

        public static uint CheckFramebufferStatus(uint target)
            => _CheckFramebufferStatus(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ClearBufferfi(uint buffer, int drawbuffer, float depth, int stencil);
        private static d_ClearBufferfi _ClearBufferfi;

        public static void ClearBufferfi(uint buffer, int drawbuffer, float depth, int stencil)
            => _ClearBufferfi(buffer, drawbuffer, depth, stencil);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ClearBufferfv(uint buffer, int drawbuffer, float* value);
        private static d_ClearBufferfv _ClearBufferfv;

        public static void ClearBufferfv(uint buffer, int drawbuffer, float* value)
            => _ClearBufferfv(buffer, drawbuffer, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ClearBufferiv(uint buffer, int drawbuffer, int* value);
        private static d_ClearBufferiv _ClearBufferiv;

        public static void ClearBufferiv(uint buffer, int drawbuffer, int* value)
            => _ClearBufferiv(buffer, drawbuffer, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ClearBufferuiv(uint buffer, int drawbuffer, uint* value);
        private static d_ClearBufferuiv _ClearBufferuiv;

        public static void ClearBufferuiv(uint buffer, int drawbuffer, uint* value)
            => _ClearBufferuiv(buffer, drawbuffer, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteFramebuffers(int n, uint* framebuffers);
        private static d_DeleteFramebuffers _DeleteFramebuffers;

        public static void DeleteFramebuffers(int n, uint* framebuffers)
            => _DeleteFramebuffers(n, framebuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);
        private static d_FramebufferRenderbuffer _FramebufferRenderbuffer;

        public static void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer)
            => _FramebufferRenderbuffer(target, attachment, renderbuffertarget, renderbuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_FramebufferTexture(uint target, uint attachment, uint texture, int level);
        private static d_FramebufferTexture _FramebufferTexture;

        public static void FramebufferTexture(uint target, uint attachment, uint texture, int level)
            => _FramebufferTexture(target, attachment, texture, level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_FramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level);
        private static d_FramebufferTexture2D _FramebufferTexture2D;

        public static void FramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level)
            => _FramebufferTexture2D(target, attachment, textarget, texture, level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_FramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);
        private static d_FramebufferTexture3D _FramebufferTexture3D;

        public static void FramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset)
            => _FramebufferTexture3D(target, attachment, textarget, texture, level, zoffset);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_FramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer);
        private static d_FramebufferTextureLayer _FramebufferTextureLayer;

        public static void FramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer)
            => _FramebufferTextureLayer(target, attachment, texture, level, layer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenFramebuffers(int n, uint* framebuffers);
        private static d_GenFramebuffers _GenFramebuffers;

        public static void GenFramebuffers(int n, uint* framebuffers)
            => _GenFramebuffers(n, framebuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, int* @params);
        private static d_GetFramebufferAttachmentParameteriv _GetFramebufferAttachmentParameteriv;

        public static void GetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, int* @params)
            => _GetFramebufferAttachmentParameteriv(target, attachment, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_InvalidateFramebuffer(uint target, int numAttachments, uint* attachments);
        private static d_InvalidateFramebuffer _InvalidateFramebuffer;

        public static void InvalidateFramebuffer(uint target, int numAttachments, uint* attachments)
            => _InvalidateFramebuffer(target, numAttachments, attachments);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsFramebuffer(uint framebuffer);
        private static d_IsFramebuffer _IsFramebuffer;

        public static bool IsFramebuffer(uint framebuffer)
            => _IsFramebuffer(framebuffer);

        // --------------------------------------------------------
        // PipelineObjects
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindProgramPipeline(uint pipeline);
        private static d_BindProgramPipeline _BindProgramPipeline;

        public static void BindProgramPipeline(uint pipeline)
            => _BindProgramPipeline(pipeline);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteProgramPipelines(int n, uint* pipelines);
        private static d_DeleteProgramPipelines _DeleteProgramPipelines;

        public static void DeleteProgramPipelines(int n, uint* pipelines)
            => _DeleteProgramPipelines(n, pipelines);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenProgramPipelines(int n, uint* pipelines);
        private static d_GenProgramPipelines _GenProgramPipelines;

        public static void GenProgramPipelines(int n, uint* pipelines)
            => _GenProgramPipelines(n, pipelines);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetProgramPipelineInfoLog(uint pipeline, int bufSize, int* length, nint infoLog);
        private static d_GetProgramPipelineInfoLog _GetProgramPipelineInfoLog;

        public static void GetProgramPipelineInfoLog(uint pipeline, int bufSize, int* length, nint infoLog)
            => _GetProgramPipelineInfoLog(pipeline, bufSize, length, infoLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetProgramPipelineiv(uint pipeline, uint pname, int* @params);
        private static d_GetProgramPipelineiv _GetProgramPipelineiv;

        public static void GetProgramPipelineiv(uint pipeline, uint pname, int* @params)
            => _GetProgramPipelineiv(pipeline, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsProgramPipeline(uint pipeline);
        private static d_IsProgramPipeline _IsProgramPipeline;

        public static bool IsProgramPipeline(uint pipeline)
            => _IsProgramPipeline(pipeline);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UseProgramStages(uint pipeline, uint stages, uint program);
        private static d_UseProgramStages _UseProgramStages;

        public static void UseProgramStages(uint pipeline, uint stages, uint program)
            => _UseProgramStages(pipeline, stages, program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ValidateProgramPipeline(uint pipeline);
        private static d_ValidateProgramPipeline _ValidateProgramPipeline;

        public static void ValidateProgramPipeline(uint pipeline)
            => _ValidateProgramPipeline(pipeline);

        // --------------------------------------------------------
        // Programs
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_AttachShader(uint program, uint shader);
        private static d_AttachShader _AttachShader;

        public static void AttachShader(uint program, uint shader)
            => _AttachShader(program, shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindAttribLocation(uint program, uint index, nint name);
        private static d_BindAttribLocation _BindAttribLocation;

        public static void BindAttribLocation(uint program, uint index, nint name)
            => _BindAttribLocation(program, index, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_CreateProgram();
        private static d_CreateProgram _CreateProgram;

        public static uint CreateProgram()
            => _CreateProgram();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteProgram(uint program);
        private static d_DeleteProgram _DeleteProgram;

        public static void DeleteProgram(uint program)
            => _DeleteProgram(program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DetachShader(uint program, uint shader);
        private static d_DetachShader _DetachShader;

        public static void DetachShader(uint program, uint shader)
            => _DetachShader(program, shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetActiveAttrib(uint program, uint index, int bufSize, int* length, int* size, uint* @type, nint name);
        private static d_GetActiveAttrib _GetActiveAttrib;

        public static void GetActiveAttrib(uint program, uint index, int bufSize, int* length, int* size, uint* @type, nint name)
            => _GetActiveAttrib(program, index, bufSize, length, size, @type, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetActiveUniform(uint program, uint index, int bufSize, int* length, int* size, uint* @type, nint name);
        private static d_GetActiveUniform _GetActiveUniform;

        public static void GetActiveUniform(uint program, uint index, int bufSize, int* length, int* size, uint* @type, nint name)
            => _GetActiveUniform(program, index, bufSize, length, size, @type, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int d_GetAttribLocation(uint program, nint name);
        private static d_GetAttribLocation _GetAttribLocation;

        public static int GetAttribLocation(uint program, nint name)
            => _GetAttribLocation(program, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetProgramBinary(uint program, int bufSize, int* length, uint* binaryFormat, nint binary);
        private static d_GetProgramBinary _GetProgramBinary;

        public static void GetProgramBinary(uint program, int bufSize, int* length, uint* binaryFormat, nint binary)
            => _GetProgramBinary(program, bufSize, length, binaryFormat, binary);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetProgramInfoLog(uint program, int bufSize, int* length, nint infoLog);
        private static d_GetProgramInfoLog _GetProgramInfoLog;

        public static void GetProgramInfoLog(uint program, int bufSize, int* length, nint infoLog)
            => _GetProgramInfoLog(program, bufSize, length, infoLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetProgramiv(uint program, uint pname, int* @params);
        private static d_GetProgramiv _GetProgramiv;

        public static void GetProgramiv(uint program, uint pname, int* @params)
            => _GetProgramiv(program, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_GetProgramResourceIndex(uint program, uint programInterface, nint name);
        private static d_GetProgramResourceIndex _GetProgramResourceIndex;

        public static uint GetProgramResourceIndex(uint program, uint programInterface, nint name)
            => _GetProgramResourceIndex(program, programInterface, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetProgramResourceiv(uint program, uint programInterface, uint index, int propCount, uint* props, int count, int* length, int* values);
        private static d_GetProgramResourceiv _GetProgramResourceiv;

        public static void GetProgramResourceiv(uint program, uint programInterface, uint index, int propCount, uint* props, int count, int* length, int* values)
            => _GetProgramResourceiv(program, programInterface, index, propCount, props, count, length, values);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetProgramResourceName(uint program, uint programInterface, uint index, int bufSize, int* length, nint name);
        private static d_GetProgramResourceName _GetProgramResourceName;

        public static void GetProgramResourceName(uint program, uint programInterface, uint index, int bufSize, int* length, nint name)
            => _GetProgramResourceName(program, programInterface, index, bufSize, length, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_GetUniformBlockIndex(uint program, nint uniformBlockName);
        private static d_GetUniformBlockIndex _GetUniformBlockIndex;

        public static uint GetUniformBlockIndex(uint program, nint uniformBlockName)
            => _GetUniformBlockIndex(program, uniformBlockName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int d_GetUniformLocation(uint program, nint name);
        private static d_GetUniformLocation _GetUniformLocation;

        public static int GetUniformLocation(uint program, nint name)
            => _GetUniformLocation(program, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsProgram(uint program);
        private static d_IsProgram _IsProgram;

        public static bool IsProgram(uint program)
            => _IsProgram(program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_LinkProgram(uint program);
        private static d_LinkProgram _LinkProgram;

        public static void LinkProgram(uint program)
            => _LinkProgram(program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ProgramBinary(uint program, uint binaryFormat, nint binary, int length);
        private static d_ProgramBinary _ProgramBinary;

        public static void ProgramBinary(uint program, uint binaryFormat, nint binary, int length)
            => _ProgramBinary(program, binaryFormat, binary, length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ProgramParameteri(uint program, uint pname, int value);
        private static d_ProgramParameteri _ProgramParameteri;

        public static void ProgramParameteri(uint program, uint pname, int value)
            => _ProgramParameteri(program, pname, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding);
        private static d_ShaderStorageBlockBinding _ShaderStorageBlockBinding;

        public static void ShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding)
            => _ShaderStorageBlockBinding(program, storageBlockIndex, storageBlockBinding);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding);
        private static d_UniformBlockBinding _UniformBlockBinding;

        public static void UniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding)
            => _UniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UseProgram(uint program);
        private static d_UseProgram _UseProgram;

        public static void UseProgram(uint program)
            => _UseProgram(program);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ValidateProgram(uint program);
        private static d_ValidateProgram _ValidateProgram;

        public static void ValidateProgram(uint program)
            => _ValidateProgram(program);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BeginConditionalRender(uint id, uint mode);
        private static d_BeginConditionalRender _BeginConditionalRender;

        public static void BeginConditionalRender(uint id, uint mode)
            => _BeginConditionalRender(id, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BeginQuery(uint target, uint id);
        private static d_BeginQuery _BeginQuery;

        public static void BeginQuery(uint target, uint id)
            => _BeginQuery(target, id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteQueries(int n, uint* ids);
        private static d_DeleteQueries _DeleteQueries;

        public static void DeleteQueries(int n, uint* ids)
            => _DeleteQueries(n, ids);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_EndConditionalRender();
        private static d_EndConditionalRender _EndConditionalRender;

        public static void EndConditionalRender()
            => _EndConditionalRender();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_EndQuery(uint target);
        private static d_EndQuery _EndQuery;

        public static void EndQuery(uint target)
            => _EndQuery(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenQueries(int n, uint* ids);
        private static d_GenQueries _GenQueries;

        public static void GenQueries(int n, uint* ids)
            => _GenQueries(n, ids);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetQueryiv(uint target, uint pname, int* @params);
        private static d_GetQueryiv _GetQueryiv;

        public static void GetQueryiv(uint target, uint pname, int* @params)
            => _GetQueryiv(target, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetQueryObjecti64v(uint id, uint pname, long* @params);
        private static d_GetQueryObjecti64v _GetQueryObjecti64v;

        public static void GetQueryObjecti64v(uint id, uint pname, long* @params)
            => _GetQueryObjecti64v(id, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetQueryObjectiv(uint id, uint pname, int* @params);
        private static d_GetQueryObjectiv _GetQueryObjectiv;

        public static void GetQueryObjectiv(uint id, uint pname, int* @params)
            => _GetQueryObjectiv(id, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetQueryObjectui64v(uint id, uint pname, ulong* @params);
        private static d_GetQueryObjectui64v _GetQueryObjectui64v;

        public static void GetQueryObjectui64v(uint id, uint pname, ulong* @params)
            => _GetQueryObjectui64v(id, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetQueryObjectuiv(uint id, uint pname, uint* @params);
        private static d_GetQueryObjectuiv _GetQueryObjectuiv;

        public static void GetQueryObjectuiv(uint id, uint pname, uint* @params)
            => _GetQueryObjectuiv(id, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsQuery(uint id);
        private static d_IsQuery _IsQuery;

        public static bool IsQuery(uint id)
            => _IsQuery(id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_QueryCounter(uint id, uint target);
        private static d_QueryCounter _QueryCounter;

        public static void QueryCounter(uint id, uint target)
            => _QueryCounter(id, target);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindRenderbuffer(uint target, uint renderbuffer);
        private static d_BindRenderbuffer _BindRenderbuffer;

        public static void BindRenderbuffer(uint target, uint renderbuffer)
            => _BindRenderbuffer(target, renderbuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteRenderbuffers(int n, uint* renderbuffers);
        private static d_DeleteRenderbuffers _DeleteRenderbuffers;

        public static void DeleteRenderbuffers(int n, uint* renderbuffers)
            => _DeleteRenderbuffers(n, renderbuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenRenderbuffers(int n, uint* renderbuffers);
        private static d_GenRenderbuffers _GenRenderbuffers;

        public static void GenRenderbuffers(int n, uint* renderbuffers)
            => _GenRenderbuffers(n, renderbuffers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsRenderbuffer(uint renderbuffer);
        private static d_IsRenderbuffer _IsRenderbuffer;

        public static bool IsRenderbuffer(uint renderbuffer)
            => _IsRenderbuffer(renderbuffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_RenderbufferStorage(uint target, uint internalformat, int width, int height);
        private static d_RenderbufferStorage _RenderbufferStorage;

        public static void RenderbufferStorage(uint target, uint internalformat, int width, int height)
            => _RenderbufferStorage(target, internalformat, width, height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_RenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height);
        private static d_RenderbufferStorageMultisample _RenderbufferStorageMultisample;

        public static void RenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height)
            => _RenderbufferStorageMultisample(target, samples, internalformat, width, height);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindSampler(uint unit, uint sampler);
        private static d_BindSampler _BindSampler;

        public static void BindSampler(uint unit, uint sampler)
            => _BindSampler(unit, sampler);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteSamplers(int count, uint* samplers);
        private static d_DeleteSamplers _DeleteSamplers;

        public static void DeleteSamplers(int count, uint* samplers)
            => _DeleteSamplers(count, samplers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenSamplers(int count, uint* samplers);
        private static d_GenSamplers _GenSamplers;

        public static void GenSamplers(int count, uint* samplers)
            => _GenSamplers(count, samplers);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsSampler(uint sampler);
        private static d_IsSampler _IsSampler;

        public static bool IsSampler(uint sampler)
            => _IsSampler(sampler);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_SamplerParameterf(uint sampler, uint pname, float param);
        private static d_SamplerParameterf _SamplerParameterf;

        public static void SamplerParameterf(uint sampler, uint pname, float param)
            => _SamplerParameterf(sampler, pname, param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_SamplerParameterfv(uint sampler, uint pname, float* param);
        private static d_SamplerParameterfv _SamplerParameterfv;

        public static void SamplerParameterfv(uint sampler, uint pname, float* param)
            => _SamplerParameterfv(sampler, pname, param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_SamplerParameteri(uint sampler, uint pname, int param);
        private static d_SamplerParameteri _SamplerParameteri;

        public static void SamplerParameteri(uint sampler, uint pname, int param)
            => _SamplerParameteri(sampler, pname, param);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_CompileShader(uint shader);
        private static d_CompileShader _CompileShader;

        public static void CompileShader(uint shader)
            => _CompileShader(shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_CreateShader(uint @type);
        private static d_CreateShader _CreateShader;

        public static uint CreateShader(uint @type)
            => _CreateShader(@type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteShader(uint shader);
        private static d_DeleteShader _DeleteShader;

        public static void DeleteShader(uint shader)
            => _DeleteShader(shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetShaderInfoLog(uint shader, int bufSize, int* length, nint infoLog);
        private static d_GetShaderInfoLog _GetShaderInfoLog;

        public static void GetShaderInfoLog(uint shader, int bufSize, int* length, nint infoLog)
            => _GetShaderInfoLog(shader, bufSize, length, infoLog);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetShaderiv(uint shader, uint pname, int* @params);
        private static d_GetShaderiv _GetShaderiv;

        public static void GetShaderiv(uint shader, uint pname, int* @params)
            => _GetShaderiv(shader, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetShaderSource(uint shader, int bufSize, int* length, nint source);
        private static d_GetShaderSource _GetShaderSource;

        public static void GetShaderSource(uint shader, int bufSize, int* length, nint source)
            => _GetShaderSource(shader, bufSize, length, source);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsShader(uint shader);
        private static d_IsShader _IsShader;

        public static bool IsShader(uint shader)
            => _IsShader(shader);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ShaderSource(uint shader, int count, nint @string, int* length);
        private static d_ShaderSource _ShaderSource;

        public static void ShaderSource(uint shader, int count, nint @string, int* length)
            => _ShaderSource(shader, count, @string, length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_SpecialiseShader(uint shader, nint pEntryPoint, uint numSpecializationConstants, uint* pConstantIndex, uint* pConstantValue);
        private static d_SpecialiseShader _SpecialiseShader;

        public static void SpecialiseShader(uint shader, nint pEntryPoint, uint numSpecializationConstants, uint* pConstantIndex, uint* pConstantValue)
            => _SpecialiseShader(shader, pEntryPoint, numSpecializationConstants, pConstantIndex, pConstantValue);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BlendColour(float red, float green, float blue, float alpha);
        private static d_BlendColour _BlendColour;

        public static void BlendColour(float red, float green, float blue, float alpha)
            => _BlendColour(red, green, blue, alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BlendEquation(uint mode);
        private static d_BlendEquation _BlendEquation;

        public static void BlendEquation(uint mode)
            => _BlendEquation(mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BlendFunc(uint sfactor, uint dfactor);
        private static d_BlendFunc _BlendFunc;

        public static void BlendFunc(uint sfactor, uint dfactor)
            => _BlendFunc(sfactor, dfactor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BlendFuncSeparate(uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha);
        private static d_BlendFuncSeparate _BlendFuncSeparate;

        public static void BlendFuncSeparate(uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha)
            => _BlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Clear(uint mask);
        private static d_Clear _Clear;

        public static void Clear(uint mask)
            => _Clear(mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ClearColour(float red, float green, float blue, float alpha);
        private static d_ClearColour _ClearColour;

        public static void ClearColour(float red, float green, float blue, float alpha)
            => _ClearColour(red, green, blue, alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ClipControl(uint origin, uint depth);
        private static d_ClipControl _ClipControl;

        public static void ClipControl(uint origin, uint depth)
            => _ClipControl(origin, depth);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ColourMask(bool red, bool green, bool blue, bool alpha);
        private static d_ColourMask _ColourMask;

        public static void ColourMask(bool red, bool green, bool blue, bool alpha)
            => _ColourMask(red, green, blue, alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_CullFace(uint mode);
        private static d_CullFace _CullFace;

        public static void CullFace(uint mode)
            => _CullFace(mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DepthFunc(uint func);
        private static d_DepthFunc _DepthFunc;

        public static void DepthFunc(uint func)
            => _DepthFunc(func);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DepthMask(bool flag);
        private static d_DepthMask _DepthMask;

        public static void DepthMask(bool flag)
            => _DepthMask(flag);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DepthRange(double nearVal, double farVal);
        private static d_DepthRange _DepthRange;

        public static void DepthRange(double nearVal, double farVal)
            => _DepthRange(nearVal, farVal);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Disable(uint cap);
        private static d_Disable _Disable;

        public static void Disable(uint cap)
            => _Disable(cap);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawBuffer(uint buf);
        private static d_DrawBuffer _DrawBuffer;

        public static void DrawBuffer(uint buf)
            => _DrawBuffer(buf);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawBuffers(int n, uint* bufs);
        private static d_DrawBuffers _DrawBuffers;

        public static void DrawBuffers(int n, uint* bufs)
            => _DrawBuffers(n, bufs);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Enable(uint cap);
        private static d_Enable _Enable;

        public static void Enable(uint cap)
            => _Enable(cap);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Finish();
        private static d_Finish _Finish;

        public static void Finish()
            => _Finish();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Flush();
        private static d_Flush _Flush;

        public static void Flush()
            => _Flush();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_FrontFace(uint mode);
        private static d_FrontFace _FrontFace;

        public static void FrontFace(uint mode)
            => _FrontFace(mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetBooleanv(uint pname, sbyte* data);
        private static d_GetBooleanv _GetBooleanv;

        public static void GetBooleanv(uint pname, sbyte* data)
            => _GetBooleanv(pname, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetDoublev(uint pname, double* data);
        private static d_GetDoublev _GetDoublev;

        public static void GetDoublev(uint pname, double* data)
            => _GetDoublev(pname, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_GetError();
        private static d_GetError _GetError;

        public static uint GetError()
            => _GetError();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetFloatv(uint pname, float* data);
        private static d_GetFloatv _GetFloatv;

        public static void GetFloatv(uint pname, float* data)
            => _GetFloatv(pname, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetIntegerv(uint pname, int* data);
        private static d_GetIntegerv _GetIntegerv;

        public static void GetIntegerv(uint pname, int* data)
            => _GetIntegerv(pname, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string d_GetString(uint name);
        private static d_GetString _GetString;

        public static string GetString(uint name)
            => _GetString(name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string d_GetStringi(uint name, uint index);
        private static d_GetStringi _GetStringi;

        public static string GetStringi(uint name, uint index)
            => _GetStringi(name, index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Hint(uint target, uint mode);
        private static d_Hint _Hint;

        public static void Hint(uint target, uint mode)
            => _Hint(target, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsEnabled(uint cap);
        private static d_IsEnabled _IsEnabled;

        public static bool IsEnabled(uint cap)
            => _IsEnabled(cap);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_LineWidth(float width);
        private static d_LineWidth _LineWidth;

        public static void LineWidth(float width)
            => _LineWidth(width);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_LogicOp(uint opcode);
        private static d_LogicOp _LogicOp;

        public static void LogicOp(uint opcode)
            => _LogicOp(opcode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_PointSize(float size);
        private static d_PointSize _PointSize;

        public static void PointSize(float size)
            => _PointSize(size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_PolygonMode(uint face, uint mode);
        private static d_PolygonMode _PolygonMode;

        public static void PolygonMode(uint face, uint mode)
            => _PolygonMode(face, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_PolygonOffset(float factor, float units);
        private static d_PolygonOffset _PolygonOffset;

        public static void PolygonOffset(float factor, float units)
            => _PolygonOffset(factor, units);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ReadBuffer(uint src);
        private static d_ReadBuffer _ReadBuffer;

        public static void ReadBuffer(uint src)
            => _ReadBuffer(src);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Scissor(int x, int y, int width, int height);
        private static d_Scissor _Scissor;

        public static void Scissor(int x, int y, int width, int height)
            => _Scissor(x, y, width, height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_StencilFunc(uint func, int @ref, uint mask);
        private static d_StencilFunc _StencilFunc;

        public static void StencilFunc(uint func, int @ref, uint mask)
            => _StencilFunc(func, @ref, mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_StencilMask(uint mask);
        private static d_StencilMask _StencilMask;

        public static void StencilMask(uint mask)
            => _StencilMask(mask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_StencilOp(uint sfail, uint dpfail, uint dppass);
        private static d_StencilOp _StencilOp;

        public static void StencilOp(uint sfail, uint dpfail, uint dppass)
            => _StencilOp(sfail, dpfail, dppass);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Viewport(int x, int y, int width, int height);
        private static d_Viewport _Viewport;

        public static void Viewport(int x, int y, int width, int height)
            => _Viewport(x, y, width, height);

        // --------------------------------------------------------
        // Sync
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint d_ClientWaitSync(nint sync, uint flags, ulong timeout);
        private static d_ClientWaitSync _ClientWaitSync;

        public static uint ClientWaitSync(nint sync, uint flags, ulong timeout)
            => _ClientWaitSync(sync, flags, timeout);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteSync(nint sync);
        private static d_DeleteSync _DeleteSync;

        public static void DeleteSync(nint sync)
            => _DeleteSync(sync);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate nint d_FenceSync(uint condition, uint flags);
        private static d_FenceSync _FenceSync;

        public static nint FenceSync(uint condition, uint flags)
            => _FenceSync(condition, flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetSynciv(nint sync, uint pname, int count, int* length, int* values);
        private static d_GetSynciv _GetSynciv;

        public static void GetSynciv(nint sync, uint pname, int count, int* length, int* values)
            => _GetSynciv(sync, pname, count, length, values);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsSync(nint sync);
        private static d_IsSync _IsSync;

        public static bool IsSync(nint sync)
            => _IsSync(sync);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_WaitSync(nint sync, uint flags, ulong timeout);
        private static d_WaitSync _WaitSync;

        public static void WaitSync(nint sync, uint flags, ulong timeout)
            => _WaitSync(sync, flags, timeout);

        // --------------------------------------------------------
        // Textures
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ActiveTexture(uint texture);
        private static d_ActiveTexture _ActiveTexture;

        public static void ActiveTexture(uint texture)
            => _ActiveTexture(texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindImageTexture(uint unit, uint texture, int level, bool layered, int layer, uint access, uint format);
        private static d_BindImageTexture _BindImageTexture;

        public static void BindImageTexture(uint unit, uint texture, int level, bool layered, int layer, uint access, uint format)
            => _BindImageTexture(unit, texture, level, layered, layer, access, format);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindTexture(uint target, uint texture);
        private static d_BindTexture _BindTexture;

        public static void BindTexture(uint target, uint texture)
            => _BindTexture(target, texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindTextureUnit(uint unit, uint texture);
        private static d_BindTextureUnit _BindTextureUnit;

        public static void BindTextureUnit(uint unit, uint texture)
            => _BindTextureUnit(unit, texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ClearTexImage(uint texture, int level, uint format, uint @type, nint data);
        private static d_ClearTexImage _ClearTexImage;

        public static void ClearTexImage(uint texture, int level, uint format, uint @type, nint data)
            => _ClearTexImage(texture, level, format, @type, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, nint data);
        private static d_CompressedTexImage2D _CompressedTexImage2D;

        public static void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, nint data)
            => _CompressedTexImage2D(target, level, internalformat, width, height, border, imageSize, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteTextures(int n, uint* textures);
        private static d_DeleteTextures _DeleteTextures;

        public static void DeleteTextures(int n, uint* textures)
            => _DeleteTextures(n, textures);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenerateMipmap(uint target);
        private static d_GenerateMipmap _GenerateMipmap;

        public static void GenerateMipmap(uint target)
            => _GenerateMipmap(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenTextures(int n, uint* textures);
        private static d_GenTextures _GenTextures;

        public static void GenTextures(int n, uint* textures)
            => _GenTextures(n, textures);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetTexImage(uint target, int level, uint format, uint @type, nint pixels);
        private static d_GetTexImage _GetTexImage;

        public static void GetTexImage(uint target, int level, uint format, uint @type, nint pixels)
            => _GetTexImage(target, level, format, @type, pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetTexLevelParameteriv(uint target, int level, uint pname, int* @params);
        private static d_GetTexLevelParameteriv _GetTexLevelParameteriv;

        public static void GetTexLevelParameteriv(uint target, int level, uint pname, int* @params)
            => _GetTexLevelParameteriv(target, level, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_InvalidateTexImage(uint texture, int level);
        private static d_InvalidateTexImage _InvalidateTexImage;

        public static void InvalidateTexImage(uint texture, int level)
            => _InvalidateTexImage(texture, level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsTexture(uint texture);
        private static d_IsTexture _IsTexture;

        public static bool IsTexture(uint texture)
            => _IsTexture(texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint @type, nint pixels);
        private static d_TexImage1D _TexImage1D;

        public static void TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint @type, nint pixels)
            => _TexImage1D(target, level, internalformat, width, border, format, @type, pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint @type, nint pixels);
        private static d_TexImage2D _TexImage2D;

        public static void TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint @type, nint pixels)
            => _TexImage2D(target, level, internalformat, width, height, border, format, @type, pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint @type, nint pixels);
        private static d_TexImage3D _TexImage3D;

        public static void TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint @type, nint pixels)
            => _TexImage3D(target, level, internalformat, width, height, depth, border, format, @type, pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexParameterf(uint target, uint pname, float param);
        private static d_TexParameterf _TexParameterf;

        public static void TexParameterf(uint target, uint pname, float param)
            => _TexParameterf(target, pname, param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexParameterfv(uint target, uint pname, float* @params);
        private static d_TexParameterfv _TexParameterfv;

        public static void TexParameterfv(uint target, uint pname, float* @params)
            => _TexParameterfv(target, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexParameteri(uint target, uint pname, int param);
        private static d_TexParameteri _TexParameteri;

        public static void TexParameteri(uint target, uint pname, int param)
            => _TexParameteri(target, pname, param);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexParameteriv(uint target, uint pname, int* @params);
        private static d_TexParameteriv _TexParameteriv;

        public static void TexParameteriv(uint target, uint pname, int* @params)
            => _TexParameteriv(target, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexStorage2D(uint target, int levels, uint internalformat, int width, int height);
        private static d_TexStorage2D _TexStorage2D;

        public static void TexStorage2D(uint target, int levels, uint internalformat, int width, int height)
            => _TexStorage2D(target, levels, internalformat, width, height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexStorage3D(uint target, int levels, uint internalformat, int width, int height, int depth);
        private static d_TexStorage3D _TexStorage3D;

        public static void TexStorage3D(uint target, int levels, uint internalformat, int width, int height, int depth)
            => _TexStorage3D(target, levels, internalformat, width, height, depth);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint @type, nint pixels);
        private static d_TexSubImage2D _TexSubImage2D;

        public static void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint @type, nint pixels)
            => _TexSubImage2D(target, level, xoffset, yoffset, width, height, format, @type, pixels);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint @type, nint pixels);
        private static d_TexSubImage3D _TexSubImage3D;

        public static void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint @type, nint pixels)
            => _TexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, @type, pixels);

        // --------------------------------------------------------
        // TransformFeedback
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BeginTransformFeedback(uint primitiveMode);
        private static d_BeginTransformFeedback _BeginTransformFeedback;

        public static void BeginTransformFeedback(uint primitiveMode)
            => _BeginTransformFeedback(primitiveMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindTransformFeedback(uint target, uint id);
        private static d_BindTransformFeedback _BindTransformFeedback;

        public static void BindTransformFeedback(uint target, uint id)
            => _BindTransformFeedback(target, id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteTransformFeedbacks(int n, uint* ids);
        private static d_DeleteTransformFeedbacks _DeleteTransformFeedbacks;

        public static void DeleteTransformFeedbacks(int n, uint* ids)
            => _DeleteTransformFeedbacks(n, ids);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawTransformFeedback(uint mode, uint id);
        private static d_DrawTransformFeedback _DrawTransformFeedback;

        public static void DrawTransformFeedback(uint mode, uint id)
            => _DrawTransformFeedback(mode, id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DrawTransformFeedbackInstanced(uint mode, uint id, int instancecount);
        private static d_DrawTransformFeedbackInstanced _DrawTransformFeedbackInstanced;

        public static void DrawTransformFeedbackInstanced(uint mode, uint id, int instancecount)
            => _DrawTransformFeedbackInstanced(mode, id, instancecount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_EndTransformFeedback();
        private static d_EndTransformFeedback _EndTransformFeedback;

        public static void EndTransformFeedback()
            => _EndTransformFeedback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenTransformFeedbacks(int n, uint* ids);
        private static d_GenTransformFeedbacks _GenTransformFeedbacks;

        public static void GenTransformFeedbacks(int n, uint* ids)
            => _GenTransformFeedbacks(n, ids);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_PauseTransformFeedback();
        private static d_PauseTransformFeedback _PauseTransformFeedback;

        public static void PauseTransformFeedback()
            => _PauseTransformFeedback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_ResumeTransformFeedback();
        private static d_ResumeTransformFeedback _ResumeTransformFeedback;

        public static void ResumeTransformFeedback()
            => _ResumeTransformFeedback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_TransformFeedbackVaryings(uint program, int count, nint* varyings, uint bufferMode);
        private static d_TransformFeedbackVaryings _TransformFeedbackVaryings;

        public static void TransformFeedbackVaryings(uint program, int count, nint* varyings, uint bufferMode)
            => _TransformFeedbackVaryings(program, count, varyings, bufferMode);

        // --------------------------------------------------------
        // Uniforms
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform1d(int location, double x);
        private static d_Uniform1d _Uniform1d;

        public static void Uniform1d(int location, double x)
            => _Uniform1d(location, x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform1f(int location, float v0);
        private static d_Uniform1f _Uniform1f;

        public static void Uniform1f(int location, float v0)
            => _Uniform1f(location, v0);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform1fv(int location, int count, float* value);
        private static d_Uniform1fv _Uniform1fv;

        public static void Uniform1fv(int location, int count, float* value)
            => _Uniform1fv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform1i(int location, int v0);
        private static d_Uniform1i _Uniform1i;

        public static void Uniform1i(int location, int v0)
            => _Uniform1i(location, v0);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform1iv(int location, int count, int* value);
        private static d_Uniform1iv _Uniform1iv;

        public static void Uniform1iv(int location, int count, int* value)
            => _Uniform1iv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform1ui(int location, uint v0);
        private static d_Uniform1ui _Uniform1ui;

        public static void Uniform1ui(int location, uint v0)
            => _Uniform1ui(location, v0);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform2d(int location, double x, double y);
        private static d_Uniform2d _Uniform2d;

        public static void Uniform2d(int location, double x, double y)
            => _Uniform2d(location, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform2f(int location, float v0, float v1);
        private static d_Uniform2f _Uniform2f;

        public static void Uniform2f(int location, float v0, float v1)
            => _Uniform2f(location, v0, v1);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform2fv(int location, int count, float* value);
        private static d_Uniform2fv _Uniform2fv;

        public static void Uniform2fv(int location, int count, float* value)
            => _Uniform2fv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform2i(int location, int v0, int v1);
        private static d_Uniform2i _Uniform2i;

        public static void Uniform2i(int location, int v0, int v1)
            => _Uniform2i(location, v0, v1);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform2iv(int location, int count, int* value);
        private static d_Uniform2iv _Uniform2iv;

        public static void Uniform2iv(int location, int count, int* value)
            => _Uniform2iv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform2ui(int location, uint v0, uint v1);
        private static d_Uniform2ui _Uniform2ui;

        public static void Uniform2ui(int location, uint v0, uint v1)
            => _Uniform2ui(location, v0, v1);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform3d(int location, double x, double y, double z);
        private static d_Uniform3d _Uniform3d;

        public static void Uniform3d(int location, double x, double y, double z)
            => _Uniform3d(location, x, y, z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform3f(int location, float v0, float v1, float v2);
        private static d_Uniform3f _Uniform3f;

        public static void Uniform3f(int location, float v0, float v1, float v2)
            => _Uniform3f(location, v0, v1, v2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform3fv(int location, int count, float* value);
        private static d_Uniform3fv _Uniform3fv;

        public static void Uniform3fv(int location, int count, float* value)
            => _Uniform3fv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform3i(int location, int v0, int v1, int v2);
        private static d_Uniform3i _Uniform3i;

        public static void Uniform3i(int location, int v0, int v1, int v2)
            => _Uniform3i(location, v0, v1, v2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform3iv(int location, int count, int* value);
        private static d_Uniform3iv _Uniform3iv;

        public static void Uniform3iv(int location, int count, int* value)
            => _Uniform3iv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform3ui(int location, uint v0, uint v1, uint v2);
        private static d_Uniform3ui _Uniform3ui;

        public static void Uniform3ui(int location, uint v0, uint v1, uint v2)
            => _Uniform3ui(location, v0, v1, v2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform4d(int location, double x, double y, double z, double w);
        private static d_Uniform4d _Uniform4d;

        public static void Uniform4d(int location, double x, double y, double z, double w)
            => _Uniform4d(location, x, y, z, w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform4f(int location, float v0, float v1, float v2, float v3);
        private static d_Uniform4f _Uniform4f;

        public static void Uniform4f(int location, float v0, float v1, float v2, float v3)
            => _Uniform4f(location, v0, v1, v2, v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform4fv(int location, int count, float* value);
        private static d_Uniform4fv _Uniform4fv;

        public static void Uniform4fv(int location, int count, float* value)
            => _Uniform4fv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform4i(int location, int v0, int v1, int v2, int v3);
        private static d_Uniform4i _Uniform4i;

        public static void Uniform4i(int location, int v0, int v1, int v2, int v3)
            => _Uniform4i(location, v0, v1, v2, v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform4iv(int location, int count, int* value);
        private static d_Uniform4iv _Uniform4iv;

        public static void Uniform4iv(int location, int count, int* value)
            => _Uniform4iv(location, count, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3);
        private static d_Uniform4ui _Uniform4ui;

        public static void Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3)
            => _Uniform4ui(location, v0, v1, v2, v3);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix2fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix2fv _UniformMatrix2fv;

        public static void UniformMatrix2fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix2fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix2x3fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix2x3fv _UniformMatrix2x3fv;

        public static void UniformMatrix2x3fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix2x3fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix2x4fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix2x4fv _UniformMatrix2x4fv;

        public static void UniformMatrix2x4fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix2x4fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix3fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix3fv _UniformMatrix3fv;

        public static void UniformMatrix3fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix3fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix3x2fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix3x2fv _UniformMatrix3x2fv;

        public static void UniformMatrix3x2fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix3x2fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix3x4fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix3x4fv _UniformMatrix3x4fv;

        public static void UniformMatrix3x4fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix3x4fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix4fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix4fv _UniformMatrix4fv;

        public static void UniformMatrix4fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix4fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix4x2fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix4x2fv _UniformMatrix4x2fv;

        public static void UniformMatrix4x2fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix4x2fv(location, count, transpose, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_UniformMatrix4x3fv(int location, int count, bool transpose, float* value);
        private static d_UniformMatrix4x3fv _UniformMatrix4x3fv;

        public static void UniformMatrix4x3fv(int location, int count, bool transpose, float* value)
            => _UniformMatrix4x3fv(location, count, transpose, value);

        // --------------------------------------------------------
        // VertexArrays
        // --------------------------------------------------------

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_BindVertexArray(uint array);
        private static d_BindVertexArray _BindVertexArray;

        public static void BindVertexArray(uint array)
            => _BindVertexArray(array);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DeleteVertexArrays(int n, uint* arrays);
        private static d_DeleteVertexArrays _DeleteVertexArrays;

        public static void DeleteVertexArrays(int n, uint* arrays)
            => _DeleteVertexArrays(n, arrays);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_DisableVertexAttribArray(uint index);
        private static d_DisableVertexAttribArray _DisableVertexAttribArray;

        public static void DisableVertexAttribArray(uint index)
            => _DisableVertexAttribArray(index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_EnableVertexAttribArray(uint index);
        private static d_EnableVertexAttribArray _EnableVertexAttribArray;

        public static void EnableVertexAttribArray(uint index)
            => _EnableVertexAttribArray(index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GenVertexArrays(int n, uint* arrays);
        private static d_GenVertexArrays _GenVertexArrays;

        public static void GenVertexArrays(int n, uint* arrays)
            => _GenVertexArrays(n, arrays);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetVertexAttribfv(uint index, uint pname, float* @params);
        private static d_GetVertexAttribfv _GetVertexAttribfv;

        public static void GetVertexAttribfv(uint index, uint pname, float* @params)
            => _GetVertexAttribfv(index, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetVertexAttribiv(uint index, uint pname, int* @params);
        private static d_GetVertexAttribiv _GetVertexAttribiv;

        public static void GetVertexAttribiv(uint index, uint pname, int* @params)
            => _GetVertexAttribiv(index, pname, @params);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_GetVertexAttribPointerv(uint index, uint pname, nint* pointer);
        private static d_GetVertexAttribPointerv _GetVertexAttribPointerv;

        public static void GetVertexAttribPointerv(uint index, uint pname, nint* pointer)
            => _GetVertexAttribPointerv(index, pname, pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool d_IsVertexArray(uint array);
        private static d_IsVertexArray _IsVertexArray;

        public static bool IsVertexArray(uint array)
            => _IsVertexArray(array);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttrib1f(uint index, float x);
        private static d_VertexAttrib1f _VertexAttrib1f;

        public static void VertexAttrib1f(uint index, float x)
            => _VertexAttrib1f(index, x);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttrib2f(uint index, float x, float y);
        private static d_VertexAttrib2f _VertexAttrib2f;

        public static void VertexAttrib2f(uint index, float x, float y)
            => _VertexAttrib2f(index, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttrib3f(uint index, float x, float y, float z);
        private static d_VertexAttrib3f _VertexAttrib3f;

        public static void VertexAttrib3f(uint index, float x, float y, float z)
            => _VertexAttrib3f(index, x, y, z);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttrib4f(uint index, float x, float y, float z, float w);
        private static d_VertexAttrib4f _VertexAttrib4f;

        public static void VertexAttrib4f(uint index, float x, float y, float z, float w)
            => _VertexAttrib4f(index, x, y, z, w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttribDivisor(uint index, uint divisor);
        private static d_VertexAttribDivisor _VertexAttribDivisor;

        public static void VertexAttribDivisor(uint index, uint divisor)
            => _VertexAttribDivisor(index, divisor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttribIPointer(uint index, int size, uint @type, int stride, nint pointer);
        private static d_VertexAttribIPointer _VertexAttribIPointer;

        public static void VertexAttribIPointer(uint index, int size, uint @type, int stride, nint pointer)
            => _VertexAttribIPointer(index, size, @type, stride, pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttribLPointer(uint index, int size, uint @type, int stride, nint pointer);
        private static d_VertexAttribLPointer _VertexAttribLPointer;

        public static void VertexAttribLPointer(uint index, int size, uint @type, int stride, nint pointer)
            => _VertexAttribLPointer(index, size, @type, stride, pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_VertexAttribPointer(uint index, int size, uint @type, bool normalized, int stride, nint pointer);
        private static d_VertexAttribPointer _VertexAttribPointer;

        public static void VertexAttribPointer(uint index, int size, uint @type, bool normalized, int stride, nint pointer)
            => _VertexAttribPointer(index, size, @type, normalized, stride, pointer);

        // --------------------------------------------------------
        // Loader — called by GLLoader.LoadAll()
        // --------------------------------------------------------

        internal static void LoadFunctions(Func<string, nint> getProcAddress)
        {
            T Load<T>(string name) where T : Delegate
            {
                nint ptr = getProcAddress(name);
                if (ptr == nint.Zero)
                    throw new EntryPointNotFoundException($"OpenGL entry point '{name}' not found.");
                return Marshal.GetDelegateForFunctionPointer<T>(ptr);
            }

            _ActiveTexture = Load<d_ActiveTexture>("glActiveTexture");
            _AttachShader = Load<d_AttachShader>("glAttachShader");
            _BeginConditionalRender = Load<d_BeginConditionalRender>("glBeginConditionalRender");
            _BeginQuery = Load<d_BeginQuery>("glBeginQuery");
            _BeginTransformFeedback = Load<d_BeginTransformFeedback>("glBeginTransformFeedback");
            _BindAttribLocation = Load<d_BindAttribLocation>("glBindAttribLocation");
            _BindBuffer = Load<d_BindBuffer>("glBindBuffer");
            _BindBufferBase = Load<d_BindBufferBase>("glBindBufferBase");
            _BindBufferRange = Load<d_BindBufferRange>("glBindBufferRange");
            _BindFramebuffer = Load<d_BindFramebuffer>("glBindFramebuffer");
            _BindImageTexture = Load<d_BindImageTexture>("glBindImageTexture");
            _BindProgramPipeline = Load<d_BindProgramPipeline>("glBindProgramPipeline");
            _BindRenderbuffer = Load<d_BindRenderbuffer>("glBindRenderbuffer");
            _BindSampler = Load<d_BindSampler>("glBindSampler");
            _BindTexture = Load<d_BindTexture>("glBindTexture");
            _BindTextureUnit = Load<d_BindTextureUnit>("glBindTextureUnit");
            _BindTransformFeedback = Load<d_BindTransformFeedback>("glBindTransformFeedback");
            _BindVertexArray = Load<d_BindVertexArray>("glBindVertexArray");
            _BlendColour = Load<d_BlendColour>("glBlendColour");
            _BlendEquation = Load<d_BlendEquation>("glBlendEquation");
            _BlendFunc = Load<d_BlendFunc>("glBlendFunc");
            _BlendFuncSeparate = Load<d_BlendFuncSeparate>("glBlendFuncSeparate");
            _BlitFramebuffer = Load<d_BlitFramebuffer>("glBlitFramebuffer");
            _BufferData = Load<d_BufferData>("glBufferData");
            _BufferStorage = Load<d_BufferStorage>("glBufferStorage");
            _BufferSubData = Load<d_BufferSubData>("glBufferSubData");
            _CheckFramebufferStatus = Load<d_CheckFramebufferStatus>("glCheckFramebufferStatus");
            _Clear = Load<d_Clear>("glClear");
            _ClearBufferfi = Load<d_ClearBufferfi>("glClearBufferfi");
            _ClearBufferfv = Load<d_ClearBufferfv>("glClearBufferfv");
            _ClearBufferiv = Load<d_ClearBufferiv>("glClearBufferiv");
            _ClearBufferuiv = Load<d_ClearBufferuiv>("glClearBufferuiv");
            _ClearColour = Load<d_ClearColour>("glClearColour");
            _ClearTexImage = Load<d_ClearTexImage>("glClearTexImage");
            _ClientWaitSync = Load<d_ClientWaitSync>("glClientWaitSync");
            _ClipControl = Load<d_ClipControl>("glClipControl");
            _ColourMask = Load<d_ColourMask>("glColorMask");
            _CompileShader = Load<d_CompileShader>("glCompileShader");
            _CompressedTexImage2D = Load<d_CompressedTexImage2D>("glCompressedTexImage2D");
            _CopyBufferSubData = Load<d_CopyBufferSubData>("glCopyBufferSubData");
            _CopyImageSubData = Load<d_CopyImageSubData>("glCopyImageSubData");
            _CreateProgram = Load<d_CreateProgram>("glCreateProgram");
            _CreateShader = Load<d_CreateShader>("glCreateShader");
            _CullFace = Load<d_CullFace>("glCullFace");
            _DebugMessageCallback = Load<d_DebugMessageCallback>("glDebugMessageCallback");
            _DebugMessageControl = Load<d_DebugMessageControl>("glDebugMessageControl");
            _DebugMessageInsert = Load<d_DebugMessageInsert>("glDebugMessageInsert");
            _DeleteBuffers = Load<d_DeleteBuffers>("glDeleteBuffers");
            _DeleteFramebuffers = Load<d_DeleteFramebuffers>("glDeleteFramebuffers");
            _DeleteProgram = Load<d_DeleteProgram>("glDeleteProgram");
            _DeleteProgramPipelines = Load<d_DeleteProgramPipelines>("glDeleteProgramPipelines");
            _DeleteQueries = Load<d_DeleteQueries>("glDeleteQueries");
            _DeleteRenderbuffers = Load<d_DeleteRenderbuffers>("glDeleteRenderbuffers");
            _DeleteSamplers = Load<d_DeleteSamplers>("glDeleteSamplers");
            _DeleteShader = Load<d_DeleteShader>("glDeleteShader");
            _DeleteSync = Load<d_DeleteSync>("glDeleteSync");
            _DeleteTextures = Load<d_DeleteTextures>("glDeleteTextures");
            _DeleteTransformFeedbacks = Load<d_DeleteTransformFeedbacks>("glDeleteTransformFeedbacks");
            _DeleteVertexArrays = Load<d_DeleteVertexArrays>("glDeleteVertexArrays");
            _DepthFunc = Load<d_DepthFunc>("glDepthFunc");
            _DepthMask = Load<d_DepthMask>("glDepthMask");
            _DepthRange = Load<d_DepthRange>("glDepthRange");
            _DetachShader = Load<d_DetachShader>("glDetachShader");
            _Disable = Load<d_Disable>("glDisable");
            _DisableVertexAttribArray = Load<d_DisableVertexAttribArray>("glDisableVertexAttribArray");
            _DispatchCompute = Load<d_DispatchCompute>("glDispatchCompute");
            _DispatchComputeIndirect = Load<d_DispatchComputeIndirect>("glDispatchComputeIndirect");
            _DrawArrays = Load<d_DrawArrays>("glDrawArrays");
            _DrawArraysIndirect = Load<d_DrawArraysIndirect>("glDrawArraysIndirect");
            _DrawArraysInstanced = Load<d_DrawArraysInstanced>("glDrawArraysInstanced");
            _DrawBuffer = Load<d_DrawBuffer>("glDrawBuffer");
            _DrawBuffers = Load<d_DrawBuffers>("glDrawBuffers");
            _DrawElements = Load<d_DrawElements>("glDrawElements");
            _DrawElementsIndirect = Load<d_DrawElementsIndirect>("glDrawElementsIndirect");
            _DrawElementsInstanced = Load<d_DrawElementsInstanced>("glDrawElementsInstanced");
            _DrawRangeElements = Load<d_DrawRangeElements>("glDrawRangeElements");
            _DrawTransformFeedback = Load<d_DrawTransformFeedback>("glDrawTransformFeedback");
            _DrawTransformFeedbackInstanced = Load<d_DrawTransformFeedbackInstanced>("glDrawTransformFeedbackInstanced");
            _Enable = Load<d_Enable>("glEnable");
            _EnableVertexAttribArray = Load<d_EnableVertexAttribArray>("glEnableVertexAttribArray");
            _EndConditionalRender = Load<d_EndConditionalRender>("glEndConditionalRender");
            _EndQuery = Load<d_EndQuery>("glEndQuery");
            _EndTransformFeedback = Load<d_EndTransformFeedback>("glEndTransformFeedback");
            _FenceSync = Load<d_FenceSync>("glFenceSync");
            _Finish = Load<d_Finish>("glFinish");
            _Flush = Load<d_Flush>("glFlush");
            _FramebufferRenderbuffer = Load<d_FramebufferRenderbuffer>("glFramebufferRenderbuffer");
            _FramebufferTexture = Load<d_FramebufferTexture>("glFramebufferTexture");
            _FramebufferTexture2D = Load<d_FramebufferTexture2D>("glFramebufferTexture2D");
            _FramebufferTexture3D = Load<d_FramebufferTexture3D>("glFramebufferTexture3D");
            _FramebufferTextureLayer = Load<d_FramebufferTextureLayer>("glFramebufferTextureLayer");
            _FrontFace = Load<d_FrontFace>("glFrontFace");
            _GenBuffers = Load<d_GenBuffers>("glGenBuffers");
            _GenerateMipmap = Load<d_GenerateMipmap>("glGenerateMipmap");
            _GenFramebuffers = Load<d_GenFramebuffers>("glGenFramebuffers");
            _GenProgramPipelines = Load<d_GenProgramPipelines>("glGenProgramPipelines");
            _GenQueries = Load<d_GenQueries>("glGenQueries");
            _GenRenderbuffers = Load<d_GenRenderbuffers>("glGenRenderbuffers");
            _GenSamplers = Load<d_GenSamplers>("glGenSamplers");
            _GenTextures = Load<d_GenTextures>("glGenTextures");
            _GenTransformFeedbacks = Load<d_GenTransformFeedbacks>("glGenTransformFeedbacks");
            _GenVertexArrays = Load<d_GenVertexArrays>("glGenVertexArrays");
            _GetActiveAttrib = Load<d_GetActiveAttrib>("glGetActiveAttrib");
            _GetActiveUniform = Load<d_GetActiveUniform>("glGetActiveUniform");
            _GetAttribLocation = Load<d_GetAttribLocation>("glGetAttribLocation");
            _GetBooleanv = Load<d_GetBooleanv>("glGetBooleanv");
            _GetBufferParameteriv = Load<d_GetBufferParameteriv>("glGetBufferParameteriv");
            _GetBufferSubData = Load<d_GetBufferSubData>("glGetBufferSubData");
            _GetDebugMessageLog = Load<d_GetDebugMessageLog>("glGetDebugMessageLog");
            _GetDoublev = Load<d_GetDoublev>("glGetDoublev");
            _GetError = Load<d_GetError>("glGetError");
            _GetFloatv = Load<d_GetFloatv>("glGetFloatv");
            _GetFramebufferAttachmentParameteriv = Load<d_GetFramebufferAttachmentParameteriv>("glGetFramebufferAttachmentParameteriv");
            _GetIntegerv = Load<d_GetIntegerv>("glGetIntegerv");
            _GetObjectLabel = Load<d_GetObjectLabel>("glGetObjectLabel");
            _GetProgramBinary = Load<d_GetProgramBinary>("glGetProgramBinary");
            _GetProgramInfoLog = Load<d_GetProgramInfoLog>("glGetProgramInfoLog");
            _GetProgramiv = Load<d_GetProgramiv>("glGetProgramiv");
            _GetProgramPipelineInfoLog = Load<d_GetProgramPipelineInfoLog>("glGetProgramPipelineInfoLog");
            _GetProgramPipelineiv = Load<d_GetProgramPipelineiv>("glGetProgramPipelineiv");
            _GetProgramResourceIndex = Load<d_GetProgramResourceIndex>("glGetProgramResourceIndex");
            _GetProgramResourceiv = Load<d_GetProgramResourceiv>("glGetProgramResourceiv");
            _GetProgramResourceName = Load<d_GetProgramResourceName>("glGetProgramResourceName");
            _GetQueryiv = Load<d_GetQueryiv>("glGetQueryiv");
            _GetQueryObjecti64v = Load<d_GetQueryObjecti64v>("glGetQueryObjecti64v");
            _GetQueryObjectiv = Load<d_GetQueryObjectiv>("glGetQueryObjectiv");
            _GetQueryObjectui64v = Load<d_GetQueryObjectui64v>("glGetQueryObjectui64v");
            _GetQueryObjectuiv = Load<d_GetQueryObjectuiv>("glGetQueryObjectuiv");
            _GetShaderInfoLog = Load<d_GetShaderInfoLog>("glGetShaderInfoLog");
            _GetShaderiv = Load<d_GetShaderiv>("glGetShaderiv");
            _GetShaderSource = Load<d_GetShaderSource>("glGetShaderSource");
            _GetString = Load<d_GetString>("glGetString");
            _GetStringi = Load<d_GetStringi>("glGetStringi");
            _GetSynciv = Load<d_GetSynciv>("glGetSynciv");
            _GetTexImage = Load<d_GetTexImage>("glGetTexImage");
            _GetTexLevelParameteriv = Load<d_GetTexLevelParameteriv>("glGetTexLevelParameteriv");
            _GetUniformBlockIndex = Load<d_GetUniformBlockIndex>("glGetUniformBlockIndex");
            _GetUniformLocation = Load<d_GetUniformLocation>("glGetUniformLocation");
            _GetVertexAttribfv = Load<d_GetVertexAttribfv>("glGetVertexAttribfv");
            _GetVertexAttribiv = Load<d_GetVertexAttribiv>("glGetVertexAttribiv");
            _GetVertexAttribPointerv = Load<d_GetVertexAttribPointerv>("glGetVertexAttribPointerv");
            _Hint = Load<d_Hint>("glHint");
            _InvalidateFramebuffer = Load<d_InvalidateFramebuffer>("glInvalidateFramebuffer");
            _InvalidateTexImage = Load<d_InvalidateTexImage>("glInvalidateTexImage");
            _IsBuffer = Load<d_IsBuffer>("glIsBuffer");
            _IsEnabled = Load<d_IsEnabled>("glIsEnabled");
            _IsFramebuffer = Load<d_IsFramebuffer>("glIsFramebuffer");
            _IsProgram = Load<d_IsProgram>("glIsProgram");
            _IsProgramPipeline = Load<d_IsProgramPipeline>("glIsProgramPipeline");
            _IsQuery = Load<d_IsQuery>("glIsQuery");
            _IsRenderbuffer = Load<d_IsRenderbuffer>("glIsRenderbuffer");
            _IsSampler = Load<d_IsSampler>("glIsSampler");
            _IsShader = Load<d_IsShader>("glIsShader");
            _IsSync = Load<d_IsSync>("glIsSync");
            _IsTexture = Load<d_IsTexture>("glIsTexture");
            _IsVertexArray = Load<d_IsVertexArray>("glIsVertexArray");
            _LineWidth = Load<d_LineWidth>("glLineWidth");
            _LinkProgram = Load<d_LinkProgram>("glLinkProgram");
            _LogicOp = Load<d_LogicOp>("glLogicOp");
            _MapBuffer = Load<d_MapBuffer>("glMapBuffer");
            _MapBufferRange = Load<d_MapBufferRange>("glMapBufferRange");
            _MemoryBarrier = Load<d_MemoryBarrier>("glMemoryBarrier");
            _MultiDrawArrays = Load<d_MultiDrawArrays>("glMultiDrawArrays");
            _MultiDrawElements = Load<d_MultiDrawElements>("glMultiDrawElements");
            _ObjectLabel = Load<d_ObjectLabel>("glObjectLabel");
            _PauseTransformFeedback = Load<d_PauseTransformFeedback>("glPauseTransformFeedback");
            _PointSize = Load<d_PointSize>("glPointSize");
            _PolygonMode = Load<d_PolygonMode>("glPolygonMode");
            _PolygonOffset = Load<d_PolygonOffset>("glPolygonOffset");
            _PopDebugGroup = Load<d_PopDebugGroup>("glPopDebugGroup");
            _ProgramBinary = Load<d_ProgramBinary>("glProgramBinary");
            _ProgramParameteri = Load<d_ProgramParameteri>("glProgramParameteri");
            _PushDebugGroup = Load<d_PushDebugGroup>("glPushDebugGroup");
            _QueryCounter = Load<d_QueryCounter>("glQueryCounter");
            _ReadBuffer = Load<d_ReadBuffer>("glReadBuffer");
            _ReadPixels = Load<d_ReadPixels>("glReadPixels");
            _RenderbufferStorage = Load<d_RenderbufferStorage>("glRenderbufferStorage");
            _RenderbufferStorageMultisample = Load<d_RenderbufferStorageMultisample>("glRenderbufferStorageMultisample");
            _ResumeTransformFeedback = Load<d_ResumeTransformFeedback>("glResumeTransformFeedback");
            _SamplerParameterf = Load<d_SamplerParameterf>("glSamplerParameterf");
            _SamplerParameterfv = Load<d_SamplerParameterfv>("glSamplerParameterfv");
            _SamplerParameteri = Load<d_SamplerParameteri>("glSamplerParameteri");
            _Scissor = Load<d_Scissor>("glScissor");
            _ShaderSource = Load<d_ShaderSource>("glShaderSource");
            _ShaderStorageBlockBinding = Load<d_ShaderStorageBlockBinding>("glShaderStorageBlockBinding");
            _SpecialiseShader = Load<d_SpecialiseShader>("glSpecializeShader");
            _StencilFunc = Load<d_StencilFunc>("glStencilFunc");
            _StencilMask = Load<d_StencilMask>("glStencilMask");
            _StencilOp = Load<d_StencilOp>("glStencilOp");
            _TexImage1D = Load<d_TexImage1D>("glTexImage1D");
            _TexImage2D = Load<d_TexImage2D>("glTexImage2D");
            _TexImage3D = Load<d_TexImage3D>("glTexImage3D");
            _TexParameterf = Load<d_TexParameterf>("glTexParameterf");
            _TexParameterfv = Load<d_TexParameterfv>("glTexParameterfv");
            _TexParameteri = Load<d_TexParameteri>("glTexParameteri");
            _TexParameteriv = Load<d_TexParameteriv>("glTexParameteriv");
            _TexStorage2D = Load<d_TexStorage2D>("glTexStorage2D");
            _TexStorage3D = Load<d_TexStorage3D>("glTexStorage3D");
            _TexSubImage2D = Load<d_TexSubImage2D>("glTexSubImage2D");
            _TexSubImage3D = Load<d_TexSubImage3D>("glTexSubImage3D");
            _TransformFeedbackVaryings = Load<d_TransformFeedbackVaryings>("glTransformFeedbackVaryings");
            _Uniform1d = Load<d_Uniform1d>("glUniform1d");
            _Uniform1f = Load<d_Uniform1f>("glUniform1f");
            _Uniform1fv = Load<d_Uniform1fv>("glUniform1fv");
            _Uniform1i = Load<d_Uniform1i>("glUniform1i");
            _Uniform1iv = Load<d_Uniform1iv>("glUniform1iv");
            _Uniform1ui = Load<d_Uniform1ui>("glUniform1ui");
            _Uniform2d = Load<d_Uniform2d>("glUniform2d");
            _Uniform2f = Load<d_Uniform2f>("glUniform2f");
            _Uniform2fv = Load<d_Uniform2fv>("glUniform2fv");
            _Uniform2i = Load<d_Uniform2i>("glUniform2i");
            _Uniform2iv = Load<d_Uniform2iv>("glUniform2iv");
            _Uniform2ui = Load<d_Uniform2ui>("glUniform2ui");
            _Uniform3d = Load<d_Uniform3d>("glUniform3d");
            _Uniform3f = Load<d_Uniform3f>("glUniform3f");
            _Uniform3fv = Load<d_Uniform3fv>("glUniform3fv");
            _Uniform3i = Load<d_Uniform3i>("glUniform3i");
            _Uniform3iv = Load<d_Uniform3iv>("glUniform3iv");
            _Uniform3ui = Load<d_Uniform3ui>("glUniform3ui");
            _Uniform4d = Load<d_Uniform4d>("glUniform4d");
            _Uniform4f = Load<d_Uniform4f>("glUniform4f");
            _Uniform4fv = Load<d_Uniform4fv>("glUniform4fv");
            _Uniform4i = Load<d_Uniform4i>("glUniform4i");
            _Uniform4iv = Load<d_Uniform4iv>("glUniform4iv");
            _Uniform4ui = Load<d_Uniform4ui>("glUniform4ui");
            _UniformBlockBinding = Load<d_UniformBlockBinding>("glUniformBlockBinding");
            _UniformMatrix2fv = Load<d_UniformMatrix2fv>("glUniformMatrix2fv");
            _UniformMatrix2x3fv = Load<d_UniformMatrix2x3fv>("glUniformMatrix2x3fv");
            _UniformMatrix2x4fv = Load<d_UniformMatrix2x4fv>("glUniformMatrix2x4fv");
            _UniformMatrix3fv = Load<d_UniformMatrix3fv>("glUniformMatrix3fv");
            _UniformMatrix3x2fv = Load<d_UniformMatrix3x2fv>("glUniformMatrix3x2fv");
            _UniformMatrix3x4fv = Load<d_UniformMatrix3x4fv>("glUniformMatrix3x4fv");
            _UniformMatrix4fv = Load<d_UniformMatrix4fv>("glUniformMatrix4fv");
            _UniformMatrix4x2fv = Load<d_UniformMatrix4x2fv>("glUniformMatrix4x2fv");
            _UniformMatrix4x3fv = Load<d_UniformMatrix4x3fv>("glUniformMatrix4x3fv");
            _UnmapBuffer = Load<d_UnmapBuffer>("glUnmapBuffer");
            _UseProgram = Load<d_UseProgram>("glUseProgram");
            _UseProgramStages = Load<d_UseProgramStages>("glUseProgramStages");
            _ValidateProgram = Load<d_ValidateProgram>("glValidateProgram");
            _ValidateProgramPipeline = Load<d_ValidateProgramPipeline>("glValidateProgramPipeline");
            _VertexAttrib1f = Load<d_VertexAttrib1f>("glVertexAttrib1f");
            _VertexAttrib2f = Load<d_VertexAttrib2f>("glVertexAttrib2f");
            _VertexAttrib3f = Load<d_VertexAttrib3f>("glVertexAttrib3f");
            _VertexAttrib4f = Load<d_VertexAttrib4f>("glVertexAttrib4f");
            _VertexAttribDivisor = Load<d_VertexAttribDivisor>("glVertexAttribDivisor");
            _VertexAttribIPointer = Load<d_VertexAttribIPointer>("glVertexAttribIPointer");
            _VertexAttribLPointer = Load<d_VertexAttribLPointer>("glVertexAttribLPointer");
            _VertexAttribPointer = Load<d_VertexAttribPointer>("glVertexAttribPointer");
            _Viewport = Load<d_Viewport>("glViewport");
            _WaitSync = Load<d_WaitSync>("glWaitSync");
        }
    }
}
