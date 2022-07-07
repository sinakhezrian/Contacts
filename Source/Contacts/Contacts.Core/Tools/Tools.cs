using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.Tools
{

    public static class Tools
    {

        public static T ParsePersianEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value.Replace(' ', '_'), true);
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static bool NumberValidation(string value)
        {
            bool _isNumber = long.TryParse(value, out long n);
            return _isNumber;
        }
        public static char GetFirstChar(string value)
        {
            return value.ToCharArray()[0];
        }
        public static int StringLength(string value)
        {
            int _length = value.ToCharArray().Length;
            return _length;
        }
        public static string GenerateUniqueString()
        {
            string _uniqueStr = Guid.NewGuid().ToString() + new Random().Next(0, 999999);
            return _uniqueStr;
        }
        
    }
}
