using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib
{
    public class BlockException : Exception
    {
        public BlockException(string message)
            : base(message) { }
    }
}
