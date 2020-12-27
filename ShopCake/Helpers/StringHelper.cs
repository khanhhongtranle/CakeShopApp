using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCake.Helpers
{
    class StringHelper
    {
        public StringHelper()
        {

        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string CutStringTo(string s, char cutPoint)
        {
            char[] charArray = s.ToCharArray();
            List<char> result = new List<char>();
            foreach (var achar in charArray)
            {
                if (!achar.Equals(cutPoint))
                {
                    result.Add(achar);
                }
                else
                {
                    break;
                }
            }
            return new string(result.ToArray());
        }
    }
}
