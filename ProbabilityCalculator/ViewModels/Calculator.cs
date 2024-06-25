using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace ProbabilityCalculator.ViewModels
{
    internal class Calculator
    {

        public Dictionary<String,Scalar> scalars;
        public Dictionary<String,RandomQuantity> randomQuantities;
        public Calculator() { 
            scalars = new Dictionary<String, Scalar>();
            randomQuantities = new Dictionary<String, RandomQuantity>();

            Scalar ANS = new Scalar();
            scalars.Add("ANS",ANS);

            Scalar OPVAL = new Scalar();
            scalars.Add("OPVAL", OPVAL);
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
