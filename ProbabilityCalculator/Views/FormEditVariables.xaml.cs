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
        protected Calculator probabilityCalculator;
        private string _workingVariable;

        public FormEditVariables()
        {
            InitializeComponent();
        }
        public FormEditVariables(ref Calculator probabilisticCalculator)
        {
            this.probabilityCalculator = probabilisticCalculator;
            InitializeComponent();

            SelectVariablesGrid.ItemsSource = probabilisticCalculator.GetDataKeys();
        }

        private void OpenEditInterface(object sender, MouseButtonEventArgs e)
        {
            if (SelectVariablesGrid.SelectedItem is KeyValuePair<String, String> selection)
            {

                string name = selection.Key;
                if (name != "OPVAL")
                {
                    string type = probabilityCalculator.GetDataKeys()[name];
                    if (type == "SCALAR")
                    {
                        FormEditScalar scalarEditor = new FormEditScalar();
                        scalarEditor.ShowDialog();
                        Scalar editedScalar = probabilityCalculator.ReadScalar(name);
                        editedScalar.SetValue(scalarEditor.GetNumericValue());
                        probabilityCalculator.WriteScalar(name, editedScalar);
                    }
                    else
                    {
                        FormEditRandomQuantity randomQuantityEditor = new FormEditRandomQuantity(ref probabilityCalculator, name);
                        randomQuantityEditor.ShowDialog();

                    }

                }
                else
                    MessageBox.Show("Access to OPVAL is restricted");

            }
        }

        private void CreateVariable(object sender, RoutedEventArgs e)
        {
            string variableName = VariableCreatorVariableName.Text;
            if(variableName.Length == 0)
            {
                return;
            }

            if(RandomQuantityRadio.IsChecked == true)
            {
                probabilityCalculator.CreateRandomQuantity(variableName);
                FormEditRandomQuantity formEditRandomQuantity = new FormEditRandomQuantity(ref probabilityCalculator, variableName);
                formEditRandomQuantity.ShowDialog();
                SelectVariablesGrid.Items.Refresh();
            }

            if(ScalarRadio.IsChecked == true)
            {
                probabilityCalculator.CreateScalar(variableName);
                FormEditScalar formEditScalar = new FormEditScalar();
                formEditScalar.ShowDialog();

                Scalar createdScalar = probabilityCalculator.ReadScalar(variableName);
                createdScalar.SetValue(formEditScalar.GetNumericValue());
                probabilityCalculator.WriteScalar(variableName, createdScalar);

                SelectVariablesGrid.Items.Refresh();

            }
        }
    }
}
