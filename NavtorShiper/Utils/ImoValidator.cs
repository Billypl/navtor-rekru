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
            if (imoNumber.Any(c => !char.IsDigit(c)))
            {
                return false;
            }
            if (imoNumber.Length != ImoNumberLength)
            {
                return false;
            }
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
