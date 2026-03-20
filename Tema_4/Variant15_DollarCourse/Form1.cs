using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DollarCourse
{
    public partial class Form1 : Form
    {
        private ArrayCurrencyData currencyData;

        public Form1()
        {
            InitializeComponent();
            currencyData = new ArrayCurrencyData();
            cmbMonth.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtYear.Text))
                {
                    MessageBox.Show("Введите год!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbMonth.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите месяц!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtRate.Text))
                {
                    MessageBox.Show("Введите курс доллара!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double rate = double.Parse(txtRate.Text);
                if (rate <= 0)
                {
                    MessageBox.Show("Курс должен быть положительным числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string month = cmbMonth.SelectedItem.ToString();

                currencyData.Year = txtYear.Text;
                currencyData.Add(new CurrencyData(month, rate));

                UpdateTable();

                MessageBox.Show("Данные добавлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtRate.Clear();
                if (cmbMonth.SelectedIndex < 11)
                    cmbMonth.SelectedIndex++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < currencyData.Count; i++)
            {
                dataGridView1.Rows.Add(currencyData[i].Month, currencyData[i].Rate.ToString("F2"));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currencyData.Count == 0)
            {
                MessageBox.Show("Нет данных для сохранения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveDialog.Title = "Сохранить данные";
            saveDialog.FileName = "currency_data.txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                currencyData.SaveToFile(saveDialog.FileName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openDialog.Title = "Загрузить данные";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                currencyData.LoadFromFile(openDialog.FileName);
                txtYear.Text = currencyData.Year;
                UpdateTable();
            }
        }

        private void btnBuildChart_Click(object sender, EventArgs e)
        {
            if (currencyData.Count == 0)
            {
                MessageBox.Show("Нет данных для построения диаграммы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BuildChart();
        }

        private void BuildChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();

            // Форматировать диаграмму
            chart1.BackColor = Color.LightGray;
            chart1.BackSecondaryColor = Color.WhiteSmoke;
            chart1.BackGradientStyle = GradientStyle.DiagonalRight;

            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            chart1.BorderlineColor = Color.Gray;

            // Форматировать область диаграммы
            chart1.ChartAreas[0].BackColor = Color.White;
            chart1.ChartAreas[0].AxisX.Title = "Месяц";
            chart1.ChartAreas[0].AxisY.Title = "Курс (₽)";
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Добавить заголовок
            chart1.Titles.Add($"Динамика курса доллара в {currencyData.Year} году");
            chart1.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            // Создать серию данных
            Series series = new Series("Курс доллара");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.Green;
            series.BorderWidth = 2;

            // Добавить данные
            for (int i = 0; i < currencyData.Count; i++)
            {
                series.Points.AddXY(currencyData[i].Month, currencyData[i].Rate);
            }

            chart1.Series.Add(series);

            // Настроить легенду
            chart1.Legends[0].Enabled = true;
            chart1.Legends[0].Title = "Валюта";
        }
    }
}
