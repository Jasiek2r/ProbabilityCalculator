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

        private String GetVariableTypes(string operand1Name, string operand2Name)
        {
            string variable1Type = probabilisticCalculator.GetDataKey(operand1Name);
            string variable2Type = probabilisticCalculator.GetDataKey(operand2Name);

            if (variable1Type == "SCALAR" && variable2Type == "SCALAR")
                return "scalars";
            else if (variable1Type == "RANDOM QUANTITY" && variable2Type == "SCALAR")
                return "randomQuantityAndScalar";
            else if (variable1Type == "SCALAR" && variable2Type == "RANDOM QUANTITY")
                return "scalarAndRandomQuantity";
            else
                return "randomQuantities";
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            UnlockOps();

            string variableDataTypes = GetVariableTypes(_operand1, _workingVariable);

            switch (_workingOperation)
            {
                case "+":
                    probabilisticCalculator.Add(variableDataTypes, _operand1, _workingVariable);
                    break;
                case "x":
                    probabilisticCalculator.Multiply(variableDataTypes, _operand1, _workingVariable);
                    break;
                case "-":
                    probabilisticCalculator.Subtract(variableDataTypes, _operand1, _workingVariable);
                    break;
                case "/":
                    probabilisticCalculator.Divide(variableDataTypes, _operand1, _workingVariable);
                    break;
                default:
                    break;
            }

            _workingOperation = "";
            _workingVariable = _operand1;
            VarNameDisplay.Text = _workingVariable;

            if(variableDataTypes == "scalars" || variableDataTypes == "scalarAndRandomQuantity")
            {
                Scalar answerScalar = probabilisticCalculator.ReadScalar(_workingVariable);
                NumericDisplay.Text = answerScalar.GetValue().ToString();
                probabilisticCalculator.ResetScalar("OPVAL");
            }


            if (variableDataTypes == "randomQuantities" || variableDataTypes == "randomQuantityAndScalar")
                LockNumericKeypad();
            else
                UnlockNumericKeypad();
            

        }

        private bool _isCommaJustClicked = false;

        private string _workingVariable = "ANS";
        private string _operand1 = "ANS";
        string _workingOperation = "";

        private bool _isNumericKeyPadLocked = false;

        private void AddNumber(int number)
        {

            if (_isNumericKeyPadLocked)
            {
                return;
            }
            //calculate the new value
            Scalar answerScalar = probabilisticCalculator.ReadScalar(_workingVariable);
            answerScalar.AppendDigit(number);
            if (_isCommaJustClicked)
            {
                _isCommaJustClicked = false;
                decimal ansValue = answerScalar.GetValue();
                ansValue = ansValue / 10;
                answerScalar.SetValue(ansValue);
                answerScalar.SetHasDecimalPart(true);
            }
            probabilisticCalculator.WriteScalar(_workingVariable, answerScalar);
            //update display
            NumericDisplay.Text = answerScalar.GetValue().ToString();
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
            if (_isNumericKeyPadLocked)
                return;
            //calculate the new value
            Scalar editedScalar = probabilisticCalculator.ReadScalar(_workingVariable);
            editedScalar.PopDigit();

            if (editedScalar.GetValue() % 1 == 0)
            {
                editedScalar.SetHasDecimalPart(false);
                _isCommaJustClicked = false;
            }
                

            probabilisticCalculator.WriteScalar(_workingVariable, editedScalar);
            //update display
            NumericDisplay.Text = editedScalar.GetValue().ToString();
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
            BtnDEL.Content = "";
            BtnCLR.Content = "";

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
            BtnCLR.Content = "CLR";
            BtnDEL.Content = "DEL";
            BtnComma.Content = ",";

            _isNumericKeyPadLocked = false;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {

            if(_isNumericKeyPadLocked)
                return;
            
            //calculate the new value
            Scalar answerScalar = probabilisticCalculator.ReadScalar(_workingVariable);
            answerScalar.SetValue(0);
            probabilisticCalculator.WriteScalar(_workingVariable, answerScalar);
            //update display
            NumericDisplay.Text = answerScalar.GetValue().ToString();
        }

        private void AddComma(object sender, RoutedEventArgs e)
        {
            if (_isNumericKeyPadLocked)
                return;

            string dataKey = probabilisticCalculator.GetDataKey(_workingVariable);

            if (dataKey != "SCALAR")
                return;

            Scalar scalar = probabilisticCalculator.ReadScalar(_workingVariable);
            bool doesScalarHaveDecimalPart = scalar.GetHasDecimalPart();

            if (doesScalarHaveDecimalPart)
                return;
            
            NumericDisplay.Text += ",";
            _isCommaJustClicked = true;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            LockOps();
            UnlockNumericKeypad();
            if (_workingOperation != "")
                return;
            _workingOperation = "+";
            _workingVariable = "OPVAL";
            VarNameDisplay.Text = _workingVariable;

        }

        private void Multiply(object sender, RoutedEventArgs e)
        {
            LockOps();
            UnlockNumericKeypad();
            if (_workingOperation != "")
                return;
            _workingOperation = "x";
            _workingVariable = "OPVAL";
            VarNameDisplay.Text = _workingVariable;
        }

        private void Subtract(object sender, RoutedEventArgs e)
        {
            LockOps();
            UnlockNumericKeypad();
            if (_workingOperation != "")
                return;
            _workingOperation = "-";
            _workingVariable = "OPVAL";
            VarNameDisplay.Text = _workingVariable;
        }

        private void Divide(object sender, RoutedEventArgs e)
        {
            LockOps();
            UnlockNumericKeypad();
            if (_workingOperation != "")
                return;
            _workingOperation = "/";
            _workingVariable = "OPVAL";
            VarNameDisplay.Text = _workingVariable;
        }

        private void SetWorkingVariable(object sender, RoutedEventArgs e)
        {
            FormSetWorkingVariable popup = new FormSetWorkingVariable(ref probabilisticCalculator, _workingVariable);

            popup.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == "_workingVariable")
                {
                    VarNameDisplay.Text = popup.GetWorkingVariable();
                    _workingVariable = popup.GetWorkingVariable();
                    if(_workingOperation == "")
                        _operand1 = popup.GetWorkingVariable();
                }
            };

            Hide();
            popup.ShowDialog();
            Show();
            _workingVariable = popup.GetWorkingVariable();
            _operand1 = popup.GetWorkingVariable();
            VarNameDisplay.Text = _workingVariable;
            

            string type = probabilisticCalculator.GetDataKey(_workingVariable);
            if (type == "SCALAR")
            {
                Scalar scalar = probabilisticCalculator.ReadScalar(_workingVariable);
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

            string type = probabilisticCalculator.GetDataKey(_workingVariable);

            if (type == "SCALAR")
            {
                Scalar possiblyEditedScalar = probabilisticCalculator.ReadScalar(_workingVariable);
                NumericDisplay.Text = possiblyEditedScalar.GetValue().ToString();

            }

        }

        private void CalculateExpectedValue(object sender, RoutedEventArgs e)
        {
            string dataKey = probabilisticCalculator.GetDataKey(_workingVariable);

            if (dataKey == "RANDOM QUANTITY")
            {
                RandomQuantity randomQuantity = probabilisticCalculator.ReadRandomQuantity(_workingVariable);

                decimal expectedValue = randomQuantity.ComputeExpectedValue();

                probabilisticCalculator.WriteScalar("ANS", new Scalar(expectedValue));

                NumericDisplay.Text = expectedValue.ToString();

                _workingVariable = "ANS";
                _workingOperation = "ANS";
                VarNameDisplay.Text = "ANS";

                UnlockNumericKeypad();


            }
            else
            {
                MessageBox.Show("Expected value calculation not supported for this data type.");
            }
        }

        private void CalculateVariance(object sender, RoutedEventArgs e)
        {
            string dataKey = probabilisticCalculator.GetDataKey(_workingVariable);

            if (dataKey == "RANDOM QUANTITY")
            {
                RandomQuantity randomQuantity = probabilisticCalculator.ReadRandomQuantity(_workingVariable);

                decimal expectedValue = randomQuantity.ComputeVariance();

                probabilisticCalculator.WriteScalar("ANS", new Scalar(expectedValue));

                NumericDisplay.Text = expectedValue.ToString();

                _workingVariable = "ANS";
                _workingOperation = "ANS";
                VarNameDisplay.Text = "ANS";

                UnlockNumericKeypad();


            }
            else
            {
                // Handle other cases or throw an error if needed
                MessageBox.Show("Variance calculation not supported for this data type.");
            }
        }
    }
}
