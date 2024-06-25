using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilityCalculator.ViewModels
{
    public class Scalar
    {
        private decimal _value;
        private bool _hasDecimalPart;


        public Scalar()
        {
            this._value = 0;
            this._hasDecimalPart = false;
        }

        public Scalar(decimal value)
        {
            this._value = value;
            this._hasDecimalPart = false;
        }

        public decimal GetValue()
        {
            return _value;
        }

        public void SetValue(decimal value)
        {
            this._value = value;
        }

        public void SetHasDecimalPart(bool hasDecimalPart)
        {
            this._hasDecimalPart = hasDecimalPart;
        }
        public void AppendDigit(Int32 digit)
        {
            if(_hasDecimalPart == false)
            {
                _value = (_value * 10) + digit;
            }
            else
            {
                _value = decimal.Parse(_value.ToString() + digit.ToString());
            }
            
        }

        public void PopDigit()
        {
            int length = _value.ToString().Length;
            String valueAsString = _value.ToString();
            valueAsString = valueAsString.Remove(length - 1);
            if (valueAsString.Length == 0)
            {
                valueAsString = "0";
            }
            _value = decimal.Parse(valueAsString);
        }
    }
}
