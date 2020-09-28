using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacManGame
{
    public partial class Form1 : Form 
    {
        bool goUp, goDown, goLeft, goRight, isGameOver;
        int score, playerSpeed, redGhostSpeed, yellowGhostX, pinkGhostX, pinkGhostY;
         

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                goUp = true;
            if (e.KeyCode == Keys.Down)
                goDown = true;
            if (e.KeyCode == Keys.Left)
                goLeft = true;
            if (e.KeyCode == Keys.Right)
                goRight = true;

            if (e.KeyCode == Keys.Enter && isGameOver == true)
                resetGame(); 
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                goUp = false;
            if (e.KeyCode == Keys.Down)
                goDown = false;
            if (e.KeyCode == Keys.Left)
                goLeft = false;
            if (e.KeyCode == Keys.Right)
                goRight = false; 
        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            if(goLeft == true)
            {
                pacMan.Left -= playerSpeed;
                pacMan.Image = Properties.Resources.left;
            }
            if (goRight == true)
            {
                pacMan.Left += playerSpeed;
                pacMan.Image = Properties.Resources.right;
            } 
            if (goDown == true)
            {
                pacMan.Top  += playerSpeed;
                pacMan.Image = Properties.Resources.down;
            }
            if (goUp == true)
            {
                pacMan.Top -= playerSpeed;
                pacMan.Image = Properties.Resources.Up ;
            }

            if (pacMan.Left < -10)
                pacMan.Left = 680;
            if (pacMan.Left > 680)
                pacMan.Left = -10;

            if (pacMan.Top < -10)
                pacMan.Top = 560;
            if (pacMan.Top > 560)
                pacMan.Top = -10;

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox )
                {
                    if((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if(pacMan.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false; 
                        }
                    }
                    if((string)x.Tag == "wall")
                    {
                        if(pacMan.Bounds.IntersectsWith(x.Bounds))
                            gameOver("YOU LOSE!");

                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds))
                            pinkGhostX = -pinkGhostX;
                    }
                    if((string)x.Tag == "ghost")
                    {
                        if (pacMan.Bounds.IntersectsWith(x.Bounds))
                            gameOver("YOU LOSE!");
                    }
                }
            }

            redGhost.Left += redGhostSpeed;
            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) ||
                redGhost.Bounds.IntersectsWith(pictureBox3.Bounds))
                redGhostSpeed = -redGhostSpeed;

            yellowGhost.Left += yellowGhostX;
            if (yellowGhost.Bounds.IntersectsWith(pictureBox5.Bounds) ||
                yellowGhost.Bounds.IntersectsWith(pictureBox8.Bounds))
                yellowGhostX = -yellowGhostX;

            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;
            if (pinkGhost.Top < 0 || pinkGhost.Top > 520)
                pinkGhostY = -pinkGhostY; 
            if(pinkGhost.Left < 0 || pinkGhost.Left > 620)
                pinkGhostX = -pinkGhostX;

            if (score == 90)
                gameOver("YOU WIN!");
        }

        private void resetGame()
        {
            txtScore.Text = "Score: 0";
            score = 0;
            redGhostSpeed = 5;
            yellowGhostX = 5;
            pinkGhostY = 5;
            pinkGhostX = 5;
            playerSpeed = 8; 

            isGameOver = false;
            pacMan.Left = 60;
            pacMan.Top = 75;

            redGhost.Left = 340;
            redGhost.Top = 80;

            yellowGhost.Left = 420;
            yellowGhost.Top = 460;

            pinkGhost.Left = 500;
            pinkGhost.Top = 250;

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                    x.Visible = true;
            }

            gameTimer.Start();
        }

        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + Environment.NewLine + message;
        }
    }
}
