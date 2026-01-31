namespace ABot.RecommenderSystem
{
    public class RecommenderSystemData
    {
        private readonly Neo4jDriver _driver;

        public RecommenderSystemData(Neo4jDriver driver)
        {
            _driver = driver;
        }

        public async Task<Article> GetArticleByUrl(string url)
        {
            return await _driver.GetArticleAsync(url);
        }

        public async Task<List<Article>> GetAllArticles()
        {
            return await _driver.GetArticlesAsync();
        }
    }
}