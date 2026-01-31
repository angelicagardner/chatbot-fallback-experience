using GraphQL.Types;

namespace ABot.RecommenderSystem.DataTypes
{
    public class ArticleInputType : InputObjectGraphType<Article>
    {
        public ArticleInputType()
        {
            Name = "ArticleInput";
            Field(a => a.Url);
            Field(a => a.Title);
            Field(a => a.Summary);
        }
    }
}