using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Laba5
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private PotentialMethod potentialMethod = null;
        private List<int> weigths;

        public Form1()
        {
            InitializeComponent();
        }

        private string NumberToSignString(int number)
        {
            if (number < 0)
            {
                return number.ToString();
            }
            else
            {
                return "+" + number.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var x11 = (int) numericUpDownX11.Value;
                var x12 = (int) numericUpDownX12.Value;
                var x21 = (int) numericUpDownX21.Value;
                var x22 = (int) numericUpDownX22.Value;
                var y11 = (int) numericUpDownY11.Value;
                var y12 = (int) numericUpDownY12.Value;
                var y21 = (int) numericUpDownY21.Value;
                var y22 = (int) numericUpDownY22.Value;

                if ((((x11 == x12 && x21 == x22) && ((y11 < y22 && y12 > y21) || (y12 < y22 && y12 > y21))) ||
                     ((y11 == y12 && y21 == y22) && ((x11 < x22 && x12 > x21) || (x12 < x22 && x12 > x21)))))
                {
                    MessageBox.Show("некорректный ввод!");
                }
                else
                {
                    potentialMethod = new PotentialMethod();
                    var classes = new List<PotentialMethod.PotentialClass>();
                    classes.Add(new PotentialMethod.PotentialClass());
                    classes.Add(new PotentialMethod.PotentialClass());
                    classes[0].vectors.Add(new Point((int) numericUpDownX11.Value, (int) numericUpDownY11.Value));
                    classes[0].vectors.Add(new Point((int) numericUpDownX12.Value, (int) numericUpDownY12.Value));
                    classes[1].vectors.Add(new Point((int) numericUpDownX21.Value, (int) numericUpDownY21.Value));
                    classes[1].vectors.Add(new Point((int) numericUpDownX22.Value, (int) numericUpDownY22.Value));
                    bool correct = true;
                    if (correct)
                    {
                        potentialMethod.SetObjects(classes);
                        weigths = potentialMethod.CalculateFunction();
                        potentialMethod.DrawChartGraphics(chart);
                        string str;
                        str = weigths[0].ToString();
                        str += NumberToSignString(weigths[1]) + "*x1";
                        str += NumberToSignString(weigths[2]) + "*x2";
                        str += NumberToSignString(weigths[3]) + "*x1*x2";
                        textBoxFunction.Text = str;
                    }
                    else
                    {
                        MessageBox.Show("Некорректный ввод признаков объектов!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректный ввод" + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (potentialMethod != null)
            {
                int res = potentialMethod.DistributeVector(new Point((int) numericUpDownTestX1.Value,
                    (int) numericUpDownTestY1.Value));
                if (res != 0)
                {
                    MessageBox.Show(String.Format("Объект относится к {0} классу", res));
                }
                else
                {
                    MessageBox.Show(String.Format("Объект находится на границе классов!"));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}