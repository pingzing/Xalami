using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xalami.Converters
{
    public class EmptyNullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool)
            {
                return (bool)value;
            }

            if (value is byte)
            {
                byte byteVal = (byte)value;
                return byteVal != 0;
            }

            if (value is short)
            {
                short shortVal = (short)value;
                return shortVal != 0;
            }

            if (value is int)
            {
                int intVal = (int)value;
                return intVal != 0;
            }

            if (value is decimal)
            {
                decimal decimalVal = (decimal)value;
                return decimalVal != 0;
            }

            if (value is long)
            {
                long longVal = (long)value;
                return longVal != 0;
            }

            if (value is float)
            {
                float floatVal = (float)value;
                return floatVal != 0;
            }

            if (value is double)
            {
                double doubleVal = (double)value;
                return doubleVal != 0;
            }

            if (value is char)
            {
                char charVal = (char)value;
                return !Char.IsWhiteSpace(charVal);
            }

            string stringVal = value as string;
            if (stringVal != null)
            {
                return !String.IsNullOrWhiteSpace(stringVal);
            }

            IEnumerable enumerableVal = value as IEnumerable;
            if (enumerableVal != null)
            {
                return enumerableVal.GetEnumerator().MoveNext();
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
