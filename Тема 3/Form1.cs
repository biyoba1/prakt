using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tema3_Variant15
{
    public partial class Form1 : Form
    {
        private int squareX;
        private int squareY;
        private int squareSize = 40;
        private Color squareColor;
        private int direction = 0;
        private int speed = 5;
        private bool isMoving = false;

        public Form1()
        {
            InitializeComponent();
            InitializeSquare();
            this.KeyPreview = true;
        }

        private void InitializeSquare()
        {
            squareX = 10;
            squareY = 10;
            squareColor = Color.Red;
            direction = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveSquare();
            this.Invalidate();
        }

        private void MoveSquare()
        {
            int margin = 10;
            int bottomLimit = this.ClientSize.Height - margin;

            switch (direction)
            {
                case 0:
                    squareX += speed;
                    if (squareX + squareSize >= this.ClientSize.Width - margin)
                    {
                        squareX = this.ClientSize.Width - margin - squareSize;
                        direction = 1;
                        ChangeColor();
                    }
                    break;

                case 1:
                    squareY += speed;
                    if (squareY + squareSize >= bottomLimit - squareSize)
                    {
                        squareY = bottomLimit - squareSize - squareSize;
                        direction = 2;
                        ChangeColor();
                    }
                    break;

                case 2:
                    squareX -= speed;
                    if (squareX <= margin)
                    {
                        squareX = margin;
                        direction = 3;
                        ChangeColor();
                    }
                    break;

                case 3:
                    squareY -= speed;
                    if (squareY <= margin)
                    {
                        squareY = margin;
                        direction = 0;
                        ChangeColor();
                    }
                    break;
            }
        }

        private void ChangeColor()
        {
            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Orange };
            squareColor = colors[direction % colors.Length];
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            using (SolidBrush brush = new SolidBrush(squareColor))
            {
                g.FillRectangle(brush, squareX, squareY, squareSize, squareSize);
            }

            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(pen, squareX, squareY, squareSize, squareSize);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isMoving)
            {
                timer1.Start();
                button1.Text = "Остановить";
                isMoving = true;
            }
            else
            {
                timer1.Stop();
                button1.Text = "Запустить";
                isMoving = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 settingsForm = new Form2();
            settingsForm.SquareColor = squareColor;
            settingsForm.Speed = timer1.Interval;

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                squareColor = settingsForm.SquareColor;
                timer1.Interval = settingsForm.Speed;
                this.Invalidate();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isMoving)
            {
                timer1.Stop();
                isMoving = false;
                button1.Text = "Запустить";
                MessageBox.Show("Движение остановлено нажатием клавиши!", "Остановка",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int margin = 10;
            int maxX = this.ClientSize.Width - margin - squareSize;
            int maxY = this.ClientSize.Height - margin - squareSize - squareSize;

            if (squareX > maxX) squareX = maxX;
            if (squareY > maxY) squareY = maxY;

            this.Invalidate();
        }
    }
}
