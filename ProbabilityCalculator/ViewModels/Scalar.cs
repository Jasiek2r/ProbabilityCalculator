using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilityCalculator.ViewModels
{
    internal class Scalar
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

        public decimal getValue()
        {
            return _value;
        }

        public void setValue(decimal value)
        {
            this._value = value;
        }

        public void setHasDecimalPart(bool hasDecimalPart)
        {
            this._hasDecimalPart = hasDecimalPart;
        }
        public void appendDigit(Int32 digit)
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

        public void popDigit()
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
