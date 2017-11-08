using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assessment
{
    public partial class Form1 : Form
    {
        private CalculatorInput input = new CalculatorInput();

        public Form1()
        {
            InitializeComponent();

            btn0.Click += new EventHandler(NumberButtonClicked);
            btn1.Click += new EventHandler(NumberButtonClicked);
            btn2.Click += new EventHandler(NumberButtonClicked);
            btn3.Click += new EventHandler(NumberButtonClicked);
            btn4.Click += new EventHandler(NumberButtonClicked);
            btn5.Click += new EventHandler(NumberButtonClicked);
            btn6.Click += new EventHandler(NumberButtonClicked);
            btn7.Click += new EventHandler(NumberButtonClicked);
            btn8.Click += new EventHandler(NumberButtonClicked);
            btn9.Click += new EventHandler(NumberButtonClicked);

            btnCancel.Click += new EventHandler(OperatorButtonClicked);
            btnDivision.Click += new EventHandler(OperatorButtonClicked);
            btnMinus.Click += new EventHandler(OperatorButtonClicked);
            btnMultiplication.Click += new EventHandler(OperatorButtonClicked);
            btnPlus.Click += new EventHandler(OperatorButtonClicked);

            btnEqual.Click += new EventHandler(ExecuteEqualSign);
        }

        private void OperatorButtonClicked(object sender, EventArgs e)
        {
            char operation = (sender as Button).Text[0];
            if (operation == 'C')
            {
                input = new CalculatorInput();
             
                txtDisplay.Text = string.Empty;
            }
            else
            {
                input.Operator = operation;
            }
          
            DisplayInputs();
        }

        private void NumberButtonClicked(object sender, EventArgs e)
        {
            if (input.Total != 0)
            {
                txtDisplay.Text = string.Empty;
                input = new CalculatorInput();
            }

            lblError.Visible = true;
            lblError.Text = string.Empty;

            string CurrentNumber = (sender as Button).Text;
            

            var Operator = input.Operator;
            var strFirstNumber = input.FirstNumber != null ? input.FirstNumber.ToString() : string.Empty;
            var strSecondNumber = input.SecondNumber != null ? input.SecondNumber.ToString() : string.Empty;

            if (Operator == null)
            {
                 strFirstNumber += CurrentNumber.ToString();
                input.FirstNumber = Convert.ToInt32(strFirstNumber);
            }
            else
            {
                strSecondNumber += CurrentNumber.ToString();
                input.SecondNumber = Convert.ToInt32(strSecondNumber);
            }

            DisplayInputs();
        }
              
        private void DisplayInputs()
        {
           // Handles the display of inputs that are entered by the user and the results based on the operator that is passed
            var strFirstNumber = input.FirstNumber != null ? input.FirstNumber.ToString() : string.Empty;
            var strSecondNumber = input.SecondNumber != null  ? input.SecondNumber.ToString() : string.Empty;
            var strOperator = input.Operator;
            var strTotal = input.Total != 0 ? input.Total.ToString() : string.Empty;
            var equalSign = input.Total != 0  ? "=" : string.Empty;

            var result = string.Format("{0} {1} {2} {3} {4}", strFirstNumber, strOperator, strSecondNumber, equalSign, strTotal);
            result = result.Trim();

            txtDisplay.Text = result;
        }

        private void ExecuteEqualSign(object sender, EventArgs e)
        {
            //This is responsible for doing the calculation operation and it calls the displa method to display the calculated results.

            try
            {
                var strOperator = input.Operator;
                var fNumber = Convert.ToDouble(input.FirstNumber);
                var sNumber = Convert.ToDouble(input.SecondNumber);
                var Total = 0.0;

                switch (strOperator)
                {
                    case '+': Total = fNumber + sNumber; break;
                    case '-': Total = fNumber - sNumber; break;
                    case '×': Total = fNumber * sNumber; break;
                    case '÷': Total = fNumber / sNumber; break;
                }

                input.Total = Total;
                DisplayInputs();
            }
            catch(Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
    }

    public class CalculatorInput
    {
        public double? FirstNumber { get; set; }
        public double? SecondNumber { get; set; } 
        public char? Operator { get; set; }
        public double Total { get; set; }
    }
}
