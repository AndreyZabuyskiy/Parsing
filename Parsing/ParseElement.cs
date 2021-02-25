using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsing
{
    class ParseElement
    {
        public string FillElement(string item, string element)
        {
            string pattern = $"{element}(.*?){ClosingTag(element)}";
            return Regex.Match(item, pattern).Groups[1].Value;
        }
        public string ClosingTag(string tag)
        {
            char[] closingTag = new char[tag.Length + 1];
            int idx = 0;

            for (int i = 0; i < closingTag.Length; ++i)
            {
                if (i == 1)
                {
                    closingTag[i] = '/';
                    continue;
                }

                closingTag[i] = tag[idx++];
            }

            return new string(closingTag);
        }
    }
}
