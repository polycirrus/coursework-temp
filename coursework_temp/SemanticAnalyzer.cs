using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace coursework_temp
{
    class UniqueWord
    {
        public String Word;
        public int Count;
        public float Frequency;

        public UniqueWord(String word)
        {
            this.Word = word;
            this.Count = 1;
            this.Frequency = 0;
        }

        public override string ToString()
        {
            return String.Format("\"{0}\" Count = {1}; Frequency = {2}", Word, Count, Frequency);
        }
    }

    class SemanticAnalyzer
    {
        const String stopDictionaryPath = "stop.txt";
        const int semanticCoreSize = 15;

        String Text;

        public SemanticAnalyzer(String text)
        {
            this.Text = text;
        }

        public int CharacterCount(bool noWhitespaces)
        {
            if (noWhitespaces)
                return Regex.Matches(Text, "\\S").Count;
            else
                return Regex.Matches(Text, ".").Count;
        }

        public String[] Words()
        {
            return Regex.Matches(Text, "[A-Za-zА-Яа-яё]+(-[a-zA-Zа-яА-Яё]+)*").Cast<Match>().Select(match => match.Value).ToArray();
        }

        public UniqueWord[] UniqueWords()
        {
            var words = Words();

            int i;
            var lemmatizer = new LemmaSharp.LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.Russian);
            for (i = 0; i < words.Length; i++)
            {
                words[i] = lemmatizer.Lemmatize(words[i].ToLower());
                
            }

            var uniqueWordsList = new List<UniqueWord>();

            //add words to list of unique words
            int index;
            foreach (String word in words)
            {
                if ((index = uniqueWordsList.FindIndex(uniqueWord => uniqueWord.Word == word)) > -1)
                {
                    uniqueWordsList.ElementAt(index).Count += 1;
                }
                else
                {
                    uniqueWordsList.Add(new UniqueWord(word));
                }
            }

            //count frequencies
            var wordCount = words.Count();
            foreach (UniqueWord uniqueWord in uniqueWordsList)
            {
                uniqueWord.Frequency = Convert.ToSingle(uniqueWord.Count) / Convert.ToSingle(wordCount);
            }

            //sort list by words' counters
            uniqueWordsList.Sort((x, y) => y.Count - x.Count);

            return uniqueWordsList.ToArray();
        }

        public UniqueWord[] SemanticCore()
        {
            var semanticCore = new List<UniqueWord>();
            var stopWords = File.ReadAllLines(stopDictionaryPath);

            var uniqueWords = UniqueWords();
            foreach (UniqueWord uniqueWord in uniqueWords)
            {
                if (! stopWords.Contains(uniqueWord.Word))
                    semanticCore.Add(uniqueWord);
            }

            return semanticCore.Take(15).ToArray();
        }

        public int WordCount()
        {
            return Words().Count();
        }

        public int UniqueWordCount()
        {
            return UniqueWords().Count();
        }

        public override String ToString()
        {
            var result = String.Format("Characters: {0}\nCharacters w/out whitespaces: {1}\nWords: {2}\nUnique words: {3}\nWords:\n", CharacterCount(false), CharacterCount(true), WordCount(), UniqueWordCount());
            
            var words = Words();
            foreach (String word in words)
            {
                result += "\t" + word + "\n";
            }

            result += "Unique words:\n";
            var uniqueWords = UniqueWords();
            foreach (UniqueWord uword in uniqueWords)
            {
                result += "\t" + uword.ToString() + "\n";
            }

            return result;
        }
    }
}
