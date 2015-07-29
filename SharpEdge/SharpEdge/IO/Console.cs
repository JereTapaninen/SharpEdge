using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpEdge.IO
{
    public static class Console
    {
        public static void Log(string message)
        {
            message = "[SharpEdge] " + message;
            Debug.WriteLine(message);
        }
    }
}
