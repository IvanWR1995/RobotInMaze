using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    static class SimpleMazeFactory
    {
        public enum TypeMaze
        {
            DefaultMaze,
            EulerMaze
        }
        public static Maze Create(TypeMaze type, int width = 7, int height = 7)
        {
            switch (type)
            {
                case TypeMaze.DefaultMaze:
                    return new MazeDefault();
                case TypeMaze.EulerMaze:
                    return new MazeEuler(width, height);
            }
            return null;
        }
    }
}
