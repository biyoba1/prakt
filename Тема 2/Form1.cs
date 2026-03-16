using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tema2_Variant15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputFields();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputFields();
        }

        private void UpdateInputFields()
        {
            if (radioButton1.Checked)
            {
                label4.Visible = label5.Visible = true;
                textBox4.Visible = textBox5.Visible = true;
                label6.Visible = label7.Visible = label8.Visible = false;
                textBox6.Visible = textBox7.Visible = textBox8.Visible = false;

                label1.Text = "Точка 1 прямой (x1, y1):";
                label2.Text = "Точка 2 прямой (x2, y2):";
                label3.Text = "Центр окружности (cx, cy):";
                label4.Text = "Радиус окружности r:";
            }
            else
            {
                label4.Visible = label5.Visible = false;
                textBox4.Visible = textBox5.Visible = false;
                label6.Visible = label7.Visible = label8.Visible = true;
                textBox6.Visible = textBox7.Visible = textBox8.Visible = true;

                label1.Text = "Центр окр. 1 (cx1, cy1):";
                label2.Text = "Радиус окр. 1 (r1):";
                label3.Text = "Центр окр. 2 (cx2, cy2):";
                label6.Text = "Радиус окр. 2 (r2):";
            }

            ClearResults();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    FindLineCircleIntersection();
                }
                else
                {
                    FindCircleCircleIntersection();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Особый случай", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FindLineCircleIntersection()
        {
            double x1 = Convert.ToDouble(textBox1.Text);
            double y1 = Convert.ToDouble(textBox2.Text);
            double x2 = Convert.ToDouble(textBox3.Text);
            double y2 = Convert.ToDouble(textBox4.Text);
            double cx = Convert.ToDouble(textBox5.Text);
            double cy = Convert.ToDouble(textBox6.Text);
            double r = Convert.ToDouble(textBox7.Text);

            GeometryHelper.ValidateCoordinates(x1, y1, x2, y2, cx, cy, r);

            List<Point> intersections = GeometryHelper.FindLineCircleIntersection(
                x1, y1, x2, y2, cx, cy, r);

            DisplayResults(intersections);
        }

        private void FindCircleCircleIntersection()
        {
            double cx1 = Convert.ToDouble(textBox1.Text);
            double cy1 = Convert.ToDouble(textBox2.Text);
            double r1 = Convert.ToDouble(textBox3.Text);
            double cx2 = Convert.ToDouble(textBox5.Text);
            double cy2 = Convert.ToDouble(textBox6.Text);
            double r2 = Convert.ToDouble(textBox8.Text);

            GeometryHelper.ValidateCoordinates(cx1, cy1, r1, cx2, cy2, r2);

            List<Point> intersections = GeometryHelper.FindCircleCircleIntersection(
                cx1, cy1, r1, cx2, cy2, r2);

            DisplayResults(intersections);
        }

        private void DisplayResults(List<Point> intersections)
        {
            if (intersections.Count == 0)
            {
                label9.Text = "Точек пересечения нет";
                label9.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (intersections.Count == 1)
            {
                label9.Text = $"Одна точка пересечения (касание):\n{intersections[0]}";
                label9.ForeColor = System.Drawing.Color.DarkGreen;
            }
            else
            {
                label9.Text = $"Две точки пересечения:\n" +
                              $"Точка 1: {intersections[0]}\n" +
                              $"Точка 2: {intersections[1]}";
                label9.ForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        private void ClearResults()
        {
            label9.Text = "";
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' &&
                e.KeyChar != '-' && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',' && ((TextBox)sender).Text.Contains(","))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '-' && ((TextBox)sender).SelectionStart != 0)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '-' && ((TextBox)sender).Text.Contains("-"))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
