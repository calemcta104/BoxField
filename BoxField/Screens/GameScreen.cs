using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        #region declaring variables
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        //used to draw boxes on screen
        SolidBrush heroBrush = new SolidBrush(Color.Red);
        SolidBrush boxBrush = new SolidBrush(Color.White);

        List<Box> left = new List<Box>();
        List<Box> right = new List<Box>();

        Box hero;
        int heroSpeed = 7;
        int heroSize = 25;

        Random randGen = new Random();

        int leftMoveCounter, rightMoveCounter = 0;

        int leftX = 250;
        int gap = 300;
        Boolean moveRight;
        //true = right
        int patternLength = 20;
        int patternSpeed = 7;


        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            MakeBox();

            Color c = Color.Red;
            hero = new Box(this.Width / 2 - heroSize / 2, 445, heroSize, c);
        }

        public void MakeBox()
        {
            int rand = randGen.Next(1, 5);
            Color c = Color.Black;

            if (rand == 1) { c = Color.MediumBlue; }
            else if (rand == 2) { c = Color.DarkSlateGray; }
            else if (rand == 3) { c = Color.CornflowerBlue; }
            else if (rand == 4) { c = Color.CadetBlue; }

            //true = right
            patternLength--;

            if (patternLength == 0)
            {
                moveRight = !moveRight;

                patternLength = randGen.Next(6, 14);
            }

            //true = right
            if (moveRight == true)
            { leftX += patternSpeed; }
            else { leftX -= patternSpeed; }

            Box newbox = new Box(leftX, 0, 20, c);
            Box newbox2 = new Box(leftX + gap, 0, 20, c);
            left.Add(newbox);
            right.Add(newbox2);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            leftMoveCounter++;
            rightMoveCounter++;

            //update location of all boxes (drop down screen)
            foreach (Box b in left)
            {
                b.Move(10);
            }
            foreach (Box b in right)
            {
                b.Move(10);
            }
            //remove box if it has gone of screen
            if (left[0].y > this.Height)
            {
                left.RemoveAt(0);
            }
            if (right[0].y > this.Height)
            {
                right.RemoveAt(0);
            }
            //add new box if it is time
            if (leftMoveCounter > 2)
            {
                leftMoveCounter = 0;
                MakeBox();
            }
            if (rightMoveCounter > 2)
            {
                rightMoveCounter = 0;
                MakeBox();
            }

            //move character
            //if (leftArrowDown == true)
            //{
            //    hero.x -= heroSpeed;
            //}
            //if (rightArrowDown == true)
            //{
            //    hero.x += heroSpeed;
            //}
            //if (upArrowDown == true)
            //{
            //    hero.y -= heroSpeed;
            //}
            //if (downArrowDown == true)
            //{
            //    hero.y += heroSpeed;
            //}

            if (leftArrowDown == true)
            {
                hero.Move(heroSpeed, false);
            }
            else if (rightArrowDown == true)
            {
                hero.Move(heroSpeed, true);
            }
            if (upArrowDown == true)
            {
                hero.Move2(heroSpeed, false);
            }
            if (downArrowDown == true)
            {
                hero.Move2(heroSpeed, true);
            }

            Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size);

            if (left.Count >= 15)
            {
                for (int i = 0; i < 15; i++)
                {
                    Rectangle boxRec = new Rectangle(left[i].x, left[i].y, left[i].size, left[i].size);
                    Rectangle rightBoxRec = new Rectangle(right[i].x, right[i].y, right[i].size, right[i].size);

                    if (boxRec.IntersectsWith(heroRec) || rightBoxRec.IntersectsWith(heroRec))
                    {
                        gameLoop.Enabled = false;
                    }
                }
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw boxes to screen
            foreach (Box b in left)
            {
                boxBrush.Color = b.color;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            foreach (Box b in right)
            {
                boxBrush.Color = b.color;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            //draw hero character
            e.Graphics.FillRectangle(heroBrush, hero.x, hero.y, hero.size, hero.size);

        }
    }
}
