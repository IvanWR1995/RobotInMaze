using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    class ExceptionAlgoritmFail:Exception
    {
        public ExceptionAlgoritmFail(string Message)
            : base(Message)
        { }
    }
}
