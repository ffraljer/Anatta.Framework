// auto-generated, do not edit.


using System;
using System.Runtime.InteropServices;

namespace Anatta.Framework.Graphics.OpenGL
{
    public struct Viewport
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Viewport(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

    /// <summary>Defines a scissor rectangle (x, y, width, height).</summary>
    public struct Scissor
    {
        /// <summary>Left edge.</summary>
        public int X;
        /// <summary>Bottom edge.</summary>
        public int Y;
        /// <summary>Width in pixels.</summary>
        public int Width;
        /// <summary>Height in pixels.</summary>
        public int Height;

        public Scissor(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

    /// <summary>Parameters for glDrawArraysIndirect.</summary>
    public struct DrawArraysIndirectCommand
    {
        /// <summary>Number of vertices.</summary>
        public uint Count;
        /// <summary>Number of instances.</summary>
        public uint InstanceCount;
        /// <summary>Starting vertex index.</summary>
        public uint First;
        /// <summary>Starting instance index.</summary>
        public uint BaseInstance;

        public DrawArraysIndirectCommand(uint count, uint instanceCount, uint first, uint baseInstance)
        {
            Count = count;
            InstanceCount = instanceCount;
            First = first;
            BaseInstance = baseInstance;
        }
    }

    /// <summary>Parameters for glDrawElementsIndirect.</summary>
    public struct DrawElementsIndirectCommand
    {
        /// <summary>Number of indices.</summary>
        public uint Count;
        /// <summary>Number of instances.</summary>
        public uint InstanceCount;
        /// <summary>Starting index.</summary>
        public uint FirstIndex;
        /// <summary>Vertex offset added to each index.</summary>
        public int BaseVertex;
        /// <summary>Starting instance index.</summary>
        public uint BaseInstance;

        public DrawElementsIndirectCommand(uint count, uint instanceCount, uint firstIndex, int baseVertex, uint baseInstance)
        {
            Count = count;
            InstanceCount = instanceCount;
            FirstIndex = firstIndex;
            BaseVertex = baseVertex;
            BaseInstance = baseInstance;
        }
    }

    /// <summary>Parameters for glDispatchComputeIndirect.</summary>
    public struct DispatchIndirectCommand
    {
        /// <summary>Number of work groups in X.</summary>
        public uint NumGroupsX;
        /// <summary>Number of work groups in Y.</summary>
        public uint NumGroupsY;
        /// <summary>Number of work groups in Z.</summary>
        public uint NumGroupsZ;

        public DispatchIndirectCommand(uint numGroupsX, uint numGroupsY, uint numGroupsZ)
        {
            NumGroupsX = numGroupsX;
            NumGroupsY = numGroupsY;
            NumGroupsZ = numGroupsZ;
        }
    }

    /// <summary>Holds a debug message retrieved via glGetDebugMessageLog.</summary>
    public struct DebugMessage
    {
        /// <summary>Origin of the message.</summary>
        public DebugSource Source;
        /// <summary>Category of the message.</summary>
        public DebugType Type;
        /// <summary>Message identifier.</summary>
        public uint Id;
        /// <summary>Severity level.</summary>
        public DebugSeverity Severity;
        /// <summary>Human-readable description.</summary>
        public string Text;
    }

    /// <summary>Holds a parsed OpenGL version number.</summary>
    public struct GlVersion
    {
        /// <summary>Major version number (e.g. 4 for OpenGL 4.6).</summary>
        public int Major;
        /// <summary>Minor version number (e.g. 6 for OpenGL 4.6).</summary>
        public int Minor;

        public GlVersion(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
    }

    /// <summary>Per-channel write enable mask.</summary>
    public struct ColourMask
    {
        /// <summary>Enable writing to the red channel.</summary>
        public bool Red;
        /// <summary>Enable writing to the green channel.</summary>
        public bool Green;
        /// <summary>Enable writing to the blue channel.</summary>
        public bool Blue;
        /// <summary>Enable writing to the alpha channel.</summary>
        public bool Alpha;

        public ColourMask(bool red, bool green, bool blue, bool alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
    }

    /// <summary>Encapsulates all blending parameters.</summary>
    public struct BlendState
    {
        /// <summary>Source RGB blend factor.</summary>
        public BlendFactor SrcRgb;
        /// <summary>Destination RGB blend factor.</summary>
        public BlendFactor DstRgb;
        /// <summary>Source alpha blend factor.</summary>
        public BlendFactor SrcAlpha;
        /// <summary>Destination alpha blend factor.</summary>
        public BlendFactor DstAlpha;
        /// <summary>RGB blend equation.</summary>
        public BlendEquationMode RgbEquation;
        /// <summary>Alpha blend equation.</summary>
        public BlendEquationMode AlphaEquation;

        public BlendState(BlendFactor srcRgb, BlendFactor dstRgb, BlendFactor srcAlpha, BlendFactor dstAlpha, BlendEquationMode rgbEquation, BlendEquationMode alphaEquation)
        {
            SrcRgb = srcRgb;
            DstRgb = dstRgb;
            SrcAlpha = srcAlpha;
            DstAlpha = dstAlpha;
            RgbEquation = rgbEquation;
            AlphaEquation = alphaEquation;
        }
    }

    /// <summary>Metadata returned by glGetActiveUniform.</summary>
    public struct ActiveUniformInfo
    {
        /// <summary>Uniform variable name.</summary>
        public string Name;
        /// <summary>Array size (1 for non-arrays).</summary>
        public int Size;
        /// <summary>GL type token.</summary>
        public uint Type;
        /// <summary>Uniform location.</summary>
        public int Location;
    }

    /// <summary>Metadata returned by glGetActiveAttrib.</summary>
    public struct ActiveAttribInfo
    {
        /// <summary>Attribute variable name.</summary>
        public string Name;
        /// <summary>Array size (1 for non-arrays).</summary>
        public int Size;
        /// <summary>GL type token.</summary>
        public uint Type;
        /// <summary>Attribute location.</summary>
        public int Location;
    }

    /// <summary>Parameters queried from a renderbuffer object.</summary>
    public struct RenderbufferParameters
    {
        /// <summary>Width in pixels.</summary>
        public int Width;
        /// <summary>Height in pixels.</summary>
        public int Height;
        /// <summary>Internal pixel format.</summary>
        public uint InternalFormat;
        /// <summary>Bits per red component.</summary>
        public int RedSize;
        /// <summary>Bits per green component.</summary>
        public int GreenSize;
        /// <summary>Bits per blue component.</summary>
        public int BlueSize;
        /// <summary>Bits per alpha component.</summary>
        public int AlphaSize;
        /// <summary>Bits in depth component.</summary>
        public int DepthSize;
        /// <summary>Bits in stencil component.</summary>
        public int StencilSize;
        /// <summary>Number of MSAA samples.</summary>
        public int Samples;

        public RenderbufferParameters(int width, int height, uint internalFormat, int redSize, int greenSize, int blueSize, int alphaSize, int depthSize, int stencilSize, int samples)
        {
            Width = width;
            Height = height;
            InternalFormat = internalFormat;
            RedSize = redSize;
            GreenSize = greenSize;
            BlueSize = blueSize;
            AlphaSize = alphaSize;
            DepthSize = depthSize;
            StencilSize = stencilSize;
            Samples = samples;
        }
    }

}
