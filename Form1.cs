using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using DynamicCurrConv.Properties;

namespace DynamicCurrConv
{
    public partial class Form1 : Form
    {
        Image reverse = Resources.reversess;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = reverse;
        }
        private double getRate(string fromCurrency, string toCurrency)
        {
            var json = "";
            string rate = "";
            try
            {
                string url = string.Format("https://free.currconv.com/api/v7/convert?q={0}_{1}&compact=ultra&apiKey=bb511152a3b90d35d7fe", fromCurrency.ToUpper(), toCurrency.ToUpper());
                string key = string.Format("{0}_{1}", fromCurrency.ToUpper(), toCurrency.ToUpper());

                json = new WebClient().DownloadString(url);
                dynamic stuff = JsonConvert.DeserializeObject(json);
                rate = stuff[key];
            }
            catch
            {
                rate = "0";
            }

            return double.Parse(rate);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                label9.Text = "Insert the amount you want to convert.";
                label9.ForeColor = Color.Red;
            }
            else
            {
                if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
                {
                    label10.Text = "One or two of the currencies are still empty.";
                    label10.ForeColor = Color.Red;
                }
                else
                {
                    double rate = getRate(comboBox1.Text, comboBox2.Text);
                    double output = double.Parse(textBox1.Text) * rate;

                    label8.Text = output.ToString();
                    label8.ForeColor = Color.Black;
                    label5.Text = comboBox2.Text;
                    label5.ForeColor = Color.Black;

                    label6.Text = ("Last update:");
                    label6.ForeColor = Color.Black;
                    label7.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    label7.ForeColor = Color.Black;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
             string new2 = comboBox1.Text;
            comboBox1.Text = comboBox2.Text;
            comboBox2.Text = new2;
        }
    }
}
