using System;
using System.Globalization;

namespace HexCodeAndRGBvalueConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;

            while (true)
            {
                input = Console.ReadLine();
                Console.WriteLine(ProcessInput(input));
            }
        }
        private static string HexCodeToRGBValues(string hexCode)
        {
            bool moreHex = false;
            if (hexCode.IndexOf("#") != -1)
                hexCode = hexCode.Remove(hexCode.IndexOf("#"), 1);
            if (hexCode.Length > 6)
                moreHex = true;
            int r, g, b = 0;
            r = int.Parse(hexCode.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            g = int.Parse(hexCode.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            b = int.Parse(hexCode.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            if (moreHex)
            {
                if (hexCode.IndexOf("#") == 7 || hexCode.IndexOf("#") != -1)
                {
                    int offset = hexCode.IndexOf("#");
                    hexCode = hexCode.Remove(offset, 1);
                }

                int _r, _g, _b = 0;
                try
                {
                    _r = int.Parse(hexCode.Substring(7, 2), NumberStyles.AllowHexSpecifier);
                    _g = int.Parse(hexCode.Substring(9, 2), NumberStyles.AllowHexSpecifier);
                    _b = int.Parse(hexCode.Substring(11, 2), NumberStyles.AllowHexSpecifier);
                    return r + ", " + g + ", " + b + " | " + _r + ", " + _g + ", " + _b;
                }
                catch (System.Exception e)
                {
                    if (e is System.ArgumentOutOfRangeException)
                    {
                        return "The second hex code is Invalid or formatted incorrectly.";
                    }
                    else
                    {
                        return "Unknown error. Please send a message to ConfuzzedCat#0001 with the input.";
                    }
                }
            }
            else
            {
                return r + ", " + g + ", " + b;
            }
        }

        private static string RGBValuesToHexCode(string RGB, bool hashtag)
        {
            int _r, _g, _b = 0;
            string filter = ", ";
            string outputString;
            _r = int.Parse(RGB.Substring(0, RGB.IndexOf(filter)));
            _g = int.Parse(RGB.Substring(RGB.IndexOf(filter) + 2, RGB.LastIndexOf(filter) - RGB.IndexOf(filter) - 2));
            _b = int.Parse(RGB.Substring(RGB.LastIndexOf(filter) + 2));
            if (hashtag)
                outputString = '#' + _r.ToString("X2") + _g.ToString("X2") + _b.ToString("X2");
            else
                outputString = _r.ToString("X2") + _g.ToString("X2") + _b.ToString("X2");
            return outputString;
        }

        private static string RGBValuesToHexCode(string RGB)
        {
            int _r, _g, _b = 0;
            string filter = ", ";
            string outputString;
            try
            {
                _r = int.Parse(RGB.Substring(0, RGB.IndexOf(filter)));
                _g = int.Parse(RGB.Substring(RGB.IndexOf(filter) + 2, RGB.LastIndexOf(filter) - RGB.IndexOf(filter) - 2));
                _b = int.Parse(RGB.Substring(RGB.LastIndexOf(filter) + 2));
                outputString = _r.ToString("X2") + _g.ToString("X2") + _b.ToString("X2");
                return outputString;
            }
            catch (System.FormatException)
            {
                return "Input contains invalid characters!";
            }
        }

        private static bool isHex(string s)
        {
            if (s[0] == '#')
                return true;
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if ((ch < '0' || ch > '9') && (ch < 'A' || ch > 'F'))
                {
                    return false;
                }
            }

            return true;
        }

        private static string ProcessInput(string input)
        {
            bool _hashtag = false;
            if (input.Contains("true"))
            {
                input = input.Remove(input.IndexOf("true"));
                _hashtag = true;
            }
            else if (input.Contains(" true"))
            {
                input = input.Remove(input.IndexOf(" true"));
                _hashtag = true;
            }

            if (input.Contains("false"))
            {
                input = input.Remove(input.IndexOf("false"));
                _hashtag = false;
            }
            else if (input.Contains(" false"))
            {
                input = input.Remove(input.IndexOf(" false"));
                _hashtag = false;
            }

            if (isHex(input) && _hashtag)
            {
                return "RGB values can't have a hashtag!";
            }
            else if (isHex(input))
            {
                return HexCodeToRGBValues(input);
            }
            else if (_hashtag)
            {
                return RGBValuesToHexCode(input, true);
            }
            else
            {
                return RGBValuesToHexCode(input);
            }
        }
    }
}
