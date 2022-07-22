namespace Smarticube.API.Utils
{
    public static class DecimalExtension
    {
        
            /// <summary>
            /// Extract the leftmost n characters of string
            /// </summary>
        public static decimal Convert(string value)
        {
            decimal tempDecimal = 0;
            return decimal.TryParse(value, out tempDecimal) ? tempDecimal : 0;
        }


        public static string ConvertLabel(decimal value, int decimal_place)
        {
            string result = "0.0";
            string formatString = string.Empty;
            decimal tempValue = Math.Abs(value);

            if (tempValue >= 1)
            {
                formatString = "##,###";

                if (decimal_place >= 1) formatString = formatString + ".";

                for (int i = 1; i <= decimal_place; i++)
                {
                    formatString = formatString + "0";
                }

                result = value < 0 ? "-" + tempValue.ToString(formatString) : tempValue.ToString(formatString);
            }



            if (tempValue > 0 && tempValue < 1)
            {

                formatString = "0";
                if (decimal_place >= 1) formatString = formatString + ".";

                for (int i = 1; i <= decimal_place; i++)
                {
                    formatString = formatString + "0";
                }

                result = value < 0 ? "-" + tempValue.ToString(formatString) : tempValue.ToString(formatString);
            }




            return result;
        }


        public static string ConvertLabel(double value, int decimal_place)
        {
            return ConvertLabel((decimal)value, decimal_place);
        }
        
    }
}
