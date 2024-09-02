using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Lesson
{
    internal class OtusException : Exception
    {
        public OtusException(string message)
            : base(message) { }
    }
}
