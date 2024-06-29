using ProbabilityCalculator.ViewModels;
using ProbabilityCalculator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for FormSetWorkingVariable.xaml
    /// </summary>
    public partial class FormSetWorkingVariable : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    
        private Calculator _probabilityCalculator;
        private string _workingVariable;

        public string WorkingVariable
        {
            get { return _workingVariable; }
            set
            {
                if (_workingVariable != value)
                {
                    _workingVariable = value;
                    OnPropertyChanged(nameof(WorkingVariable));
                }
            }
        }

        public FormSetWorkingVariable()
        {
            InitializeComponent();
        }
        public FormSetWorkingVariable(ref Calculator probabilisticCalculator, string workingVariable) : this()
        {
            this._probabilityCalculator = probabilisticCalculator;
            SelectVariablesGrid.ItemsSource = probabilisticCalculator.GetDataKeys();
            this._workingVariable = workingVariable;
        }

        private void SelectWorkingVariable(object sender, MouseButtonEventArgs e)
        {
            if (SelectVariablesGrid.SelectedItem is KeyValuePair<String, String> selection)
            {
                string name = selection.Key;
                if (name != "OPVAL")
                {
                    WorkingVariable = selection.Key;
                    Close();
                }
                else
                    MessageBox.Show("Access to OPVAL is restricted");
                
            }
            else
            {
                MessageBox.Show("No item selected or selected item is not valid.");
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetWorkingVariable()
        {
            return _workingVariable;
        }
    }
}
