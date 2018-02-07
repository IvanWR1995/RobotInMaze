using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    class MazeDefault:Maze
    {
        

        public MazeDefault()
        {
            Width = 7;
            Height = 7;
            MazeCells = new Cell[7, 7];
            for (int i = 0; i != 7; i++)//top wall
                MazeCells[0, i] = new Cell(CellType.Wall);
            for (int i = 0; i != 7; i++)//left wall
                MazeCells[i, 0] = new Cell(CellType.Wall);
            for (int i = 0; i != 7; i++)// rigth wall
                MazeCells[i, 6] = new Cell(CellType.Wall);
            for (int i = 0; i != 7; i++)// bottom wall
                MazeCells[6, i] = new Cell(CellType.Wall);
            for (int i = 1; i != 6; i++)// 5 string
                MazeCells[5, i] = new Cell(CellType.Empty);
            for (int i = 1; i != 6; i++)// 1 string
                MazeCells[1, i] = new Cell(CellType.Empty);
            //4 string
            MazeCells[4, 1] = new Cell(CellType.Wall);
            MazeCells[4, 2] = new Cell(CellType.Wall);// Robot
            MazeCells[4, 3] = new Cell(CellType.Wall);
            MazeCells[4, 4] = new Cell(CellType.Wall);
            MazeCells[4, 5] = new Cell(CellType.Wall);
            //3 string
            MazeCells[3, 1] = new Cell(CellType.Wall);
            MazeCells[3, 2] = new Cell(CellType.Empty);
            MazeCells[3, 3] = new Cell(CellType.Wall);
            MazeCells[3, 4] = new Cell(CellType.Empty);
            MazeCells[3, 5] = new Cell(CellType.Empty);
            // 2 string
            MazeCells[2, 1] = new Cell(CellType.Empty);
            MazeCells[2, 2] = new Cell(CellType.Wall);
            MazeCells[2, 3] = new Cell(CellType.Empty);
            MazeCells[2, 4] = new Cell(CellType.Exit);//exit
            MazeCells[2, 5] = new Cell(CellType.Empty);
            
        }
       
    }
}
