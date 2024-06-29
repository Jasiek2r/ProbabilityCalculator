using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilityCalculator.ViewModels
{
    public class RandomQuantity
    {
        private Dictionary<decimal, decimal> _realizations;
        public RandomQuantity()
        {
            _realizations = new Dictionary<decimal, decimal>();
            _realizations.Add(1, 1);
        }

        public Dictionary<decimal,decimal> GetRealizations()
        {
            return _realizations;
        }

        public void SetRealizations(Dictionary<decimal, decimal> realizations)
        {
            this._realizations = realizations;
        }

        public decimal Realize()
        {
            Random randomNumberGenerator = new Random();

            decimal randNormalised = (decimal)randomNumberGenerator.NextDouble();

            decimal threshold = 0;

            decimal realizationValue = 0;

            foreach(KeyValuePair<decimal, decimal> realization in _realizations)
            {
                threshold += realization.Value;
                if(randNormalised < threshold)
                {
                    realizationValue = realization.Key;
                    break;
                }
            }

            return realizationValue;
        }
        public decimal ComputeExpectedValue()
        {
            decimal expectedValue = 0;

            foreach (var realization in _realizations)
            {
                decimal value = realization.Key;
                decimal probability = realization.Value;

                expectedValue += value * probability;
            }

            return expectedValue;
        }

        public decimal ComputeVariance()
        {

            decimal mean = ComputeExpectedValue();

            decimal sumSquaredDifferences = 0;
            foreach (KeyValuePair<decimal, decimal> realisation in _realizations)
            {
                decimal difference = realisation.Key - mean;
                sumSquaredDifferences += difference * difference * realisation.Value;
            }

            decimal variance = sumSquaredDifferences;

            return variance;
        }
    }
}
