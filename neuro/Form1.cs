using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuro
{
    public partial class Form1 : Form
    {
        private int step;
        private bool isFirstEra;

        private double i1;
        private double i2;
        private double w04;
        private double w05;
        private double w14;
        private double w15;
        private double w24;
        private double w25;
        private double w36;
        private double w46;
        private double w56;
        private double dw04;
        private double dw05;
        private double dw14;
        private double dw15;
        private double dw24;
        private double dw25;
        private double dw36;
        private double dw46;
        private double dw56;
        private double o0;
        private double o1;
        private double o2;
        private double o3;
        private double o4;
        private double o5;
        private double o6;
        private double net4;
        private double net5;
        private double net6;
        private double gamma1;
        private double gamma2;
        private double th;
        
        public Form1() {
            InitializeComponent();
            
            step = 0;
            isFirstEra = true;
        }

        private void RenewParameters() {
            i1 = Convert.ToDouble( numericUpDown_i1.Value );
            i2 = Convert.ToDouble( numericUpDown_i2.Value );
            w04 = Convert.ToDouble( numericUpDown_w04.Value );
            w05 = Convert.ToDouble( numericUpDown_w05.Value );
            w14 = Convert.ToDouble( numericUpDown_w14.Value );
            w15 = Convert.ToDouble( numericUpDown_w15.Value );
            w24 = Convert.ToDouble( numericUpDown_w24.Value );
            w25 = Convert.ToDouble( numericUpDown_w25.Value );
            w36 = Convert.ToDouble( numericUpDown_w36.Value );
            w46 = Convert.ToDouble( numericUpDown_w46.Value );
            w56 = Convert.ToDouble( numericUpDown_w56.Value );
            o0 = Convert.ToDouble( numericUpDown_o0.Value );
            o1 = Convert.ToDouble( numericUpDown_o1.Value );
            o2 = Convert.ToDouble( numericUpDown_o2.Value );
            o3 = Convert.ToDouble( numericUpDown_o3.Value );
            o4 = Convert.ToDouble( numericUpDown_o4.Value );
            o5 = Convert.ToDouble( numericUpDown_o5.Value );
            o6 = Convert.ToDouble( numericUpDown_o6.Value );
            net4 = Convert.ToDouble( numericUpDown_net4.Value );
            net5 = Convert.ToDouble( numericUpDown_net5.Value );
            net6 = Convert.ToDouble( numericUpDown_net6.Value );
            gamma1 = Convert.ToDouble( numericUpDown_gamma1.Value );
            gamma2 = Convert.ToDouble( numericUpDown_gamma2.Value );
            th = Convert.ToDouble( numericUpDown_th.Value );
        }

        private void RenewNet4() {
            RenewParameters();
            numericUpDown_net4.Value = Convert.ToDecimal(o0 * w04 + o1 * w14 + o2 * w24);
        }
        private void RenewNet5() {
            RenewParameters();
            numericUpDown_net5.Value = Convert.ToDecimal(o0 * w05 + o1 * w15 + o2 * w25);
        }
        private void RenewNet6() {
            RenewParameters();
            numericUpDown_net6.Value = Convert.ToDecimal(o3 * w36 + o4 * w46 + o5 * w56);
        }
        private void RenewO0() {
            numericUpDown_o0.Value = 1;
        }
        private void RenewO1() {
            numericUpDown_o1.Value = numericUpDown_i1.Value;
        }
        private void RenewO2() {
            numericUpDown_o2.Value = numericUpDown_i2.Value;
        }
        private void RenewO3() {
            numericUpDown_o3.Value = 1;
        }
        private void RenewO4() {
            numericUpDown_o4.Value = Convert.ToDecimal(Math.Tanh(Convert.ToDouble(numericUpDown_net4.Value)));
        }
        private void RenewO5() {
            numericUpDown_o5.Value = Convert.ToDecimal(Math.Tanh(Convert.ToDouble(numericUpDown_net5.Value)));
        }
        private void RenewO6() {
            numericUpDown_o6.Value = Convert.ToDecimal(Math.Tanh(Convert.ToDouble(numericUpDown_net6.Value)));
        }
        private void RenewOutput() {
            RenewParameters();
            numericUpDown_output.Value = numericUpDown_o6.Value;
            if (Math.Abs(o6 - th) <= 0.001)
            {
                MessageBox.Show("ИНС обучена!");
                button1.Enabled = false;
            }
        }
        private void RenewW56() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            if (isFirstEra)
                dw56 = gamma1 * delta6 * o5;
            else
                dw56 = gamma1 * delta6 * o5 + gamma2 * dw56;
            w56 = w56 + dw56;
            numericUpDown_w56.Value = Convert.ToDecimal(w56);
        }
        private void RenewW46() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            if (isFirstEra)
                dw46 = gamma1 * delta6 * o4;
            else
                dw46 = gamma1 * delta6 * o4 + gamma2 * dw46;
            w46 = w46 + dw46;
            numericUpDown_w46.Value = Convert.ToDecimal(w46);
        }
        private void RenewW36() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            if (isFirstEra)
                dw36 = gamma1 * delta6 * o3;
            else
                dw36 = gamma1 * delta6 * o3 + gamma2 * dw36;
            w36 = w36 + dw36;
            numericUpDown_w36.Value = Convert.ToDecimal(w36);
        }
        private void RenewW25() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            double delta5 = (o5 + 1) * (1 - o5) * delta6 * w56;
            double delta4 = (o4 + 1) * (1 - o4) * delta6 * w46;
            if (isFirstEra)
                dw25 = gamma1 * delta5 * o2;
            else
                dw25 = gamma1 * delta5 * o2 + gamma2 * dw25;
            w25 = w25 + dw25;
            numericUpDown_w25.Value = Convert.ToDecimal(w25);
        }
        private void RenewW24() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            double delta5 = (o5 + 1) * (1 - o5) * delta6 * w56;
            double delta4 = (o4 + 1) * (1 - o4) * delta6 * w46;
            if (isFirstEra)
                dw24 = gamma1 * delta4 * o2;
            else
                dw24 = gamma1 * delta4 * o2 + gamma2 * dw24;
            w24 = w24 + dw24;
            numericUpDown_w24.Value = Convert.ToDecimal(w24);
        }
        private void RenewW15() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            double delta5 = (o5 + 1) * (1 - o5) * delta6 * w56;
            double delta4 = (o4 + 1) * (1 - o4) * delta6 * w46;
            if (isFirstEra)
                dw15 = gamma1 * delta5 * o1;
            else
                dw15 = gamma1 * delta5 * o1 + gamma2 * dw15;
            w15 = w15 + dw15;
            numericUpDown_w15.Value = Convert.ToDecimal(w15);
        }
        private void RenewW14() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            double delta5 = (o5 + 1) * (1 - o5) * delta6 * w56;
            double delta4 = (o4 + 1) * (1 - o4) * delta6 * w46;
            if (isFirstEra)
                dw14 = gamma1 * delta4 * o1;
            else
                dw14 = gamma1 * delta4 * o1 + gamma2 * dw14;
            w14 = w14 + dw14;
            numericUpDown_w14.Value = Convert.ToDecimal(w14);
        }
        private void RenewW05() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            double delta5 = (o5 + 1) * (1 - o5) * delta6 * w56;
            double delta4 = (o4 + 1) * (1 - o4) * delta6 * w46;
            if (isFirstEra)
                dw05 = gamma1 * delta5 * o0;
            else
                dw05 = gamma1 * delta5 * o0 + gamma2 * dw05;
            w05 = w05 + dw05;
            numericUpDown_w05.Value = Convert.ToDecimal(w05);
        }
        private void RenewW04() {
            RenewParameters();
            double delta6 = (th - o6) * (o6 + 1) * (1 - o6);
            double delta5 = (o5 + 1) * (1 - o5) * delta6 * w56;
            double delta4 = (o4 + 1) * (1 - o4) * delta6 * w46;
            if (isFirstEra)
                dw04 = gamma1 * delta4 * o0;
            else
                dw04 = gamma1 * delta4 * o0 + gamma2 * dw04;
            w04 = w04 + dw04;
            numericUpDown_w04.Value = Convert.ToDecimal(w04);
        }

        private void button1_Click(object sender, EventArgs e) {
            switch (step)
            {
                case 0:
                    button1.Text = "Далее";
                    numericUpDown_i1.Enabled = false;
                    numericUpDown_i2.Enabled = false;
                    numericUpDown_w04.Enabled = false;
                    numericUpDown_w05.Enabled = false;
                    numericUpDown_w14.Enabled = false;
                    numericUpDown_w15.Enabled = false;
                    numericUpDown_w24.Enabled = false;
                    numericUpDown_w25.Enabled = false;
                    numericUpDown_w36.Enabled = false;
                    numericUpDown_w46.Enabled = false;
                    numericUpDown_w56.Enabled = false;
                    numericUpDown_th.Enabled = false;
                    numericUpDown_gamma1.Enabled = false;
                    numericUpDown_gamma2.Enabled = false; 
                    step = 1;
                    break;
                case 1:
                    numericUpDown_o0.BackColor = Color.Yellow;
                    numericUpDown_o1.BackColor = Color.Yellow;
                    numericUpDown_o2.BackColor = Color.Yellow;
                    RenewO0();
                    RenewO1();
                    RenewO2();
                    step = 2;
                    break;
                case 2:
                    numericUpDown_o0.BackColor = SystemColors.Control;
                    numericUpDown_o1.BackColor = SystemColors.Control;
                    numericUpDown_o2.BackColor = SystemColors.Control;
                    numericUpDown_net4.BackColor = Color.Yellow;
                    numericUpDown_net5.BackColor = Color.Yellow;
                    numericUpDown_o3.BackColor = Color.Yellow;
                    numericUpDown_o4.BackColor = Color.Yellow;
                    numericUpDown_o5.BackColor = Color.Yellow;
                    RenewNet4();
                    RenewNet5();
                    RenewO3();
                    RenewO4();
                    RenewO5();
                    step = 3;
                    break;
                case 3:
                    numericUpDown_net4.BackColor = SystemColors.Control;
                    numericUpDown_net5.BackColor = SystemColors.Control;
                    numericUpDown_o3.BackColor = SystemColors.Control;
                    numericUpDown_o4.BackColor = SystemColors.Control;
                    numericUpDown_o5.BackColor = SystemColors.Control;
                    numericUpDown_net6.BackColor = Color.Yellow;
                    numericUpDown_o6.BackColor = Color.Yellow;
                    RenewNet6();
                    RenewO6();
                    step = 4;
                    break;
                case 4:
                    numericUpDown_net6.BackColor = SystemColors.Control;
                    numericUpDown_o6.BackColor = SystemColors.Control;
                    numericUpDown_output.BackColor = Color.Yellow;
                    RenewOutput();
                    step = 5;
                    break;
                case 5:
                    numericUpDown_net6.BackColor = Color.Yellow;
                    numericUpDown_o6.BackColor = Color.Yellow;
                    numericUpDown_output.BackColor = SystemColors.Control;
                    step = 6;
                    break;
                case 6:
                    numericUpDown_net6.BackColor = SystemColors.Control;
                    numericUpDown_o6.BackColor = SystemColors.Control;
                    numericUpDown_w56.BackColor = Color.Yellow;
                    numericUpDown_w46.BackColor = Color.Yellow;
                    numericUpDown_w36.BackColor = Color.Yellow;
                    RenewW56();
                    RenewW46();
                    RenewW36();
                    step = 7;
                    break;
                case 7:
                    numericUpDown_w56.BackColor = SystemColors.Control;
                    numericUpDown_w46.BackColor = SystemColors.Control;
                    numericUpDown_w36.BackColor = SystemColors.Control;
                    numericUpDown_w25.BackColor = Color.Yellow;
                    numericUpDown_w15.BackColor = Color.Yellow;
                    numericUpDown_w05.BackColor = Color.Yellow;
                    numericUpDown_w24.BackColor = Color.Yellow;
                    numericUpDown_w14.BackColor = Color.Yellow;
                    numericUpDown_w04.BackColor = Color.Yellow;
                    RenewW25();
                    RenewW24();
                    RenewW15();
                    RenewW14();
                    RenewW05();
                    RenewW04();
                    isFirstEra = false;
                    step = 8;
                    break;
                case 8:
                    numericUpDown_w25.BackColor = SystemColors.Control;
                    numericUpDown_w15.BackColor = SystemColors.Control;
                    numericUpDown_w05.BackColor = SystemColors.Control;
                    numericUpDown_w24.BackColor = SystemColors.Control;
                    numericUpDown_w14.BackColor = SystemColors.Control;
                    numericUpDown_w04.BackColor = SystemColors.Control;
                    numericUpDown_net4.BackColor = Color.Yellow;
                    numericUpDown_net5.BackColor = Color.Yellow;
                    numericUpDown_o3.BackColor = Color.Yellow;
                    numericUpDown_o4.BackColor = Color.Yellow;
                    numericUpDown_o5.BackColor = Color.Yellow;
                    RenewNet4();
                    RenewNet5();
                    RenewO3();
                    RenewO4();
                    RenewO5();
                    step = 3;
                    break;
            }
        }
    }
}
