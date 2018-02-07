using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{
    class MazeEuler:Maze
    {
       
        public MazeEuler(int Width,int Height)
        {
            this.Width = Width;
            this.Height = Height;
            CellType[,] LocalMaze = new CellType[Height, Width];
            for (int i = 0; i != Width; i++)// создание верхней соенки
                LocalMaze[0, i] =CellType.Wall;
            for (int i = 0; i != Width; i++)// создание нижней стенки
                LocalMaze[Height - 1, i] = CellType.Wall;
            for (int i = 0; i != Height; i++)//создание левой стенки
                LocalMaze[i, 0] = CellType.Wall;
            for (int i = 0; i != Height; i++)// создание правой стенки
                LocalMaze[i, Width - 1] = CellType.Wall;
            int BeginWidthIndex = 1;
            int EndWidthIndex = Width - 1;
            int BeginHeightIndex = 1;
            int EndHeightIndex = Height - 1;
            Dictionary<int, int> NumberSet = new Dictionary<int, int>();
            Random rand;
            int CurrentString = BeginHeightIndex;
            while (CurrentString < EndHeightIndex -1)
            {
                for (int i = 1; i <= EndWidthIndex ; i += 2)//присвоение ячейкам номеров наборов
                {
                    if (!NumberSet.ContainsKey(i))
                    {
                        NumberSet.Add(i, i);
                       
                    }
                }
                for (int i = BeginWidthIndex; i < EndWidthIndex - 1; i += 2)// создание правой границы ячейки
                {
                    if (NumberSet[i] == NumberSet[i + 2] || IsRandTrue(i))
                    {
                        LocalMaze[CurrentString, i + 1] = CellType.Wall;
                    }
                    else
                        NumberSet[i + 2] = NumberSet[i];

                }
                //создание нижней границы ячейки
                int BorderString = CurrentString + 1;
                for (int i = BeginWidthIndex; i < EndWidthIndex - 1 ; i += 2)
                {
                    var c = NumberSet.Where(e => e.Value == NumberSet[i] && LocalMaze[CurrentString + 1, i] == CellType.Empty);
                    if (c.Count() > 1 && !IsCloseSpace(LocalMaze, CurrentString, i) && IsRandTrue(i))
                    {
                        if (i - 2 > 0 && LocalMaze[BorderString, i - 2] != CellType.Wall)
                        {
                            LocalMaze[BorderString, i] = CellType.Wall;
                            if (IsRandTrue(i+1))
                                LocalMaze[BorderString, i + 1] = CellType.Wall;
                        }
                        else if (i - 2 < 0)
                        {
                            LocalMaze[BorderString, i] = CellType.Wall;
                            if (IsRandTrue(i + 1))
                                LocalMaze[BorderString, i + 1] = CellType.Wall;
                        }   
                       
                    }
                }
                //создание новой строки
                int newString = CurrentString + 2;
                //создание последней строки лабиринта
                if (newString == EndHeightIndex - 2)
                {
                    for (int i = BeginWidthIndex; i < EndWidthIndex; i++)
                    {
                        LocalMaze[newString, i] = LocalMaze[CurrentString,i +1];
                    }
                    for (int i = BeginWidthIndex; i < EndWidthIndex - 1; i += 2)
                    {
                        if (NumberSet[i] != NumberSet[i + 2])
                        {
                            LocalMaze[newString, i + 1] = CellType.Empty;
                        }
                        NumberSet[i + 2] = NumberSet[i];
                    }
                    break;
                }
                else// кейс для не последней строки
                {
                    for (int i = BeginWidthIndex; i < EndWidthIndex; i+=2)
                    {
                        if (LocalMaze[CurrentString + 1, i] == CellType.Wall)// если ячейка имеет нижнюю границу - удалить ее из набора
                            NumberSet.Remove(i);

                    }
                }
                CurrentString = newString;
            }
            //случайная генерация выхода
            rand = new Random((int)DateTime.Now.Ticks);
            bool IsExitCreate = false;
            int x = 0;
            int y = 0;
            while (!IsExitCreate)
            {
                x = rand.Next(1, Width - 1);
                y = rand.Next(1, Height - 1);
                if (LocalMaze[y, x] == CellType.Empty)
                {
                    LocalMaze[y, x] = CellType.Exit;
                    IsExitCreate = true;
                }
            }
            //создание ячеек лабиринта
            MazeCells = new Cell[Height, Width];
          
           
            for (int i = 0; i != Height; i++)
            {
                for (int j = 0; j != Width; j++)
                {
                    switch (LocalMaze[i, j])
                    {
                        case CellType.Empty:
                            MazeCells[i, j] = new Cell(CellType.Empty);
                            break;
                        case CellType.Exit:
                            MazeCells[i, j] = new Cell(CellType.Exit);
                            break;
                        case CellType.Wall:
                            MazeCells[i, j] = new Cell(CellType.Wall);
                            break;

                    }
                }
            }
        }
        static bool IsCloseSpace(CellType[,] LocalMaze,int y, int x)// провека, что при генерации нижней границы получится замкнутое пространство
        {
            if (LocalMaze[y - 1, x] == CellType.Wall && LocalMaze[y, x - 1] == CellType.Wall 
                && LocalMaze[y, x + 1] == CellType.Wall)
                return true;
            return false;
                
        }
        
      
        static bool IsRandTrue(int begin,int min=0, int max=100, double ratio=-1)// используется для случайного принтия решения
        {
            Random rand = new Random(DateTime.Now.Second + DateTime.Now.Millisecond + (int)DateTime.Now.Ticks + begin);
           
            if(ratio<0)
                ratio = rand.Next(min, (min+max)/2);
            return rand.Next(min, max) > ratio;

        }
    }
}
