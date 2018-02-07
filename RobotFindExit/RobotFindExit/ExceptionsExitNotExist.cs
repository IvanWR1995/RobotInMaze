using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    class ExceptionsExitNotExist:Exception
    {
        public ExceptionsExitNotExist(string Message)
            : base(Message)
        { 
        }
    }
}
