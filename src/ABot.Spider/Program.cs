using System;
using System.Collections.Generic;
using System.IO;
using DotnetSpider.Core;
using ABot.Spider.Spiders;
using ABot.RecommenderSystem;
using ABot.RecommenderSystem.DataTypes;
using Newtonsoft.Json;

namespace ABot.Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            var spider = DotnetSpider.Spider.Create<SimpleaSpider>();
            spider.Run();
            Console.WriteLine("Crawling completed.");

            string projectPath = Directory.GetCurrentDirectory();

            // Replace with actual connection details
            var driver = new Neo4jDriver("bolt://localhost:7687", "neo4j", "password");

            var processor = new DataProcessor(projectPath);

            string jsonPath = Path.Combine(projectPath, "Data", "SimpleaSpider", "SimpleaSpider.json");

            if (File.Exists(jsonPath))
            {
                string jsonContent = File.ReadAllText(jsonPath);
                var scrapedData = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

                foreach (var item in scrapedData)
                {
                    var article = new Article
                    {
                        Url = item.URL,
                        Title = item.Title,
                        Summary = item.Summary,
                        Keywords = new List<string> { (string)item.Keywords }
                    };

                    driver.AddArticle(article);

                    var wordScores = processor.ProcessArticle(item);

                    foreach (var property in item)
                    {
                        string key = property.Name;
                        if (key.StartsWith("Paragraph"))
                        {
                            string paragraphText = property.Value.ToString();
                            driver.AddParagraphData(article.Url, paragraphText, wordScores);
                        }
                    }
                }
                Console.WriteLine("Graph population complete!");
            }
            else
            {
                Console.WriteLine($"Could not find JSON file at: {jsonPath}");
            }

            Console.WriteLine("System task finished. Press any key to exit.");
            Console.ReadKey();

            driver.Dispose();
        }
    }
}