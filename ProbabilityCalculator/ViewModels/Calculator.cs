using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace ProbabilityCalculator.ViewModels
{
    public class Calculator
    {

        private Dictionary<String,Scalar> _scalars;
        private Dictionary<String,RandomQuantity> _randomQuantities;
        private Dictionary<String, String> _dataKeys;

        
        public Calculator() {
            _scalars = new Dictionary<String, Scalar>();
            _randomQuantities = new Dictionary<String, RandomQuantity>();
            _dataKeys = new Dictionary<String, String>();

            InitializeStartupVariables();
        }

        private void InitializeStartupVariables()
        {
            CreateScalar("ANS");
            CreateScalar("OPVAL");
            CreateRandomQuantity("X");
        }

        public bool VariableExists(String identifier)
        {
            return _dataKeys.ContainsKey(identifier);
        }

        public Scalar ReadScalar(String identifier)
        {
            return _scalars[identifier];
        }

        public void WriteScalar(String key, Scalar value)
        {
            _scalars[key] = value;
        }

        public void CreateScalar(String identifier)
        {
            _scalars.Add(identifier, new Scalar());
            _dataKeys.Add(identifier, "SCALAR");
        }

        public void ResetScalar(String key)
        {
            _scalars[key] = new Scalar();
        }

        public Dictionary<String, Scalar> GetScalars()
        {
            return _scalars;
        }
        public Dictionary<String, RandomQuantity> GetRandomQuantities()
        {
            return _randomQuantities;
        }

        public String GetDataKey(String identifier)
        {
            return _dataKeys[identifier];
        }

        public Dictionary<String, String> GetDataKeys()
        {
            return _dataKeys;
        }

        public RandomQuantity ReadRandomQuantity(String identifier)
        {
            return _randomQuantities[identifier];
        }

        public void WriteRandomQuantity(String key,  RandomQuantity value)
        {
            _randomQuantities[key] = value;
        }

        public void CreateRandomQuantity(String identifier)
        {
            _randomQuantities.Add(identifier, new RandomQuantity());
            _dataKeys.Add(identifier, "RANDOM QUANTITY");
        }

        #region Operations

        public void Add(string identifier, string operand1Name, string operand2Name)
        {
            switch (identifier)
            {
                case "scalars":
                    PerformScalarOperation(operand1Name, operand2Name, (x, y) => x + y);
                    break;
                case "randomQuantityAndScalar":
                    PerformRandomQuantityAndScalarOperation(operand1Name, operand2Name, (x, y) => x + y);
                    break;
                case "scalarAndRandomQuantity":
                    PerformScalarAndRandomQuantityOperation(operand1Name, operand2Name, (x, y) => x + y);
                    break;
                case "randomQuantities":
                    PerformRandomQuantitiesOperation(operand1Name, operand2Name, (x, y) => x + y);
                    break;
                default:
                    throw new ArgumentException("Unsupported operation identifier.");
            }
        }

        public void Multiply(string identifier, string operand1Name, string operand2Name)
        {
            switch (identifier)
            {
                case "scalars":
                    PerformScalarOperation(operand1Name, operand2Name, (x, y) => x * y);
                    break;
                case "randomQuantityAndScalar":
                    PerformRandomQuantityAndScalarOperation(operand1Name, operand2Name, (x, y) => x * y);
                    break;
                case "scalarAndRandomQuantity":
                    PerformScalarAndRandomQuantityOperation(operand1Name, operand2Name, (x, y) => x * y);
                    break;
                case "randomQuantities":
                    PerformRandomQuantitiesOperation(operand1Name, operand2Name, (x, y) => x * y);
                    break;
                default:
                    throw new ArgumentException("Unsupported operation identifier.");
            }
        }

        public void Subtract(string identifier, string operand1Name, string operand2Name)
        {
            switch (identifier)
            {
                case "scalars":
                    PerformScalarOperation(operand1Name, operand2Name, (x, y) => x - y);
                    break;
                case "randomQuantityAndScalar":
                    PerformRandomQuantityAndScalarOperation(operand1Name, operand2Name, (x, y) => x - y);
                    break;
                case "scalarAndRandomQuantity":
                    PerformScalarAndRandomQuantityOperation(operand1Name, operand2Name, (x, y) => x - y);
                    break;
                case "randomQuantities":
                    PerformRandomQuantitiesOperation(operand1Name, operand2Name, (x, y) => x - y);
                    break;
                default:
                    throw new ArgumentException("Unsupported operation identifier.");
            }
        }

        public void Divide(string identifier, string operand1Name, string operand2Name)
        {
            switch (identifier)
            {
                case "scalars":
                    PerformScalarDivision(operand1Name, operand2Name);
                    break;
                case "randomQuantityAndScalar":
                    PerformRandomQuantityAndScalarDivision(operand1Name, operand2Name);
                    break;
                case "scalarAndRandomQuantity":
                    PerformScalarAndRandomQuantityDivision(operand1Name, operand2Name);
                    break;
                case "randomQuantities":
                    PerformRandomQuantitiesDivision(operand1Name, operand2Name);
                    break;
                default:
                    throw new ArgumentException("Unsupported operation identifier.");
            }
        }

        #endregion

        #region Helper Methods for Operations

        private void PerformScalarOperation(string operand1Name, string operand2Name, Func<decimal, decimal, decimal> operation)
        {
            decimal scalar1Value = _scalars[operand1Name].GetValue();
            decimal scalar2Value = _scalars[operand2Name].GetValue();

            _scalars[operand1Name].SetValue(operation(scalar1Value, scalar2Value));
        }

        private void PerformRandomQuantityAndScalarOperation(string operand1Name, string operand2Name, Func<decimal, decimal, decimal> operation)
        {
            RandomQuantity randomQuantity = ReadRandomQuantity(operand1Name);
            decimal scalarValue = _scalars[operand2Name].GetValue();

            Dictionary<decimal, decimal> oldRealisations = randomQuantity.GetRealizations();
            Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

            foreach (KeyValuePair<decimal, decimal> realisation in oldRealisations)
            {
                newRealisations.Add(operation(realisation.Key, scalarValue), realisation.Value);
            }

            randomQuantity.SetRealizations(newRealisations);
            WriteRandomQuantity(operand1Name, randomQuantity);
        }

        private void PerformScalarAndRandomQuantityOperation(string operand1Name, string operand2Name, Func<decimal, decimal, decimal> operation)
        {
            decimal scalarValue = _scalars[operand1Name].GetValue();
            RandomQuantity randomQuantity = ReadRandomQuantity(operand2Name);

            decimal randomQuantityValue = randomQuantity.Realize();

            _scalars[operand1Name].SetValue(operation(scalarValue, randomQuantityValue));
        }

        private void PerformRandomQuantitiesOperation(string operand1Name, string operand2Name, Func<decimal, decimal, decimal> operation)
        {
            RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
            RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

            Dictionary<decimal, decimal> realisations1 = randomQuantity1.GetRealizations();
            Dictionary<decimal, decimal> realisations2 = randomQuantity2.GetRealizations();
            Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

            foreach (KeyValuePair<decimal, decimal> realisation1 in realisations1)
            {
                foreach (KeyValuePair<decimal, decimal> realisation2 in realisations2)
                {
                    decimal newRealisationValue = operation(realisation1.Key, realisation2.Key);
                    decimal newProbability = realisation1.Value * realisation2.Value;

                    if (newRealisations.ContainsKey(newRealisationValue))
                    {
                        newRealisations[newRealisationValue] += newProbability;
                    }
                    else
                    {
                        newRealisations.Add(newRealisationValue, newProbability);
                    }
                }
            }

            randomQuantity1.SetRealizations(newRealisations);
            WriteRandomQuantity(operand1Name, randomQuantity1);
        }

        private void PerformScalarDivision(string operand1Name, string operand2Name)
        {
            decimal scalar1Value = _scalars[operand1Name].GetValue();
            decimal scalar2Value = _scalars[operand2Name].GetValue();

            if (scalar2Value == 0)
            {
                MessageBox.Show("Cannot divide by zero. The app will throw an exception after closing this window.");
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            _scalars[operand1Name].SetValue(scalar1Value / scalar2Value);
        }

        private void PerformRandomQuantityAndScalarDivision(string operand1Name, string operand2Name)
        {
            RandomQuantity randomQuantity = ReadRandomQuantity(operand1Name);
            decimal scalarValue = _scalars[operand2Name].GetValue();

            if (scalarValue == 0)
            {
                MessageBox.Show("Cannot divide by zero. The app will throw an exception after closing this window.");
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            Dictionary<decimal, decimal> oldRealisations = randomQuantity.GetRealizations();
            Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

            foreach (KeyValuePair<decimal, decimal> realisation in oldRealisations)
            {
                newRealisations.Add(realisation.Key / scalarValue, realisation.Value);
            }

            randomQuantity.SetRealizations(newRealisations);
            WriteRandomQuantity(operand1Name, randomQuantity);
        }

        private void PerformScalarAndRandomQuantityDivision(string operand1Name, string operand2Name)
        {
            decimal scalarValue = _scalars[operand1Name].GetValue();
            RandomQuantity randomQuantity = ReadRandomQuantity(operand2Name);

            decimal randomQuantityValue = randomQuantity.Realize();

            if (randomQuantityValue == 0)
            {
                MessageBox.Show("Cannot divide by zero. The app will throw an exception after closing this window.");
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            _scalars[operand1Name].SetValue(scalarValue / randomQuantityValue);
        }

        private void PerformRandomQuantitiesDivision(string operand1Name, string operand2Name)
        {
            RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
            RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

            Dictionary<decimal, decimal> realisations1 = randomQuantity1.GetRealizations();
            Dictionary<decimal, decimal> realisations2 = randomQuantity2.GetRealizations();
            Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

            foreach (KeyValuePair<decimal, decimal> realisation1 in realisations1)
            {
                foreach (KeyValuePair<decimal, decimal> realisation2 in realisations2)
                {
                    if (realisation2.Key == 0)
                    {
                        MessageBox.Show("Cannot divide by zero in random quantities. The exception will be thrown after closing this window.");
                        throw new DivideByZeroException("Cannot divide by zero in random quantities.");
                    }

                    decimal newRealisationValue = realisation1.Key / realisation2.Key;
                    decimal newProbability = realisation1.Value * realisation2.Value;

                    if (newRealisations.ContainsKey(newRealisationValue))
                    {
                        newRealisations[newRealisationValue] += newProbability;
                    }
                    else
                    {
                        newRealisations.Add(newRealisationValue, newProbability);
                    }
                }
            }

            randomQuantity1.SetRealizations(newRealisations);
            WriteRandomQuantity(operand1Name, randomQuantity1);
        }

        #endregion


    }
}
