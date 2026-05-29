// auto-generated, do not edit.

using System;

namespace Anatta.Framework.Graphics.OpenGL
{
    [Flags]
    public enum ClearBufferMask : uint
    {
        /// <summary>GL_DEPTH_BUFFER_BIT</summary>
        DepthBufferBit = 0x0100,
        /// <summary>GL_STENCIL_BUFFER_BIT</summary>
        StencilBufferBit = 0x0400,
        /// <summary>GL_COLOR_BUFFER_BIT</summary>
        ColourBufferBit = 0x4000,
    }

    public enum BufferTarget : uint
    {
        ArrayBuffer = 0x8892,
        ElementArrayBuffer = 0x8893,
        CopyReadBuffer = 0x8F36,
        CopyWriteBuffer = 0x8F37,
        DrawIndirectBuffer = 0x8F3F,
        DispatchIndirectBuffer = 0x90EE,
        PixelPackBuffer = 0x88EB,
        PixelUnpackBuffer = 0x88EC,
        QueryBuffer = 0x9192,
        ShaderStorageBuffer = 0x90D2,
        TextureBuffer = 0x8C2A,
        TransformFeedbackBuffer = 0x8C8E,
        UniformBuffer = 0x8A11,
        AtomicCounterBuffer = 0x92C0,
        ParameterBuffer = 0x80EE,
    }

    public enum BufferUsage : uint
    {
        StreamDraw = 0x88E0,
        StreamRead = 0x88E1,
        StreamCopy = 0x88E2,
        StaticDraw = 0x88E4,
        StaticRead = 0x88E5,
        StaticCopy = 0x88E6,
        DynamicDraw = 0x88E8,
        DynamicRead = 0x88E9,
        DynamicCopy = 0x88EA,
    }

    [Flags]
    public enum BufferStorageFlags : uint
    {
        None = 0x0000,
        DynamicStorageBit = 0x0100,
        MapReadBit = 0x0001,
        MapWriteBit = 0x0002,
        MapPersistentBit = 0x0040,
        MapCoherentBit = 0x0080,
        ClientStorageBit = 0x0200,
    }

    [Flags]
    public enum MapAccessFlags : uint
    {
        ReadBit = 0x0001,
        WriteBit = 0x0002,
        InvalidateRangeBit = 0x0004,
        InvalidateBufferBit = 0x0008,
        FlushExplicitBit = 0x0010,
        UnsynchronisedBit = 0x0020,
        PersistentBit = 0x0040,
        CoherentBit = 0x0080,
    }

    public enum TextureTarget : uint
    {
        Texture1D = 0x0DE0,
        Texture2D = 0x0DE1,
        Texture3D = 0x806F,
        Texture1DArray = 0x8C18,
        Texture2DArray = 0x8C1A,
        TextureRectangle = 0x84F5,
        TextureCubeMap = 0x8513,
        TextureCubeMapArray = 0x9009,
        Texture2DMultisample = 0x9100,
        Texture2DMultisampleArray = 0x9102,
        TextureBuffer = 0x8C2A,
        TextureCubeMapPositiveX = 0x8515,
        TextureCubeMapNegativeX = 0x8516,
        TextureCubeMapPositiveY = 0x8517,
        TextureCubeMapNegativeY = 0x8518,
        TextureCubeMapPositiveZ = 0x8519,
        TextureCubeMapNegativeZ = 0x851A,
    }

    public enum TextureParameterName : uint
    {
        TextureMinFilter = 0x2801,
        TextureMagFilter = 0x2800,
        TextureWrapS = 0x2802,
        TextureWrapT = 0x2803,
        TextureWrapR = 0x8072,
        TextureBorderColour = 0x1004,
        TextureMinLod = 0x813A,
        TextureMaxLod = 0x813B,
        TextureBaseLevel = 0x813C,
        TextureMaxLevel = 0x813D,
        TextureLodBias = 0x8501,
        TextureCompareMode = 0x884C,
        TextureCompareFunc = 0x884D,
        TextureMaxAnisotropy = 0x84FE,
    }

    public enum TextureFilter : uint
    {
        Nearest = 0x2600,
        Linear = 0x2601,
        NearestMipmapNearest = 0x2700,
        LinearMipmapNearest = 0x2701,
        NearestMipmapLinear = 0x2702,
        LinearMipmapLinear = 0x2703,
    }

    public enum TextureWrapMode : uint
    {
        Repeat = 0x2901,
        ClampToEdge = 0x812F,
        ClampToBorder = 0x812D,
        MirroredRepeat = 0x8370,
        MirrorClampToEdge = 0x8743,
    }

    public enum InternalFormat : uint
    {
        R8 = 0x8229,
        R8Snorm = 0x8F94,
        R16 = 0x822A,
        R16Snorm = 0x8F98,
        Rg8 = 0x822B,
        Rg8Snorm = 0x8F95,
        Rg16 = 0x822C,
        Rg16Snorm = 0x8F99,
        R3G3B2 = 0x2A10,
        Rgb4 = 0x804F,
        Rgb5 = 0x8050,
        Rgb565 = 0x8D62,
        Rgb8 = 0x8051,
        Rgb8Snorm = 0x8F96,
        Rgb10 = 0x8052,
        Rgb12 = 0x8053,
        Rgb16 = 0x8054,
        Rgb16Snorm = 0x8F9A,
        Rgba2 = 0x8055,
        Rgba4 = 0x8056,
        Rgb5A1 = 0x8057,
        Rgba8 = 0x8058,
        Rgba8Snorm = 0x8F97,
        Rgb10A2 = 0x8059,
        Rgb10A2ui = 0x906F,
        Rgba12 = 0x805A,
        Rgba16 = 0x805B,
        Rgba16Snorm = 0x8F9B,
        Srgb8 = 0x8C41,
        Srgb8Alpha8 = 0x8C43,
        R16f = 0x822D,
        Rg16f = 0x822F,
        Rgb16f = 0x881B,
        Rgba16f = 0x881A,
        R32f = 0x822E,
        Rg32f = 0x8230,
        Rgb32f = 0x8815,
        Rgba32f = 0x8814,
        R11fG11fB10f = 0x8C3A,
        Rgb9E5 = 0x8C3D,
        R8i = 0x8231,
        R8ui = 0x8232,
        R16i = 0x8233,
        R16ui = 0x8234,
        R32i = 0x8235,
        R32ui = 0x8236,
        Rg8i = 0x8237,
        Rg8ui = 0x8238,
        Rg16i = 0x8239,
        Rg16ui = 0x823A,
        Rg32i = 0x823B,
        Rg32ui = 0x823C,
        Rgb8i = 0x8D8F,
        Rgb8ui = 0x8D7D,
        Rgb16i = 0x8D89,
        Rgb16ui = 0x8D77,
        Rgb32i = 0x8D83,
        Rgb32ui = 0x8D71,
        Rgba8i = 0x8D8E,
        Rgba8ui = 0x8D7C,
        Rgba16i = 0x8D88,
        Rgba16ui = 0x8D76,
        Rgba32i = 0x8D82,
        Rgba32ui = 0x8D70,
        DepthComponent16 = 0x81A5,
        DepthComponent24 = 0x81A6,
        DepthComponent32 = 0x81A7,
        DepthComponent32f = 0x8CAC,
        Depth24Stencil8 = 0x88F0,
        Depth32fStencil8 = 0x8CAD,
        StencilIndex8 = 0x8D48,
    }

    public enum PixelFormat : uint
    {
        Red = 0x1903,
        Rg = 0x8227,
        Rgb = 0x1907,
        Rgba = 0x1908,
        Bgr = 0x80E0,
        Bgra = 0x80E1,
        RedInteger = 0x8D94,
        RgInteger = 0x8228,
        RgbInteger = 0x8D98,
        RgbaInteger = 0x8D99,
        BgrInteger = 0x8D9A,
        BgraInteger = 0x8D9B,
        StencilIndex = 0x1901,
        DepthComponent = 0x1902,
        DepthStencil = 0x84F9,
    }

    public enum PixelType : uint
    {
        UnsignedByte = 0x1401,
        Byte = 0x1400,
        UnsignedShort = 0x1403,
        Short = 0x1402,
        UnsignedInt = 0x1405,
        Int = 0x1404,
        HalfFloat = 0x140B,
        Float = 0x1406,
        UnsignedByte332 = 0x8032,
        UnsignedByte233Rev = 0x8362,
        UnsignedShort565 = 0x8363,
        UnsignedShort565Rev = 0x8364,
        UnsignedShort4444 = 0x8033,
        UnsignedShort4444Rev = 0x8365,
        UnsignedShort5551 = 0x8034,
        UnsignedShort1555Rev = 0x8366,
        UnsignedInt8888 = 0x8035,
        UnsignedInt8888Rev = 0x8367,
        UnsignedInt1010102 = 0x8036,
        UnsignedInt2101010Rev = 0x8368,
        UnsignedInt248 = 0x84FA,
        UnsignedInt10f11f11fRev = 0x8C3B,
        UnsignedInt5999Rev = 0x8C3E,
        Float32UnsignedInt248Rev = 0x8DAD,
    }

    public enum ShaderType : uint
    {
        FragmentShader = 0x8B30,
        VertexShader = 0x8B31,
        GeometryShader = 0x8DD9,
        TessControlShader = 0x8E88,
        TessEvaluationShader = 0x8E87,
        ComputeShader = 0x91B9,
    }

    [Flags]
    public enum ProgramStageMask : uint
    {
        VertexShaderBit = 0x0001,
        FragmentShaderBit = 0x0002,
        GeometryShaderBit = 0x0004,
        TessControlShaderBit = 0x0008,
        TessEvaluationShaderBit = 0x0010,
        ComputeShaderBit = 0x0020,
        AllShaderBits = 0xFFFFFFFF,
    }

    public enum PrimitiveType : uint
    {
        Points = 0x0000,
        Lines = 0x0001,
        LineLoop = 0x0002,
        LineStrip = 0x0003,
        Triangles = 0x0004,
        TriangleStrip = 0x0005,
        TriangleFan = 0x0006,
        Quads = 0x0007,
        LinesAdjacency = 0x000A,
        LineStripAdjacency = 0x000B,
        TrianglesAdjacency = 0x000C,
        TriangleStripAdjacency = 0x000D,
        Patches = 0x000E,
    }

    public enum DrawElementsType : uint
    {
        UnsignedByte = 0x1401,
        UnsignedShort = 0x1403,
        UnsignedInt = 0x1405,
    }

    public enum EnableCap : uint
    {
        Blend = 0x0BE2,
        ClipDistance0 = 0x3000,
        ClipDistance1 = 0x3001,
        ClipDistance2 = 0x3002,
        ClipDistance3 = 0x3003,
        ClipDistance4 = 0x3004,
        ClipDistance5 = 0x3005,
        ClipDistance6 = 0x3006,
        ClipDistance7 = 0x3007,
        ColourLogicOp = 0x0BF2,
        CullFace = 0x0B44,
        DebugOutput = 0x92E0,
        DebugOutputSynchronous = 0x8242,
        DepthClamp = 0x864F,
        DepthTest = 0x0B71,
        Dither = 0x0BD0,
        FramebufferSrgb = 0x8DB9,
        LineSmooth = 0x0B20,
        Multisample = 0x809D,
        PolygonOffsetFill = 0x8037,
        PolygonOffsetLine = 0x2A02,
        PolygonOffsetPoint = 0x2A01,
        PolygonSmooth = 0x0B41,
        PrimitiveRestart = 0x8F9D,
        PrimitiveRestartFixedIndex = 0x8D69,
        RasterizerDiscard = 0x8C89,
        SampleAlphaToCoverage = 0x809E,
        SampleAlphaToOne = 0x809F,
        SampleCoverage = 0x80A0,
        SampleShading = 0x8C36,
        SampleMask = 0x8E51,
        ScissorTest = 0x0C11,
        StencilTest = 0x0B90,
        TextureCubeMapSeamless = 0x884F,
        ProgramPointSize = 0x8642,
    }

    public enum ErrorCode : uint
    {
        NoError = 0x0000,
        InvalidEnum = 0x0500,
        InvalidValue = 0x0501,
        InvalidOperation = 0x0502,
        StackOverflow = 0x0503,
        StackUnderflow = 0x0504,
        OutOfMemory = 0x0505,
        InvalidFramebufferOperation = 0x0506,
        ContextLost = 0x0507,
    }

    public enum DepthFunction : uint
    {
        Never = 0x0200,
        Less = 0x0201,
        Equal = 0x0202,
        LessEqual = 0x0203,
        Greater = 0x0204,
        NotEqual = 0x0205,
        GreaterEqual = 0x0206,
        Always = 0x0207,
    }

    public enum BlendFactor : uint
    {
        Zero = 0x0000,
        One = 0x0001,
        SrcColour = 0x0300,
        OneMinusSrcColour = 0x0301,
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305,
        DstColour = 0x0306,
        OneMinusDstColour = 0x0307,
        SrcAlphaSaturate = 0x0308,
        ConstantColour = 0x8001,
        OneMinusConstantColour = 0x8002,
        ConstantAlpha = 0x8003,
        OneMinusConstantAlpha = 0x8004,
        Src1Alpha = 0x8589,
        Src1Colour = 0x88F9,
        OneMinusSrc1Colour = 0x88FA,
        OneMinusSrc1Alpha = 0x88FB,
    }

    public enum BlendEquationMode : uint
    {
        FuncAdd = 0x8006,
        FuncSubtract = 0x800A,
        FuncReverseSubtract = 0x800B,
        Min = 0x8007,
        Max = 0x8008,
    }

    public enum CullFaceMode : uint
    {
        Front = 0x0404,
        Back = 0x0405,
        FrontAndBack = 0x0408,
    }

    public enum FrontFaceDirection : uint
    {
        Clockwise = 0x0900,
        CounterClockwise = 0x0901,
    }

    public enum PolygonModeEnum : uint
    {
        Point = 0x1B00,
        Line = 0x1B01,
        Fill = 0x1B02,
    }

    public enum FramebufferTarget : uint
    {
        ReadFramebuffer = 0x8CA8,
        DrawFramebuffer = 0x8CA9,
        Framebuffer = 0x8D40,
    }

    public enum FramebufferAttachment : uint
    {
        DepthStencilAttachment = 0x821A,
        ColourAttachment0 = 0x8CE0,
        ColourAttachment1 = 0x8CE1,
        ColourAttachment2 = 0x8CE2,
        ColourAttachment3 = 0x8CE3,
        ColourAttachment4 = 0x8CE4,
        ColourAttachment5 = 0x8CE5,
        ColourAttachment6 = 0x8CE6,
        ColourAttachment7 = 0x8CE7,
        ColourAttachment8 = 0x8CE8,
        ColourAttachment9 = 0x8CE9,
        ColourAttachment10 = 0x8CEA,
        ColourAttachment11 = 0x8CEB,
        ColourAttachment12 = 0x8CEC,
        ColourAttachment13 = 0x8CED,
        ColourAttachment14 = 0x8CEE,
        ColourAttachment15 = 0x8CEF,
        DepthAttachment = 0x8D00,
        StencilAttachment = 0x8D20,
    }

    public enum FramebufferStatus : uint
    {
        Complete = 0x8CD5,
        IncompleteAttachment = 0x8CD6,
        IncompleteMissingAttachment = 0x8CD7,
        IncompleteDrawBuffer = 0x8CDB,
        IncompleteReadBuffer = 0x8CDC,
        Unsupported = 0x8CDD,
        IncompleteMultisample = 0x8D56,
        IncompleteLayerTargets = 0x8DA8,
    }

    public enum QueryTarget : uint
    {
        SamplesPassed = 0x8914,
        AnySamplesPassed = 0x8C2F,
        AnySamplesPassedConservative = 0x8D6A,
        PrimitivesGenerated = 0x8C87,
        TransformFeedbackPrimitivesWritten = 0x8C88,
        TimeElapsed = 0x88BF,
        Timestamp = 0x8E28,
    }

    public enum SyncCondition : uint
    {
        SyncGpuCommandsComplete = 0x9117,
    }

    [Flags]
    public enum SyncWaitFlags : uint
    {
        None = 0x0000,
        SyncFlushCommandsBit = 0x0001,
    }

    public enum ClientWaitSyncResult : uint
    {
        AlreadySignalled = 0x911A,
        TimeoutExpired = 0x911B,
        ConditionSatisfied = 0x911C,
        WaitFailed = 0x911D,
    }

    public enum DebugSource : uint
    {
        Api = 0x8246,
        WindowSystem = 0x8247,
        ShaderCompiler = 0x8248,
        ThirdParty = 0x8249,
        Application = 0x824A,
        Other = 0x824B,
        DontCare = 0x1100,
    }

    public enum DebugType : uint
    {
        Error = 0x824C,
        DeprecatedBehaviour = 0x824D,
        UndefinedBehaviour = 0x824E,
        Portability = 0x824F,
        Performance = 0x8250,
        Marker = 0x8268,
        PushGroup = 0x8269,
        PopGroup = 0x826A,
        Other = 0x8251,
        DontCare = 0x1100,
    }

    public enum DebugSeverity : uint
    {
        High = 0x9146,
        Medium = 0x9147,
        Low = 0x9148,
        Notification = 0x826B,
        DontCare = 0x1100,
    }

    public enum ObjectIdentifier : uint
    {
        Buffer = 0x82E0,
        Shader = 0x82E1,
        Program = 0x82E2,
        VertexArray = 0x8074,
        Query = 0x82E3,
        ProgramPipeline = 0x82E4,
        TransformFeedback = 0x8E22,
        Sampler = 0x82E6,
        Texture = 0x1702,
        Renderbuffer = 0x8D41,
        Framebuffer = 0x8D40,
    }

    [Flags]
    public enum MemoryBarrierMask : uint
    {
        VertexAttribArrayBarrierBit = 0x0001,
        ElementArrayBarrierBit = 0x0002,
        UniformBarrierBit = 0x0004,
        TextureFetchBarrierBit = 0x0008,
        ShaderImageAccessBarrierBit = 0x0020,
        CommandBarrierBit = 0x0040,
        PixelBufferBarrierBit = 0x0080,
        TextureUpdateBarrierBit = 0x0100,
        BufferUpdateBarrierBit = 0x0200,
        FramebufferBarrierBit = 0x0400,
        TransformFeedbackBarrierBit = 0x0800,
        AtomicCounterBarrierBit = 0x1000,
        ShaderStorageBarrierBit = 0x2000,
        ClientMappedBufferBarrierBit = 0x4000,
        QueryBufferBarrierBit = 0x8000,
        AllBarrierBits = 0xFFFFFFFF,
    }

    public enum VertexAttribType : uint
    {
        Byte = 0x1400,
        UnsignedByte = 0x1401,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        Int = 0x1404,
        UnsignedInt = 0x1405,
        Float = 0x1406,
        Double = 0x140A,
        HalfFloat = 0x140B,
        Fixed = 0x140C,
        Int2101010Rev = 0x8D9F,
        UnsignedInt2101010Rev = 0x8368,
        UnsignedInt10f11f11fRev = 0x8C3B,
    }

    public enum TextureUnit : uint
    {
        Texture0 = 0x84C0,
        Texture1 = 0x84C1,
        Texture2 = 0x84C2,
        Texture3 = 0x84C3,
        Texture4 = 0x84C4,
        Texture5 = 0x84C5,
        Texture6 = 0x84C6,
        Texture7 = 0x84C7,
        Texture8 = 0x84C8,
        Texture9 = 0x84C9,
        Texture10 = 0x84CA,
        Texture11 = 0x84CB,
        Texture12 = 0x84CC,
        Texture13 = 0x84CD,
        Texture14 = 0x84CE,
        Texture15 = 0x84CF,
        Texture16 = 0x84D0,
        Texture17 = 0x84D1,
        Texture18 = 0x84D2,
        Texture19 = 0x84D3,
        Texture20 = 0x84D4,
        Texture21 = 0x84D5,
        Texture22 = 0x84D6,
        Texture23 = 0x84D7,
        Texture24 = 0x84D8,
        Texture25 = 0x84D9,
        Texture26 = 0x84DA,
        Texture27 = 0x84DB,
        Texture28 = 0x84DC,
        Texture29 = 0x84DD,
        Texture30 = 0x84DE,
        Texture31 = 0x84DF,
    }

    public enum StringName : uint
    {
        Vendor = 0x1F00,
        Renderer = 0x1F01,
        Version = 0x1F02,
        Extensions = 0x1F03,
        ShadingLanguageVersion = 0x8B8C,
    }

    public enum GetPName : uint
    {
        MaxTextureSize = 0x0D33,
        MaxVertexAttribs = 0x8869,
        MaxVertexUniformComponents = 0x8B4A,
        MaxFragmentUniformComponents = 0x8B49,
        MaxTextureImageUnits = 0x8872,
        MaxVertexTextureImageUnits = 0x8B4C,
        MaxCombinedTextureImageUnits = 0x8B4D,
        MaxDrawBuffers = 0x8824,
        MaxColourAttachments = 0x8CDF,
        MaxSamples = 0x8D57,
        MaxComputeWorkGroupCount = 0x91BE,
        MaxComputeWorkGroupSize = 0x91BF,
        MaxComputeWorkGroupInvocations = 0x90EB,
        MajorVersion = 0x821B,
        MinorVersion = 0x821C,
        NumExtensions = 0x821D,
        ViewportBoundsRange = 0x825D,
        Viewport = 0x0BA2,
        ScissorBox = 0x0C10,
        LineWidth = 0x0B21,
        PointSize = 0x0B11,
        CurrentProgram = 0x8B8D,
        ArrayBufferBinding = 0x8894,
        ElementArrayBufferBinding = 0x8895,
        VertexArrayBinding = 0x85B5,
        FramebufferBinding = 0x8CA6,
        RenderbufferBinding = 0x8CA7,
        MaxUniformBlockSize = 0x8A30,
        MaxShaderStorageBlockSize = 0x90DE,
        UniformBufferOffsetAlignment = 0x8A34,
        ShaderStorageBufferOffsetAlignment = 0x90DF,
    }

    public enum ImageAccess : uint
    {
        ReadOnly = 0x88B8,
        WriteOnly = 0x88B9,
        ReadWrite = 0x88BA,
    }

    public enum HintTarget : uint
    {
        LineSmoothHint = 0x0C52,
        PolygonSmoothHint = 0x0C53,
        TextureCompressionHint = 0x84EF,
        FragmentShaderDerivativeHint = 0x8B8B,
    }

    public enum HintMode : uint
    {
        DontCare = 0x1100,
        Fastest = 0x1101,
        Nicest = 0x1102,
    }

    public enum LogicOp : uint
    {
        Clear = 0x1500,
        And = 0x1501,
        AndReverse = 0x1502,
        Copy = 0x1503,
        AndInverted = 0x1504,
        Noop = 0x1505,
        Xor = 0x1506,
        Or = 0x1507,
        Nor = 0x1508,
        Equiv = 0x1509,
        Invert = 0x150A,
        OrReverse = 0x150B,
        CopyInverted = 0x150C,
        OrInverted = 0x150D,
        Nand = 0x150E,
        Set = 0x150F,
    }

    public enum StencilOp : uint
    {
        Keep = 0x1E00,
        Zero = 0x0000,
        Replace = 0x1E01,
        Incr = 0x1E02,
        Decr = 0x1E03,
        Invert = 0x150A,
        IncrWrap = 0x8507,
        DecrWrap = 0x8508,
    }

    public enum TransformFeedbackBufferMode : uint
    {
        InterleavedAttribs = 0x8C8C,
        SeparateAttribs = 0x8C8D,
    }

}
