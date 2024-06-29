using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProbabilityCalculator.ViewModels;



namespace ProbabilityCalculator.Views
{
    /// <summary>
    /// Interaction logic for FormEditRandomQuantity.xaml
    /// </summary>
    public partial class FormEditRandomQuantity : Window
    {
        private DataTable _realisationsTable;
        Calculator probabilisticCalculator;
        private string _randomQuantityName;
        public FormEditRandomQuantity()
        {
            InitializeComponent();

        }
        public FormEditRandomQuantity(ref Calculator probabilisticCalculator, string randomQuantityName)
        {
            InitializeComponent();
            RandomQuantity editedRandomQuantity = probabilisticCalculator.ReadRandomQuantity(randomQuantityName);
            this.probabilisticCalculator = probabilisticCalculator;
            this._randomQuantityName = randomQuantityName;

            Dictionary<decimal, decimal> realisationsDictionary = editedRandomQuantity.GetRealisations();
            _realisationsTable = new DataTable();

            _realisationsTable.Columns.Add("Key", typeof(decimal));
            _realisationsTable.Columns.Add("Value", typeof(decimal));

            foreach (var keyValue in realisationsDictionary)
            {
                DataRow row = _realisationsTable.NewRow();
                row["Key"] = keyValue.Key;
                row["Value"] = keyValue.Value;
                _realisationsTable.Rows.Add(row);
            }

            RandomQuantityDataGrid.ItemsSource = _realisationsTable.DefaultView;

            this.Closing += OnClosingSave;
        }

        private void OnClosingSave(object sender, System.ComponentModel.CancelEventArgs e)
        {

            bool isDataProbabilisticallyCorrect = true;

            decimal normalisationCondition = 0;

            Dictionary<decimal, decimal> realisations = new Dictionary<decimal, decimal>();

            foreach (DataRow row in _realisationsTable.Rows)
            {

                decimal rowProbability;
                decimal rowRealisation;

                bool isCorrectDecimal = true;

                isCorrectDecimal = decimal.TryParse(row["Value"].ToString(), out rowProbability);
                isDataProbabilisticallyCorrect &= isCorrectDecimal;
                isCorrectDecimal = decimal.TryParse(row["Key"].ToString(), out rowRealisation);
                isDataProbabilisticallyCorrect &= isCorrectDecimal;

                if (!isDataProbabilisticallyCorrect)
                    break;

                if (!realisations.ContainsKey(rowRealisation))
                {
                    realisations.Add(rowRealisation, rowProbability);
                }
                else
                {
                    isDataProbabilisticallyCorrect = false;

                }

                

                //from Kolmogorov axioms
                if(rowProbability < 0 || rowProbability > 1)
                {
                    isDataProbabilisticallyCorrect = false;
                }

                normalisationCondition += rowProbability;
                
            }

            if(normalisationCondition != 1)
            {
                isDataProbabilisticallyCorrect = false;
            }

            if (isDataProbabilisticallyCorrect)
            {
                RandomQuantity savedRandomQuantity = probabilisticCalculator.ReadRandomQuantity(_randomQuantityName);
                savedRandomQuantity.SetRealisations(realisations);
                probabilisticCalculator.WriteRandomQuantity(_randomQuantityName, savedRandomQuantity);

            }
            else {
                var result = MessageBox.Show("The data inputted is not probabilistically correct. Discard changes?", "Saving failed",
                                 MessageBoxButton.YesNo,
                                 MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }

        }
    }
}
