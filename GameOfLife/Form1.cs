using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        GameOfLife gol;
        Panel[,] boxArray;
        int sx = 85;
        int sy = 85;
        int generation = 1;

        bool[,] startGrid = gliderGenerator; /*{
            {true, false, true, false, true, false },
            {false, true, false, true, false, true },
            {true, false, true, false, true, false }
        };*/

        static bool[,] gliderGenerator = {
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true , false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true , false, true , false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, true , true , false, false, false, false, false, false, true , true , false, false, false, false, false, false, false, false, false, false, false, false, true , true },
            {false, false, false, false, false, false, false, false, false, false, false, false, true , false, false, false, true , false, false, false, false, true , true , false, false, false, false, false, false, false, false, false, false, false, false, true , true },
            {false, true , true , false, false, false, false, false, false, false, false, true , false, false, false, false, false, true , false, false, false, true , true , false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, true , true , false, false, false, false, false, false, false, false, true , false, false, false, true , false, true , true , false, false, false, false, true , false, true , false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, true , false, false, false, false, false, true , false, false, false, false, false, false, false, true , false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, true , false, false, false, true , false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false, false, false, false, false, false, false, true , true , false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
        };

        public Form1()
        {
            InitializeComponent();
            this.initGrid();
            //this.tickCount.Start();
        }

        public void initGrid()
        {
            this.gol = new GameOfLife(new Size(sx, sy), startGrid);
            this.boxArray = new Panel[sx, sy];

            for(int y = 0; y < sy; y++)
            {
                for (int x = 0; x < sx; x++)
                {
                    Panel pic = new Panel();
                    pic.Name = x + ":" + y;
                    pic.Size = new Size(10, 10);
                    pic.Location = new Point(10*x, 10*y);
                    pic.BackColor = Color.White;

                    this.boxArray[x, y] = pic;
                    this.Controls.Add(pic);
                }
            }
            this.colour();
        }

        public void colour()
        {
            var grid = this.gol.grid;
            for (int y = 0; y < sy; y++)
            {
                for (int x = 0; x < sx; x++)
                {
                    if (grid[x, y])
                    {
                        //cell alive
                        this.boxArray[x, y].BackColor = Color.Green;
                    }
                    else
                    {
                        //dead
                        this.boxArray[x, y].BackColor = Color.Black;
                    }
                }
            }
            this.label1.Text = this.generation.ToString();
        }

        public void nextGen()
        {
            this.generation++;
            this.gol.tick();
            this.colour();
        }

        private void tickCount_Tick(object sender, EventArgs e)
        {
            this.nextGen();
        }

        private void nextGenButton_Click(object sender, EventArgs e)
        {
            this.nextGen();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            this.tickCount.Start();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            this.tickCount.Stop();
        }
    }
}
