namespace ABot.RecommenderSystem.DataTypes
{
    public class Paragraph
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<Word> Words { get; set; } = new List<Word>();
    }
}