using System;
using System.Globalization;

namespace VersionService
{
    public static class Version
    {
        public const string _INPUT_ERROR = "Version strings must be valid and of same length";
        public static string CompareVersions(string version1, string version2)
        {
            if(String.IsNullOrEmpty(version1) || String.IsNullOrEmpty(version2))
            {
                return _INPUT_ERROR;
            }

            string[] v1 = version1.Trim().Split(new char[] {'.'}, StringSplitOptions.None);
            string[] v2 = version2.Trim().Split(new char[] {'.'}, StringSplitOptions.None);

            if (v1.Length != v2.Length) //versions must have same amount of major and minor versions 
            {
                return _INPUT_ERROR;
            }

            int num1;
            int num2;

            bool num1isInt;
            bool num2isInt;

            string comparison = $"{version1} is equal to {version2}";
            for (int i = 0; i < v1.Length; i++)
            {
                num1isInt = int.TryParse(v1[i], NumberStyles.None, CultureInfo.InvariantCulture, out num1);
                num2isInt = int.TryParse(v2[i], NumberStyles.None, CultureInfo.InvariantCulture, out num2);

                if (num1isInt && num2isInt)
                {
                    if (num1 > num2)
                    {
                        comparison = $"{version1} is greater than {version2}";
                        break;
                    }
                    else if (num1 < num2)
                    {
                        comparison = $"{version1} is less than {version2}";
                        break;
                    }
                }
                else
                {
                    return _INPUT_ERROR;
                }
            }
            return comparison;
        }
    }
}