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

        protected Dictionary<String,Scalar> scalars;
        protected Dictionary<String,RandomQuantity> randomQuantities;
        protected Dictionary<String, String> dataKeys;

        
        public Calculator() { 
            scalars = new Dictionary<String, Scalar>();
            randomQuantities = new Dictionary<String, RandomQuantity>();
            dataKeys = new Dictionary<String, String>();

            CreateScalar("ANS");
            CreateScalar("OPVAL");
            CreateRandomQuantity("X");
        }

        public bool VariableExists(String identifier)
        {
            return dataKeys.ContainsKey(identifier);
        }

        public Scalar ReadScalar(String identifier)
        {
            return scalars[identifier];
        }

        public void WriteScalar(String key, Scalar value)
        {
            scalars[key] = value;
        }

        public void CreateScalar(String identifier)
        {
            scalars.Add(identifier, new Scalar());
            dataKeys.Add(identifier, "SCALAR");
        }

        public void ResetScalar(String key)
        {
            scalars[key] = new Scalar();
        }

        public Dictionary<String, Scalar> GetScalars()
        {
            return scalars;
        }
        public Dictionary<String, RandomQuantity> GetRandomQuantities()
        {
            return randomQuantities;
        }

        public String GetDataKey(String identifier)
        {
            return dataKeys[identifier];
        }

        public Dictionary<String, String> GetDataKeys()
        {
            return dataKeys;
        }

        public RandomQuantity ReadRandomQuantity(String identifier)
        {
            return randomQuantities[identifier];
        }

        public void WriteRandomQuantity(String key,  RandomQuantity value)
        {
            randomQuantities[key] = value;
        }

        public void CreateRandomQuantity(String identifier)
        {
            randomQuantities.Add(identifier, new RandomQuantity());
            dataKeys.Add(identifier, "RANDOM QUANTITY");
        }

        public void Add(String identifier, String operand1Name, String operand2Name)
        {
            if(identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars[operand1Name].SetValue(scalar1Value + scalar2Value);
            }
            else if(identifier == "randomQuantityAndScalar")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                decimal scalar2Value = scalars[operand2Name].GetValue();

                Dictionary<decimal, decimal> oldRealisations = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

                foreach (KeyValuePair<decimal, decimal> realisation in oldRealisations)
                {
                    newRealisations.Add(realisation.Key + scalar2Value, realisation.Value);
                }
                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }
            else if(identifier == "scalarAndRandomQuantity")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                decimal randomQuantity2Value = randomQuantity2.Realise();

                scalars[operand1Name].SetValue(scalar1Value + randomQuantity2Value);

            }
            else if (identifier == "randomQuantities")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                Dictionary<decimal, decimal> realisations1 = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> realisations2 = randomQuantity2.GetRealisations();
                Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

                foreach (KeyValuePair<decimal, decimal> realisation1 in realisations1)
                {
                    foreach (KeyValuePair<decimal, decimal> realisation2 in realisations2)
                    {
                        decimal newRealisationValue = realisation1.Key + realisation2.Key;
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

                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }

            
        }
        public void Multiply(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars[operand1Name].SetValue(scalar1Value * scalar2Value);
            }
            else if (identifier == "randomQuantityAndScalar")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                decimal scalar2Value = scalars[operand2Name].GetValue();

                Dictionary<decimal, decimal> oldRealisations = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

                foreach (KeyValuePair<decimal, decimal> realisation in oldRealisations)
                {
                    newRealisations.Add(realisation.Key * scalar2Value, realisation.Value);
                }
                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }
            else if (identifier == "scalarAndRandomQuantity")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                decimal randomQuantity2Value = randomQuantity2.Realise();

                scalars[operand1Name].SetValue(scalar1Value * randomQuantity2Value);
            }
            else if (identifier == "randomQuantities")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                Dictionary<decimal, decimal> realisations1 = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> realisations2 = randomQuantity2.GetRealisations();
                Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

                foreach (KeyValuePair<decimal, decimal> realisation1 in realisations1)
                {
                    foreach (KeyValuePair<decimal, decimal> realisation2 in realisations2)
                    {
                        decimal newRealisationValue = realisation1.Key * realisation2.Key;
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

                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }

        }

        public void Subtract(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars[operand1Name].SetValue(scalar1Value - scalar2Value);
            }
            else if (identifier == "randomQuantityAndScalar")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                decimal scalar2Value = scalars[operand2Name].GetValue();

                Dictionary<decimal, decimal> oldRealisations = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

                foreach (KeyValuePair<decimal, decimal> realisation in oldRealisations)
                {
                    newRealisations.Add(realisation.Key - scalar2Value, realisation.Value);
                }
                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }
            else if (identifier == "scalarAndRandomQuantity")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                decimal randomQuantity2Value = randomQuantity2.Realise();

                scalars[operand1Name].SetValue(scalar1Value - randomQuantity2Value);
            }
            else if (identifier == "randomQuantities")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                Dictionary<decimal, decimal> realisations1 = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> realisations2 = randomQuantity2.GetRealisations();
                Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

                foreach (KeyValuePair<decimal, decimal> realisation1 in realisations1)
                {
                    foreach (KeyValuePair<decimal, decimal> realisation2 in realisations2)
                    {
                        decimal newRealisationValue = realisation1.Key - realisation2.Key;
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

                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }

        }
        public void Divide(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                if (scalar2Value == 0)
                {
                    MessageBox.Show("Cannot divide by zero. The app will throw an exception after closing this window.");
                    throw new DivideByZeroException("Cannot divide by zero.");
                }

                scalars[operand1Name].SetValue(scalar1Value / scalar2Value);
            }
            else if (identifier == "randomQuantityAndScalar")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                decimal scalar2Value = scalars[operand2Name].GetValue();

                if (scalar2Value == 0)
                {
                    MessageBox.Show("Cannot divide by zero. The app will throw an exception after closing this window.");
                    throw new DivideByZeroException("Cannot divide by zero.");
                }

                Dictionary<decimal, decimal> oldRealisations = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> newRealisations = new Dictionary<decimal, decimal>();

                foreach (KeyValuePair<decimal, decimal> realisation in oldRealisations)
                {
                    newRealisations.Add(realisation.Key / scalar2Value, realisation.Value);
                }
                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }
            else if (identifier == "scalarAndRandomQuantity")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                decimal randomQuantity2Value = randomQuantity2.Realise();

                if (randomQuantity2Value == 0)
                {
                    MessageBox.Show("Cannot divide by zero. The app will throw an exception after closing this window.");
                    throw new DivideByZeroException("Cannot divide by zero.");
                }

                scalars[operand1Name].SetValue(scalar1Value / randomQuantity2Value);
            }
            else if (identifier == "randomQuantities")
            {
                RandomQuantity randomQuantity1 = ReadRandomQuantity(operand1Name);
                RandomQuantity randomQuantity2 = ReadRandomQuantity(operand2Name);

                Dictionary<decimal, decimal> realisations1 = randomQuantity1.GetRealisations();
                Dictionary<decimal, decimal> realisations2 = randomQuantity2.GetRealisations();
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

                randomQuantity1.SetRealisations(newRealisations);
                WriteRandomQuantity(operand1Name, randomQuantity1);
            }
        }


    }
}
