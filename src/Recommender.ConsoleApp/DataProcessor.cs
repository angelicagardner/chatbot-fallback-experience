using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Recommender.ConsoleApp
{
    public class DataProcessor
    {
        private readonly HashSet<string> _stopWords;

        public DataProcessor(string stopWordsPath)
        {
            _stopWords = new HashSet<string>(
                File.ReadAllLines(stopWordsPath).Select(s => s.Trim().ToLower())
            );
        }

        public Dictionary<string, int[]> ProcessParagraph(string rawText)
        {
            string normalizedText = Regex.Replace(rawText.ToLower(), @"[^a-z\s]", "");

            string[] allWords = normalizedText.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> filteredWords = allWords
                .Where(w => !_stopWords.Contains(w))
                .ToList();

            int totalWordsCount = allWords.Length;

            Dictionary<string, int[]> wordScores = new Dictionary<string, int[]>();

            foreach (var word in filteredWords)
            {
                if (!wordScores.ContainsKey(word))
                {
                    int count = filteredWords.Count(w => w == word);

                    double frequency = ((double)count / totalWordsCount) * 100;
                    int roundedFrequency = (int)Math.Round(frequency);

                    wordScores.Add(word, new int[] { count, roundedFrequency });
                }
            }

            return wordScores;
        }
    }
}