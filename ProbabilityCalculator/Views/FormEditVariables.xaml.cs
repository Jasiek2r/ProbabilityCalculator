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
using ProbabilityCalculator.ViewModels;


namespace ProbabilityCalculator.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FormEditVariables : Window
    {
        private Calculator _probabilisticCalculator;
        private string _workingVariable;

        public FormEditVariables()
        {
            InitializeComponent();
        }
        public FormEditVariables(ref Calculator probabilisticCalculator) : this()
        {
            this._probabilisticCalculator = probabilisticCalculator;
            SelectVariablesGrid.ItemsSource = probabilisticCalculator.GetDataKeys();
        }

        private void OpenEditInterface(object sender, MouseButtonEventArgs e)
        {
            if (SelectVariablesGrid.SelectedItem is KeyValuePair<String, String> selection)
            {

                string name = selection.Key;
                if (name != "OPVAL")
                {
                    string type = _probabilisticCalculator.GetDataKey(name);
                    if (type == "SCALAR")
                        EditScalarVariable(name);
                    
                    else
                        EditRandomQuantityVariable(name);
                        

                }
                else
                    MessageBox.Show("Access to OPVAL is restricted");

            }
        }

        private void EditScalarVariable(string name)
        {
            FormEditScalar scalarEditor = new FormEditScalar();
            scalarEditor.ShowDialog();
            Scalar editedScalar = _probabilisticCalculator.ReadScalar(name);
            editedScalar.SetValue(scalarEditor.GetNumericValue());
            _probabilisticCalculator.WriteScalar(name, editedScalar);
        }

        private void EditRandomQuantityVariable(string name)
        {
            FormEditRandomQuantity randomQuantityEditor = new FormEditRandomQuantity(ref _probabilisticCalculator, name);
            randomQuantityEditor.ShowDialog();
        }

        private void CreateVariable(object sender, RoutedEventArgs e)
        {
            string variableName = VariableCreatorVariableName.Text;
            if(variableName.Length == 0 || _probabilisticCalculator.VariableExists(variableName))
            {
                return;
            }

            if(RandomQuantityRadio.IsChecked == true)
            {
                CreateRandomQuantityVariable(variableName);
            }

            if(ScalarRadio.IsChecked == true)
            {
                CreateScalarVariable(variableName);
            }
            SelectVariablesGrid.Items.Refresh();
        }

        private void CreateRandomQuantityVariable(string variableName)
        {
            _probabilisticCalculator.CreateRandomQuantity(variableName);
            EditRandomQuantityVariable(variableName);
        }

        private void CreateScalarVariable(string variableName)
        {
            _probabilisticCalculator.CreateScalar(variableName);
            EditScalarVariable(variableName);
        }
    }
}
