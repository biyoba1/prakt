using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tema3_Variant15
{
    public partial class Form2 : Form
    {
        public Color SquareColor { get; set; }
        public int Speed { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            UpdateColorDisplay();
            UpdateSpeedDisplay();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = SquareColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                SquareColor = colorDialog.Color;
                UpdateColorDisplay();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Speed = 200 - trackBar1.Value * 20;
            UpdateSpeedDisplay();
        }

        private void UpdateColorDisplay()
        {
            panel1.BackColor = SquareColor;
        }

        private void UpdateSpeedDisplay()
        {
            label3.Text = $"Интервал: {Speed} мс";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
