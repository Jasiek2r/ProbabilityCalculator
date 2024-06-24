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
        }

        public Scalar readScalar(String identifier)
        {
            return scalars[identifier];
        }

        public void writeScalar(String key, Scalar value)
        {
            scalars[key] = value;
        }
    }
}
