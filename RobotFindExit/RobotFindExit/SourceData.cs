using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotFindExit
{

    public class Cell
    {
        public CellType Type { get; private set; }
        public Cell(CellType type)
        {
            Type = type;
        }
    }

    public enum CellType
    {
        Empty,
        Wall,
        Exit
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public interface IRobot
    {
        // Sensors
        Cell CurrentCell { get; }
        Cell AdjacentCell(Direction direction);

        // Moving
        void Move(Direction direction);
    }
    
    public static class PathFinder
    {

        public static void FindExit(IRobot robot)
        {
            List<Cell> VisitedCells = new List<Cell>();// список посещенных клеток
            Stack<Cell> PrevCells = new Stack<Cell>();// стек с клетками, которые имеют не посещенных соседей
            VisitedCells.Add(robot.CurrentCell);// отмечаем текущую клетку как посещенную
            while (robot.CurrentCell.Type != CellType.Exit)
            {
                 List<Direction> UnvisitedNeighbors = GetUnvisitedNeighbors(robot, VisitedCells);// поиск не посещенных клеток
                 if (UnvisitedNeighbors.Count() > 0)// если есть не посещенные клетки
                 {
                     PrevCells.Push(robot.CurrentCell);
                     Direction? NextDirection = GetExitNeighbor(robot,UnvisitedNeighbors);// проверяем есть ли рядом выход
                     if(NextDirection == null)
                         NextDirection = GetRandUnvisitedNeighbors(UnvisitedNeighbors); // случайный выбор направления дальнейшего движения 
                     robot.Move((Direction)NextDirection);
                     VisitedCells.Add(robot.CurrentCell);// отмечаем текущую клетку как посещенную
                 }
                 else if (PrevCells.Count != 0)// если не посещенных клеток нет, возвращаемся на предыдущую клетку, тк у нее есть не посещенные клетки
                 {
                     Cell NextCell = PrevCells.Pop();
                     Direction? NextDirection = FindCell(robot, NextCell);// поиск нужного направления для перехода на нужную клетку
                     if (NextDirection != null)
                         robot.Move(NextDirection.Value);
                     else
                     {
                         throw new ExceptionAlgoritmFail("Не найдено нужное направление!");
                     }
                 }
                 else // не осталось больше не  посещенных клеток, значит выхода из лабиринта нет.
                 {
                     throw new ExceptionsExitNotExist("Exit not found!");
                 }
                 
            }
            Console.WriteLine("Win!");
            
        }
        /// <summary>
        /// Метод FindCell() возвращает 
        /// направление по которому нужно пойти роботу, чтобы попасть в искомую соседнюю клетку.
        /// </summary>
        /// <param name="RequiredCell">Аргумент представляет искомую клетку 
        /// </param>
        /// <returns>Возвращает направление по которому должен пойти робот, 
        /// чтобы попасть в нужную соседнюю клетку и null,
        /// если по соседству такой клетки не найдено</returns>
        static Direction? FindCell(IRobot robot, Cell RequiredCell)
        {
            foreach (Direction neighbor in Enum.GetValues(typeof(Direction)))
            {
                Cell tmp = robot.AdjacentCell(neighbor);
                if (tmp == RequiredCell)
                    return neighbor;
            }
            return null;
        }
        /// <summary>
        /// Метод GetRandUnvisitedNeighbors() возвращает 
        /// случайно выбранное направление из переданных через аргумент UnvisitedNeighbors.
        /// </summary>
        /// <param name="UnvisitedNeighbors">Аргумент представляющий список направлений
        /// из которых случайным образом выбирается направление
        /// </param>
        /// <returns>Возвращает случайно выбранное направление в виде объекта перечисления Direction</returns>
        static Direction GetRandUnvisitedNeighbors(List<Direction> UnvisitedNeighbors)
        {
            Random rand = new Random();
            int index = rand.Next(0, UnvisitedNeighbors.Count() - 1);
            return UnvisitedNeighbors.ElementAt(index);
        }
        static Direction? GetExitNeighbor(IRobot robot, List<Direction> UnvisitedNeighbors)
        {

            foreach (Direction neighbor in UnvisitedNeighbors)
            {
                Cell cell = robot.AdjacentCell(neighbor);
                if (cell.Type == CellType.Exit )
                {
                    return neighbor;
                }
            }
            return null;

        }
        /// <summary>
        /// Метод GetUnvisitedNeighbors() возвращает 
        /// список направлений в которых находятся не посещенные клетки, которые не яляются стенами
        /// </summary>
        /// <param name="VisitedCells">Аргумент представляющий коллекцию посещенных клеток.
        /// Используется, чтобы проверить, что соседние клетки не были посещены ранее. 
        /// </param>
        /// <param name="robot">Объект, реализующий интерфейс IRobot.
        /// Необходим для вызова метода AdjacentCell для просмотра соседних клеток по всем направлениям.
        /// </param>
        /// <returns>Возвращает список возможных направлений в виде списка объектов перечисления Direction</returns>
        static List<Direction> GetUnvisitedNeighbors(IRobot robot, List<Cell> VisitedCells)
        {
            List<Direction> UnvisitedNeighbors = new List<Direction>();
            foreach(Direction neighbor in Enum.GetValues(typeof(Direction)))
            {
                Cell tmp = robot.AdjacentCell(neighbor);
                if (tmp.Type != CellType.Wall && !VisitedCells.Contains(tmp))
                {
                    UnvisitedNeighbors.Add(neighbor);
                }
            }
            return UnvisitedNeighbors;
            
        }
    }
}
