using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    class Robot : IRobot
    {
        Maze CurrentMaze;
        int x = 0;
        int y = 0;
        public Robot(Maze maze)
        {
            CurrentMaze = maze;
            Random rand = new Random();
            bool IsRobotCreate = false;
            while (!IsRobotCreate)
            {
                x = rand.Next(1, maze.Width - 1);
                y = rand.Next(1, maze.Height - 1);
                if (maze.GetCellMaze(x, y).Type == CellType.Empty)
                    IsRobotCreate = true;

            }


        }
        public Robot(Maze maze, int x, int y)
        {
            CurrentMaze = maze;
            this.x = x;
            this.y = y;


        }

        public Cell CurrentCell
        {
            get { return CurrentMaze.GetCellMaze(x, y); }
        }
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    x -= 1;
                    CurrentMaze.Print('#', ' ', 'E', 'R', x, y);
                    return;
                case Direction.Right:
                    x += 1;
                    CurrentMaze.Print('#', ' ', 'E', 'R', x, y);
                    return;
                case Direction.Up:
                    y += 1;
                    CurrentMaze.Print('#', ' ', 'E', 'R', x, y);
                    return;
                case Direction.Down:
                    y -= 1;
                    CurrentMaze.Print('#', ' ', 'E', 'R', x, y);
                    return;
            }
        }
        public Cell AdjacentCell(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return CurrentMaze.GetCellMaze(x - 1, y);
                case Direction.Right:
                    return CurrentMaze.GetCellMaze(x + 1, y);
                case Direction.Up:
                    return CurrentMaze.GetCellMaze(x, y + 1);
                case Direction.Down:
                    return CurrentMaze.GetCellMaze(x, y - 1);
            }
            return null;
        }
    }

}
