using ProbabilityCalculator.ViewModels;
using ProbabilityCalculator.Views;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProbabilityCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Calculator probabilisticCalculator = new Calculator();
        public MainWindow()
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

        private void AddNumber(Int32 number)
        {
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

        private void Clear(object sender, RoutedEventArgs e)
        {
            //calculate the new value
            Scalar ANS = probabilisticCalculator.ReadScalar(workingVariable);
            ANS.SetValue(0);
            probabilisticCalculator.WriteScalar(workingVariable, ANS);
            //update display
            NumericDisplay.Text = ANS.GetValue().ToString();
        }

        private void AddSemicolon(object sender, RoutedEventArgs e)
        {
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
            Window1 popup = new Window1(ref probabilisticCalculator, workingVariable);
            
            popup.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == "WorkingVariable")
                {
                    VarNameDisplay.Text = popup.WorkingVariable;
                    workingVariable = popup.WorkingVariable;
                }
            };
            popup.ShowDialog();
            VarNameDisplay.Text = workingVariable;

            string type = probabilisticCalculator.GetDataKeys()[workingVariable];
            if(type == "SCALAR")
            {
                Scalar scalar = probabilisticCalculator.ReadScalar(workingVariable);
                NumericDisplay.Text = scalar.GetValue().ToString();
            }

        }
    }
}