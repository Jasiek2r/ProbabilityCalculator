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
    }
}
