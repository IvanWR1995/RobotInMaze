using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    interface IMaze
    {
        Cell GetCellMaze(int x, int y);
        
    }
}
