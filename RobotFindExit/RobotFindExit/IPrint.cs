using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    interface IPrint
    {
        void Print(Char Wall, Char Emplty, Char Exit);
        void Print(Char Wall, Char Emplty, Char Exit, Char Robot, int RobotX, int RobotY);
    }
}
