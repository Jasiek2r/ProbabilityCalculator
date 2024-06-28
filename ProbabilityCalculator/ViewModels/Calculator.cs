using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            
        }
        public void Multiply(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars[operand1Name].SetValue(scalar1Value * scalar2Value);
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
            
        }
        public void Divide(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars[operand1Name].SetValue(scalar1Value / scalar2Value);
            }
        }


    }
}
