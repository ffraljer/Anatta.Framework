using System;
using System.Collections.Generic;
using System.Text;

namespace Anatta.Framework.Graphics {
    public class FontFace {
        public byte[] Bytes { get; }
        public FontFace(byte[] bytes) => Bytes = bytes;
    }
}
