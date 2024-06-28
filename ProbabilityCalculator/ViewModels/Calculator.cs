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

            Scalar ANS = new Scalar();
            scalars.Add("ANS",ANS);
            dataKeys.Add("ANS", "SCALAR");

            Scalar OPVAL = new Scalar();
            scalars.Add("OPVAL", OPVAL);
            dataKeys.Add("OPVAL", "SCALAR");

            RandomQuantity X = new RandomQuantity();
            randomQuantities.Add("X", X);
            dataKeys.Add("X", "RANDOM QUANTITY");
        }

        public Scalar ReadScalar(String identifier)
        {
            return scalars[identifier];
        }

        public void WriteScalar(String key, Scalar value)
        {
            scalars[key] = value;
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

        public void Add(String identifier, String operand1Name, String operand2Name)
        {
            if(identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars["ANS"].SetValue(scalar1Value + scalar2Value);
            }
        }
        public void Multiply(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars["ANS"].SetValue(scalar1Value * scalar2Value);
            }
        }

        public void Subtract(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars["ANS"].SetValue(scalar1Value - scalar2Value);
            }
        }
        public void Divide(String identifier, String operand1Name, String operand2Name)
        {
            if (identifier == "scalars")
            {
                decimal scalar1Value = scalars[operand1Name].GetValue();
                decimal scalar2Value = scalars[operand2Name].GetValue();

                scalars["ANS"].SetValue(scalar1Value / scalar2Value);
            }
        }


    }
}
