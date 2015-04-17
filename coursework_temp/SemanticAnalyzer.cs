using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace coursework_temp
{
    class SemanticAnalyzer
    {
        String text;

        public int CharacterCount(bool noWhitespaces)
        {
            if (noWhitespaces)
                return Regex.Matches(text, "\\S").Count;
            else
                return Regex.Matches(text, ".").Count;
        }

        public String[] Words()
        {
            return Regex.Matches(text, "[A-Za-z]*(-[a-zA-Z]+)*").Cast<Match>().Select(match => match.Value).ToArray();
        }

        public String[] UniqueWords()
        {
            return Words().Distinct().ToArray();
        }

        public int WordCount()
        {
            return Words().Count();
        }

        public int UniqueWordCount()
        {
            return UniqueWords().Count();
        }
    }
}
