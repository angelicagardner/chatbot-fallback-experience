namespace ABot.RecommenderSystem.DataTypes
{
    public class Article
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<string> Keywords { get; set; }
    }
}