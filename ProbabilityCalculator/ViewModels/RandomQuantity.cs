using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilityCalculator.ViewModels
{
    public class RandomQuantity
    {
        Dictionary<decimal, decimal> realisations;
        public RandomQuantity()
        {
            realisations = new Dictionary<decimal, decimal>();
            realisations.Add(1, 1);
        }

        public Dictionary<decimal,decimal> GetRealisations()
        {
            return realisations;
        }

        public void SetRealisations(Dictionary<decimal, decimal> realisations)
        {
            this.realisations = realisations;
        }

        public decimal Realise()
        {
            Random randomNumberGenerator = new Random();

            decimal randNormalised = (decimal)randomNumberGenerator.NextDouble();

            decimal threshold = 0;

            decimal realisationValue = 0;

            foreach(KeyValuePair<decimal, decimal> realisation in realisations)
            {
                threshold += realisation.Value;
                if(randNormalised < threshold)
                {
                    realisationValue = realisation.Key;
                    break;
                }
            }

            return realisationValue;
        }
        public decimal ComputeExpectedValue()
        {
            decimal expectedValue = 0;

            foreach (var realization in realisations)
            {
                decimal value = realization.Key;
                decimal probability = realization.Value;

                expectedValue += value * probability;
            }

            return expectedValue;
        }
    }
}
