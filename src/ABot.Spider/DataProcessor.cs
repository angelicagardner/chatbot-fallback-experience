using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ABot.Spider
{
    public class DataProcessor
    {
        private readonly string _projectPath;

        public DataProcessor(string projectPath)
        {
            _projectPath = projectPath;
        }

        public Dictionary<string, int[]> ProcessArticle(dynamic article)
        {
            string fullArticleText = "";
            string stopwords = File.ReadAllText(Path.Combine(_projectPath, "stopwords.txt"));
            Dictionary<string, int[]> finalWordScores = new Dictionary<string, int[]>();
            foreach (var paragraph in article.Paragraphs)
            {
                // 1. Get full article text by adding all paragraphs together
                fullArticleText += paragraph.Text + "\n";
                // 2. Split each paragraph into single words
                List<string> words = paragraph.Text.Split(" ").ToList();
                int totalWordsInParagraph = words.Count;
                foreach (string word in words.ToList())
                {
                    // 3. Remove whitespace and characters and convert to lower case
                    string normalizedWord = Regex.Replace(word.Trim().ToLower(), @"[^A-Za-z]?", "");
                    words.Remove(word);
                    words.Add(normalizedWord);
                    // 4. Remove stopwords
                    if (stopwords.Contains(normalizedWord) || string.IsNullOrEmpty(normalizedWord))
                    {
                        words.Remove(normalizedWord);
                    }
                }

                // --- Scoring Logic (Count & Frequency) ---
                foreach (var uniqueWord in words.Distinct())
                {
                    int count = words.Count(w => w == uniqueWord);
                    double frequency = ((double)count / totalWordsInParagraph) * 100;
                    int roundedFrequency = (int)Math.Round(frequency);
                    finalWordScores[uniqueWord] = new int[] { count, roundedFrequency };
                }
            }

            return finalWordScores;
        }
    }
}