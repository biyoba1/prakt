using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DollarCourse
{
    public class ArrayCurrencyData
    {
        List<CurrencyData> data;
        string year;

        public ArrayCurrencyData()
        {
            data = new List<CurrencyData>();
            year = "";
        }

        public void Add(CurrencyData item)
        {
            data.Add(item);
        }

        public int Count { get { return data.Count; } }
        public string Year { get { return year; } set { year = value; } }

        public CurrencyData this[int i]
        {
            get
            {
                if (i >= 0 && i < data.Count)
                    return data[i];
                else
                    return null;
            }
        }

        public void Clear()
        {
            data.Clear();
        }

        public void SaveToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(year);
                    writer.WriteLine(data.Count);
                    foreach (var item in data)
                    {
                        writer.WriteLine(item.Month);
                        writer.WriteLine(item.Rate.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    }
                }
                MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadFromFile(string fileName)
        {
            try
            {
                data.Clear();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    year = reader.ReadLine();
                    int count = int.Parse(reader.ReadLine());
                    for (int i = 0; i < count; i++)
                    {
                        string month = reader.ReadLine();
                        double rate = double.Parse(reader.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                        data.Add(new CurrencyData(month, rate));
                    }
                }
                MessageBox.Show("Данные успешно загружены!", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
