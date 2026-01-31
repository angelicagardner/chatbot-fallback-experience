using System;
using System.Collections.Generic;
using Neo4j.Driver.V1;
using ABot.RecommenderSystem.DataTypes;

namespace ABot.RecommenderSystem
{
    public class Neo4jDriver : IDisposable
    {
        public IDriver Driver { get; set; }

        public Neo4jDriver(string uri, string user, string password)
        {
            Driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        /// <summary>
        /// To add a new article to the database
        /// </summary>
        /// <param name="article"> The new article object </param>
        /// <returns> True or false depending on if the article was added to the database or not </returns>
        public bool AddArticle(Article article)
        {
            try
            {
                using (var session = Driver.Session())
                {
                    return session.WriteTransaction(
                    tx =>
                    {
                        tx.Run("MERGE (a:Article { Url: $Url }) " +
                               "ON CREATE SET a.Url = $Url, " +
                               "a.Keywords = $Keywords, " +
                               "a.Title = $Title, " +
                               "a.Summary = $Summary " +
                               "ON MATCH SET a.Url = $Url, " +
                               "a.Keywords = $Keywords, " +
                               "a.Title = $Title, " +
                               "a.Summary = $Summary",
                            new { article.Url, article.Keywords, article.Title, article.Summary });
                        return true;
                    });
                }
            }
            catch (ServiceUnavailableException)
            {
                return false;
            }
        }

        public void AddParagraphData(string articleUrl, string paragraphText, Dictionary<string, int[]> wordScores)
        {
            using (var session = Driver.Session())
            {
                session.WriteTransaction(tx =>
                {
                    tx.Run("MATCH (a:Article { Url: $articleUrl }) " +
                           "MERGE (p:Paragraph { Text: $paragraphText }) " +
                           "MERGE (a)-[:HAS_PARAGRAPH]->(p)",
                        new { articleUrl, paragraphText });

                    foreach (var entry in wordScores)
                    {
                        tx.Run("MATCH (p:Paragraph { Text: $paragraphText }) " +
                               "MERGE (w:Word { Label: $word }) " +
                               "MERGE (p)-[r:PARAGRAPH_CONTAINS]->(w) " +
                               "SET r.Count = $count, r.Frequency = $frequency",
                            new
                            {
                                paragraphText,
                                word = entry.Key,
                                count = entry.Value[0],
                                frequency = entry.Value[1]
                            });
                    }
                });
            }
        }

        /// <summary>
        /// Close or release unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Driver?.Dispose();
        }
    }
}