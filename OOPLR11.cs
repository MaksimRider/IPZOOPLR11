
using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class FormCalc : Form
    {
        bool resultTablo = true;
        string operation = "";
        double memory = 0;

        public FormCalc()
        {
            InitializeComponent();
        }

        void plusTablo(char symbol)
        {
            if (resultTablo)
            {
                if (textTablo.Text == "" || textTablo.Text == "0")
                    textTablo.Text = "";
                textTablo.Text = symbol.ToString();
                resultTablo = false;
            }
            else
                textTablo.Text += symbol.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            plusTablo('9');
        }

        private void buttonKom_Click(object sender, EventArgs e)
        {
            if (resultTablo)
                textTablo.Text = "0";
            bool available = true;
            int i, len = textTablo.Text.Length;
            for (i = 0; i < len; i++)
                if (textTablo.Text[i] == ',')
                {
                    available = false;
                    break;
                }
            if (available)
                textTablo.Text += ",";
            resultTablo = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (resultTablo)
                textTablo.Text = "0";
            else
                textTablo.Text = textTablo.Text.Substring(0, textTablo.Text.Length - 1);
            if (textTablo.Text == "")
                textTablo.Text = "0";
            resultTablo = false;
        }

        private void runOperationTablo(string opr)
        {
            double tablo = 0;
            if (opr == "")
                return;
            try
            {
                tablo = Convert.ToDouble(textTablo.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Операція виконана неможливо", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textTablo.Text = "0";
                return;
            }

            switch (opr)
            {
                case "sqrt":
                    if (tablo < 0)
                    {
                        MessageBox.Show("Операція неможлива: корінь із від’ємного числа", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textTablo.Text = "0";
                        return;
                    }
                    tablo = Math.Sqrt(tablo);
                    break;
                case "%":
                    tablo *= 0.01;
                    break;
                // ДОДАНО: Обчислення факторіалу
                case "fact":
                    if (tablo < 0 || tablo != Math.Floor(tablo))
                    {
                        MessageBox.Show("Факторіал визначено лише для невід'ємних цілих чисел", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    double fact = 1;
                    for (int i = 1; i <= (int)tablo; i++)
                        fact *= i;
                    tablo = fact;
                    break;
                // ДОДАНО: Зміна знаку
                case "+/-":
                    tablo = -tablo;
                    break;
            }
            textTablo.Text = tablo.ToString();
            resultTablo = true;
        }

        private void buttonFactorial_Click(object sender, EventArgs e)
        {
            runOperationTablo("fact");
        }

        private void buttonSignChange_Click(object sender, EventArgs e)
        {
            runOperationTablo("+/-");
        }
    }
}
