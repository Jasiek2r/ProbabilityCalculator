using ProbabilityCalculator.ViewModels;
using System.Text;
using System.Windows;
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
            
        }

        private bool isCommaJustClicked = false;

        private void addNumber(Int32 number)
        {
            //calculate the new value
            Scalar ANS = probabilisticCalculator.readScalar("ANS");
            ANS.appendDigit(number);
            if (isCommaJustClicked)
            {
                isCommaJustClicked = false;
                decimal ansValue = ANS.getValue();
                ansValue = ansValue / 10;
                ANS.setValue(ansValue);
                ANS.setHasDecimalPart(true);
            }
            probabilisticCalculator.writeScalar("ANS",ANS);
            //update display
            NumericDisplay.Text = ANS.getValue().ToString();
        }

        private void Add1(object sender, RoutedEventArgs e)
        {
            addNumber(1);
        }

        private void Add2(object sender, RoutedEventArgs e)
        {
            addNumber(2);
        }

        private void Add3(object sender, RoutedEventArgs e)
        {
            addNumber(3);
        }

        private void Add4(object sender, RoutedEventArgs e)
        {
            addNumber(4);
        }

        private void Add5(object sender, RoutedEventArgs e)
        {
            addNumber(5);
        }

        private void Add6(object sender, RoutedEventArgs e)
        {
            addNumber(6);
        }

        private void Add7(object sender, RoutedEventArgs e)
        {
            addNumber(7);
        }

        private void Add8(object sender, RoutedEventArgs e)
        {
            addNumber(8);
        }

        private void Add9(object sender, RoutedEventArgs e)
        {
            addNumber(9);
        }

        private void DelNumber(object sender, RoutedEventArgs e)
        {
            //calculate the new value
            Scalar ANS = probabilisticCalculator.readScalar("ANS");
            ANS.popDigit();
            probabilisticCalculator.writeScalar("ANS", ANS);
            //update display
            NumericDisplay.Text = ANS.getValue().ToString();
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            //calculate the new value
            Scalar ANS = probabilisticCalculator.readScalar("ANS");
            ANS.setValue(0);
            probabilisticCalculator.writeScalar("ANS", ANS);
            //update display
            NumericDisplay.Text = ANS.getValue().ToString();
        }

        private void AddSemicolon(object sender, RoutedEventArgs e)
        {
            NumericDisplay.Text += ",";
            isCommaJustClicked = true;
        }
    }
}