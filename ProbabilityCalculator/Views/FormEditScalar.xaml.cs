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
    /// Interaction logic for FormEditScalar.xaml
    /// </summary>
    public partial class FormEditScalar : Window
    {
        private decimal _value = 0;
        public FormEditScalar()
        {
            InitializeComponent();
        }

        public decimal GetNumericValue()
        {
            return _value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textValue = NumericValue.Text;
            if (Decimal.TryParse(textValue, out _value))
                this.Close();
            else
                MessageBox.Show("Incorrect input");
        }
    }
}
