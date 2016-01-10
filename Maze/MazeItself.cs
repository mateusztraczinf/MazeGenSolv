using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze
{
    class MazeItself
    {
        Cell[,] grid;
        public List<Cell> Solved = new List<Cell>();

        public int SizeX
        {
            get { return grid.GetLength(0); }
        }
        public int SizeY
        {
            get { return grid.GetLength(1); }
        }

        public MazeItself(int size)
        {
            CreateGrid(size);
        }
        void CreateGrid(int size)
        {
            if (size >= 10) grid = new Cell[size, size];
            else grid = new Cell[10, 10];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new Cell(i, j);
                }
            }

            grid[0, 0].CType = CellTypes.Start;
            grid[0, 0].Visited = true;

            int size1 = grid.GetLength(0);
            int size2 = grid.GetLength(1);
            grid[size1 - 1, size2 - 1].CType = CellTypes.End; 
        }
        public Cell this[int x, int y]
        {
            get { return grid[x, y]; }
        }
        public void GenerateMaze()
        {
            int oldSize = grid.GetLength(0);
            CreateGrid(oldSize);

            var r = new Random();
            var route = new Stack<Point>();
            var currentCell = new Point(0, 0);
            var possibleCell = new Point(0, 0);
            var possibleMoves = new List<Point>();

            route.Push(currentCell);

            int visitedCells = 1;
            int allCells = grid.Length;

            while (visitedCells != allCells)
            {
                possibleMoves.Clear();

                int x = currentCell.X, y = currentCell.Y;
                if (CheckMove(x - 1, y, grid)) possibleMoves.Add(new Point(x - 1, y));  
                if (CheckMove(x, y - 1, grid)) possibleMoves.Add(new Point(x, y - 1));
                if (CheckMove(x + 1, y, grid)) possibleMoves.Add(new Point(x + 1, y));  
                if (CheckMove(x, y + 1, grid)) possibleMoves.Add(new Point(x, y + 1));

                if (possibleMoves.Count == 0)
                {
                    if (route.Count == 0)
                    {
                        return;
                    }
                    route.Pop();
                    currentCell = route.Peek();
                }  
                else
                {
                    int pickedNumber = r.Next(possibleMoves.Count);
                    var newCell = possibleMoves[pickedNumber];

                    RemoveWalls(ref grid[currentCell.X, currentCell.Y], ref grid[newCell.X, newCell.Y]);

                    currentCell = newCell;
                    grid[currentCell.X, currentCell.Y].Visited = true;
                    visitedCells++;

                    if (grid[currentCell.X,currentCell.Y].CType == CellTypes.End)
                    {
                        var list = route.ToList();
                        foreach (var item in list)
                        {
                            grid[item.X, item.Y].Solve = true;    
                        }
                        grid[SizeX - 1, SizeY - 1].Solve = true;
                    }

                    route.Push(currentCell);
                }
            }
        }
        void RemoveWalls(ref Cell c1, ref Cell c2)
        {
            if (c1.X == c2.X)
            {
                if (c1.Y < c2.Y)
                {
                    c1.wS.Right = false;
                    c2.wS.Left = false; 
                }
                else
                {
                    c1.wS.Left = false;
                    c2.wS.Right = false;
                }
            }
            else if (c1. Y == c2.Y)
            {
                if (c1.X < c2.X)
                {
                    c1.wS.Down = false;
                    c2.wS.Up = false;
                }
                else
                {
                    c1.wS.Up = false;
                    c2.wS.Down = false;
                }
            } 
        }
        bool CheckMove(int x, int y, Cell[,] grind)
        {
            if (x >= 0 &&
                y >= 0 &&
                x < grid.GetLength(0) &&
                y < grid.GetLength(1) && !grid[x, y].Visited)
                return true;
            else return false;
        }
    }
}
