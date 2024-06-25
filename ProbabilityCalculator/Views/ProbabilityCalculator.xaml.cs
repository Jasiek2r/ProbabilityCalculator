using ProbabilityCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProbabilityCalculator.Views
{
    /// <summary>
    /// Interaction logic for ProbabilityCalculator.xaml
    /// </summary>
    public partial class ProbabilityCalculator : Window
    {

        Calculator probabilisticCalculator = new Calculator();
        public ProbabilityCalculator()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            UnlockOps();
            switch (workingOperation)
            {
                case "+":
                    probabilisticCalculator.Add("scalars", "ANS", "OPVAL");

                    break;
                case "x":
                    probabilisticCalculator.Multiply("scalars", "ANS", "OPVAL");
                    break;
                case "-":
                    probabilisticCalculator.Subtract("scalars", "ANS", "OPVAL");
                    break;
                case "/":
                    probabilisticCalculator.Divide("scalars", "ANS", "OPVAL");
                    break;
                default:
                    break;
            }

            workingOperation = "";
            workingVariable = "ANS";
            VarNameDisplay.Text = workingVariable;
            Scalar ANS = probabilisticCalculator.ReadScalar(workingVariable);
            NumericDisplay.Text = ANS.GetValue().ToString();
            probabilisticCalculator.ResetScalar("OPVAL");

        }

        private bool _isCommaJustClicked = false;

        string workingVariable = "ANS";
        string workingOperation = "";

        private bool _isNumericKeyPadLocked = false;

        private void AddNumber(int number)
        {

            if (_isNumericKeyPadLocked)
            {
                return;
            }
            //calculate the new value
            Scalar ANS = probabilisticCalculator.ReadScalar(workingVariable);
            ANS.AppendDigit(number);
            if (_isCommaJustClicked)
            {
                _isCommaJustClicked = false;
                decimal ansValue = ANS.GetValue();
                ansValue = ansValue / 10;
                ANS.SetValue(ansValue);
                ANS.SetHasDecimalPart(true);
            }
            probabilisticCalculator.WriteScalar(workingVariable, ANS);
            //update display
            NumericDisplay.Text = ANS.GetValue().ToString();
        }

        private void Add0(object sender, RoutedEventArgs e)
        {
            AddNumber(0);
        }

        private void Add1(object sender, RoutedEventArgs e)
        {
            AddNumber(1);
        }

        private void Add2(object sender, RoutedEventArgs e)
        {
            AddNumber(2);
        }

        private void Add3(object sender, RoutedEventArgs e)
        {
            AddNumber(3);
        }

        private void Add4(object sender, RoutedEventArgs e)
        {
            AddNumber(4);
        }

        private void Add5(object sender, RoutedEventArgs e)
        {
            AddNumber(5);
        }

        private void Add6(object sender, RoutedEventArgs e)
        {
            AddNumber(6);
        }

        private void Add7(object sender, RoutedEventArgs e)
        {
            AddNumber(7);
        }

        private void Add8(object sender, RoutedEventArgs e)
        {
            AddNumber(8);
        }

        private void Add9(object sender, RoutedEventArgs e)
        {
            AddNumber(9);
        }

        private void DelNumber(object sender, RoutedEventArgs e)
        {
            //calculate the new value
            Scalar ANS = probabilisticCalculator.ReadScalar(workingVariable);
            ANS.PopDigit();
            probabilisticCalculator.WriteScalar(workingVariable, ANS);
            //update display
            NumericDisplay.Text = ANS.GetValue().ToString();
        }

        private void LockOps()
        {
            Adder.Content = "";
            Subtractor.Content = "";
            Divider.Content = "";
            Multiplicator.Content = "";
        }

        private void UnlockOps()
        {
            Adder.Content = "+";
            Subtractor.Content = "-";
            Divider.Content = "/";
            Multiplicator.Content = "x";
        }

        private void LockNumericKeypad()
        {
            Btn0.Content = "";
            Btn1.Content = "";
            Btn2.Content = "";
            Btn3.Content = "";
            Btn4.Content = "";
            Btn5.Content = "";
            Btn6.Content = "";
            Btn7.Content = "";
            Btn8.Content = "";
            Btn9.Content = "";
            BtnComma.Content = "";

            _isNumericKeyPadLocked = true;
        }

        private void UnlockNumericKeypad()
        {
            Btn0.Content = "0";
            Btn1.Content = "1";
            Btn2.Content = "2";
            Btn3.Content = "3";
            Btn4.Content = "4";
            Btn5.Content = "5";
            Btn6.Content = "6";
            Btn7.Content = "7";
            Btn8.Content = "8";
            Btn9.Content = "9";
            BtnComma.Content = ",";

            _isNumericKeyPadLocked = false;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            //calculate the new value
            Scalar ANS = probabilisticCalculator.ReadScalar(workingVariable);
            ANS.SetValue(0);
            probabilisticCalculator.WriteScalar(workingVariable, ANS);
            //update display
            NumericDisplay.Text = ANS.GetValue().ToString();
        }

        //meant comma (,) to be changed later!
        private void AddSemicolon(object sender, RoutedEventArgs e)
        {
            if (_isNumericKeyPadLocked)
                return;
            
            NumericDisplay.Text += ",";
            _isCommaJustClicked = true;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            LockOps();
            if (workingOperation != "")
                return;
            workingOperation = "+";
            workingVariable = "OPVAL";
            VarNameDisplay.Text = workingVariable;

        }

        private void Multiply(object sender, RoutedEventArgs e)
        {
            LockOps();
            if (workingOperation != "")
                return;
            workingOperation = "x";
            workingVariable = "OPVAL";
            VarNameDisplay.Text = workingVariable;
        }

        private void Subtract(object sender, RoutedEventArgs e)
        {
            LockOps();
            if (workingOperation != "")
                return;
            workingOperation = "-";
            workingVariable = "OPVAL";
            VarNameDisplay.Text = workingVariable;
        }

        private void Divide(object sender, RoutedEventArgs e)
        {
            LockOps();
            if (workingOperation != "")
                return;
            workingOperation = "/";
            workingVariable = "OPVAL";
            VarNameDisplay.Text = workingVariable;
        }

        private void SetWorkingVariable(object sender, RoutedEventArgs e)
        {
            FormSetWorkingVariable popup = new FormSetWorkingVariable(ref probabilisticCalculator, workingVariable);

            popup.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == "WorkingVariable")
                {
                    VarNameDisplay.Text = popup.WorkingVariable;
                    workingVariable = popup.WorkingVariable;
                }
            };

            this.Hide();
            popup.ShowDialog();
            this.Show();
            VarNameDisplay.Text = workingVariable;

            string type = probabilisticCalculator.GetDataKeys()[workingVariable];
            if (type == "SCALAR")
            {
                Scalar scalar = probabilisticCalculator.ReadScalar(workingVariable);
                NumericDisplay.Text = scalar.GetValue().ToString();
                UnlockNumericKeypad();
            }
            else
            {
                LockNumericKeypad();
            }

        }

        private void EditVariables(object sender, RoutedEventArgs e)
        {
            FormEditVariables popup = new FormEditVariables(ref probabilisticCalculator);
            this.Hide();
            popup.ShowDialog();
            this.Show();

            string type = probabilisticCalculator.GetDataKeys()[workingVariable];

            if (type == "SCALAR")
            {
                Scalar possiblyEditedScalar = probabilisticCalculator.ReadScalar(workingVariable);
                NumericDisplay.Text = possiblyEditedScalar.GetValue().ToString();

            }
            else
            {

            }

        }
    }
}
