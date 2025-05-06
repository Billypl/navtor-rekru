using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Utils
{
    public class ImoValidator
    {
        private const int ImoNumberLength = 7;
        public static bool IsValidImoNumber(string imoNumber)
        {
            return IsValidFormat(imoNumber) && IsValidControlSum(imoNumber);
        }

        private static bool IsValidFormat(string imoNumber)
        {
            return !(imoNumber.Length != ImoNumberLength || imoNumber.Any(c => !char.IsDigit(c)));
        }

        private static bool IsValidControlSum(string imoNumber)
        {
            // https://en.wikipedia.org/wiki/IMO_number#:~:text=IMO%20number%20of%20a%20vessel%5Bedit%5D
            int[] digits = imoNumber.Select(c => c - '0').ToArray();
            int controlDigit = digits[ImoNumberLength - 1];
            int controlSum = 0;
            for (int i = 0; i < ImoNumberLength - 1; i++)
            {
                int digit = digits[i];
                controlSum += digit * (ImoNumberLength - i);
            }
            return controlSum % 10 == controlDigit;
        }
    }
}
