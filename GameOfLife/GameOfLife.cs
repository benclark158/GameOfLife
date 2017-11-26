using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class GameOfLife
    {
        public bool[,] grid;
        public Size gridSize;
        public GameOfLife(Size size)
        {
            this.grid = new bool[size.Width, size.Height];
            this.gridSize = size;

            this.basicGrid();
        }

        public GameOfLife(Size size, bool[,] initGrid)
        {
            this.grid = new bool[size.Width, size.Height];
            this.gridSize = size;

            this.basicGrid();

            for (int y = 0; y < initGrid.GetLength(0); y++)
            {
                for (int x = 0; x < initGrid.GetLength(1); x++)
                {
                    this.grid[x, y] = initGrid[y, x];
                }
            }
        }

        private void basicGrid()
        {
            for (int y = 0; y < this.gridSize.Height; y++)
            {
                for (int x = 0; x < this.gridSize.Width; x++)
                {
                    this.grid[x, y] = false;
                }
            }
        }

        public void tick()
        {
            //if point on grid is true then the cell is alive. 

            bool[,] newGrid = (bool[,])(this.grid.Clone());

            for (int y = 0; y < this.gridSize.Height; y++)
            {
                for (int x = 0; x < this.gridSize.Width; x++)
                {
                    int noAdjCells = this.getAdjAliveCells(this.getAdjCells(x, y));
                    bool cell = this.grid[x, y];

                    //Console.WriteLine(x + ":" + y + " - " + noAdjCells);

                    //RULES
                    /*
                    
                    Any live cell with fewer than two live neighbours dies, as if caused by under-population.
                    Any live cell with two or three live neighbours lives on to the next generation.
                    Any live cell with more than three live neighbours dies, as if by over-population.
                    Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

                    */

                    if (cell == true)
                    {
                        if(noAdjCells < 2)
                        {
                            //cell dies
                            newGrid[x, y] = false;
                            //Console.WriteLine(x + ":" + y + " - " + noAdjCells);
                            continue;
                        }
                        else if(noAdjCells == 2 || noAdjCells == 3)
                        {
                            //cell lives
                            newGrid[x, y] = true;
                            continue;
                        }
                        else if(noAdjCells > 3)
                        {
                            //cell dies
                            newGrid[x, y] = false;
                            continue;
                        }
                    }
                    else
                    {
                        if(noAdjCells == 3)
                        {
                            //cell born
                            newGrid[x, y] = true;
                            continue;
                        }
                    }
                }
            }
            this.grid = newGrid;
        }

        public List<Point> getAdjCells(int x, int y)
        {
            List<Point> l = new List<Point>();

            l.Add(new Point(x + 1, y));
            l.Add(new Point(x - 1, y));
            l.Add(new Point(x, y + 1));
            l.Add(new Point(x, y - 1));

            l.Add(new Point(x + 1, y + 1));
            l.Add(new Point(x + 1, y - 1));
            l.Add(new Point(x - 1, y + 1));
            l.Add(new Point(x - 1, y - 1));

            return l;
        }

        public int getAdjAliveCells(List<Point> adjPoints)
        {
            int count = 0; 
            foreach(Point p in adjPoints)
            {
                int x = p.X;
                int y = p.Y;

                //Console.WriteLine(this.gridSize.Width + " : " + this.gridSize.Height);

                if(x < 0 || y < 0 || x >= this.gridSize.Width || y >= this.gridSize.Height)
                {
                    continue;
                }
                else
                {
                    bool cell = this.grid[x, y];
                    if (cell)
                    {
                        //alive
                        count++;
                    }
                }
            }
            return count;
        }


    }
}
