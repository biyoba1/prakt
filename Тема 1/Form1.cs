using System;
using System.Windows.Forms;

namespace Tema1_Variant15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Вычисление значения функции e^(-x²) с помощью встроенной функции
        private double ExpFunction(double x)
        {
            return Math.Exp(-x * x);
        }

        // Вычисление значения функции e^(-x²) через разложение в ряд
        // e^(-x²) = Σ(n=0 to ∞) [(-1)^n * x^(2n)] / n!
        private double Sum(double x, double eps, out int count)
        {
            double sum = 1.0;
            double a = 1.0;
            count = 0;
            double x2 = x * x;

            // Вычисляем сумму, пока текущий член ряда больше заданной точности
            while (Math.Abs(a) >= eps * Math.Abs(sum))
            {
                count++;
                a = a * (-x2) / count;
                sum = sum + a;
            }

            return sum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double x = Convert.ToDouble(textBox1.Text);
                double eps = Convert.ToDouble(textBox2.Text);

                if (eps <= 0 || eps >= 1)
                {
                    MessageBox.Show("Точность должна быть в диапазоне 0 < eps < 1",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int count;
                double expResult = ExpFunction(x);
                double sumResult = Sum(x, eps, out count);

                label3.Text = $"Значение e^(-x²) = {expResult:F10}\n" +
                              $"Сумма ряда = {sumResult:F10}\n" +
                              $"Количество членов ряда: {count + 1}\n" +
                              $"Погрешность: {Math.Abs(expResult - sumResult):E4}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения!",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text) &&
                              !string.IsNullOrWhiteSpace(textBox2.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' &&
                e.KeyChar != '\b')
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
        }
    }
}
