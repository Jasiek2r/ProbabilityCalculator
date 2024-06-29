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
        private DataTable _realizationsTable;
        private Calculator _probabilisticCalculator;
        private string _randomQuantityName;
        private const decimal PROPER_NORMALIZATION_CONDITION_VALUE = 1;

        public FormEditRandomQuantity()
        {
            InitializeComponent();

        }
        public FormEditRandomQuantity(ref Calculator probabilisticCalculator, string randomQuantityName) : this()
        {
            RandomQuantity editedRandomQuantity = probabilisticCalculator.ReadRandomQuantity(randomQuantityName);
            this._probabilisticCalculator = probabilisticCalculator;
            this._randomQuantityName = randomQuantityName;

            InitializeRealizationsTable(editedRandomQuantity);
            InitializeDataGrid();
            

            this.Closing += OnClosingSave;
        }

        private void InitializeDataGrid()
        {
            RandomQuantityDataGrid.ItemsSource = _realizationsTable.DefaultView;
        }

        private void InitializeRealizationsTable(RandomQuantity editedRandomQuantity)
        {
            _realizationsTable = new DataTable();

            _realizationsTable.Columns.Add("Key", typeof(decimal));
            _realizationsTable.Columns.Add("Value", typeof(decimal));

            foreach (var keyValue in editedRandomQuantity.GetRealizations())
            {
                DataRow row = _realizationsTable.NewRow();
                row["Key"] = keyValue.Key;
                row["Value"] = keyValue.Value;
                _realizationsTable.Rows.Add(row);
            }
        }


        private void OnClosingSave(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool isDataProbabilisticallyCorrect = ValidateRealizationsTable();

            if (isDataProbabilisticallyCorrect)
            {
                SaveRandomQuantity();
            }
            else
            {
                HandleInvalidData(e);
            }
        }

        private bool ValidateRealizationsTable()
        {
            decimal normalisationCondition = 0;
            Dictionary<decimal, decimal> realizations = new Dictionary<decimal, decimal>();

            foreach (DataRow row in _realizationsTable.Rows)
            {
                if (!Decimal.TryParse(row["Value"].ToString(), out decimal rowProbability) ||
                    !Decimal.TryParse(row["Key"].ToString(), out decimal rowRealisation))
                    return false;


                if (realizations.ContainsKey(rowRealisation))
                    return false;

                if (rowProbability < 0 || rowProbability > 1)
                    return false;

                normalisationCondition += rowProbability;
            }

            if (normalisationCondition != PROPER_NORMALIZATION_CONDITION_VALUE)
                return false;

            return true;
        }

        private void SaveRandomQuantity()
        {
            RandomQuantity savedRandomQuantity = _probabilisticCalculator.ReadRandomQuantity(_randomQuantityName);
            Dictionary<decimal, decimal> realizations = new Dictionary<decimal, decimal>();

            foreach (DataRow row in _realizationsTable.Rows)
            {
                decimal.TryParse(row["Value"].ToString(), out decimal rowProbability);
                decimal.TryParse(row["Key"].ToString(), out decimal rowRealisation);

                realizations.Add(rowRealisation, rowProbability);
            }

            savedRandomQuantity.SetRealizations(realizations);
            _probabilisticCalculator.WriteRandomQuantity(_randomQuantityName, savedRandomQuantity);
        }

        private void HandleInvalidData(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("The data inputted is not probabilistically correct. Discard changes?", "Saving failed",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                e.Cancel = true;
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _realizationsTable.Dispose();
        }
    }
}
