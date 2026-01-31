using GraphQL.Types;

namespace ABot.RecommenderSystem.DataTypes
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType(RecommenderSystemData data)
        {
            Name = "Article";
            Field(a => a.Url).Description("The URL of the article's web page.");
            Field(a => a.Title).Description("The title of the article.");
            Field(a => a.Summary).Description("A brief summary of the article content.");

            Field<ListGraphType<ParagraphType>>(
                "paragraphs",
                resolve: context => data.GetParagraphsForArticle(context.Source.Url)
            );
        }
    }
}