using System;

namespace DollarCourse
{
    public class CurrencyData
    {
        private string month;
        private double rate;

        public CurrencyData(string month, double rate)
        {
            this.month = month;
            this.rate = rate;
        }

        public string Month
        {
            get { return month; }
        }

        public double Rate
        {
            get { return rate; }
            set { rate = value; }
        }
    }
}
