using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    abstract class Maze:IMaze,IPrint
    {
        protected Cell[,] MazeCells;
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public virtual  Cell GetCellMaze(int x, int y)
        {
            return MazeCells[y, x];
        }
        public virtual void Print(Char Wall, Char Emplty, Char Exit)
        {
            for (int i = 0; i != Height; i++)
            {
                for (int j = 0; j != Width; j++)
                {
                    switch (MazeCells[i, j].Type)
                    {
                        case CellType.Empty:
                            Console.Write(Emplty);
                            break;
                        case CellType.Exit:
                            Console.Write(Exit);
                            break;
                        case CellType.Wall:
                            Console.Write(Wall);
                            break;

                    }
                }
                Console.WriteLine();
            }
        }
        public virtual void Print(Char Wall, Char Emplty, Char Exit, Char Robot, int RobotX, int RobotY)
        {
            for (int i = 0; i != Height; i++)
            {
                for (int j = 0; j != Width; j++)
                {
                    switch (MazeCells[i, j].Type)
                    {
                        case CellType.Empty:
                            if (RobotX == j && RobotY == i)
                                Console.Write(Robot);
                            else
                                Console.Write(Emplty);
                            break;
                        case CellType.Exit:
                            Console.Write(Exit);
                            break;
                        case CellType.Wall:
                            Console.Write(Wall);
                            break;

                    }
                }
                Console.WriteLine();
            }
        }
    }
}
