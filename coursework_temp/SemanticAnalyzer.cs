using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace coursework_temp
{
    class UniqueWord
    {
        public String word;
        public int count;
        public float frequency;

        public UniqueWord(String word)
        {
            this.word = word;
            this.count = 1;
            this.frequency = 0;
        }
    }

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

        public UniqueWord[] UniqueWords()
        {
            var words = Words().Distinct().ToArray();
            var uniqueWordsList = new List<UniqueWord>();

            int index;
            foreach (String word in words)
            {
                if ((index = uniqueWordsList.FindIndex(uniqueWord => uniqueWord.word == word)) > -1)
                {
                    uniqueWordsList.ElementAt(index).count += 1;
                }
                else
                {
                    uniqueWordsList.Add(new UniqueWord(word));
                }
            }

            var wordCount = words.Count();
            foreach (UniqueWord uniqueWord in uniqueWordsList)
            {
                uniqueWord.frequency = uniqueWord.count / wordCount;
            }

            return uniqueWordsList.ToArray();
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
