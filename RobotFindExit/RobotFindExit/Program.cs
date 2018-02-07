using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze maze = SimpleMazeFactory.Create(SimpleMazeFactory.TypeMaze.EulerMaze, 7, 10);
            IRobot robot = new Robot(maze);
            try
            {
                PathFinder.FindExit(robot);
            }
            catch (ExceptionsExitNotExist exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ExceptionAlgoritmFail exception)
            {
                Console.WriteLine(exception.Message);
            }

        }
    }
}
