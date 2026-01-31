using GraphQL.Types;
using ABot.RecommenderSystem.DataTypes;

namespace ABot.RecommenderSystem
{
    public class RecommenderSystemQuery : ObjectGraphType
    {
        public RecommenderSystemQuery(RecommenderSystemData data)
        {
            Name = "Query";

            Field<ArticleType>(
                "article",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "url" }),
                resolve: context => data.GetArticleByUrl(context.GetArgument<string>("url"))
            );

            Field<ListGraphType<ArticleType>>(
                "articles",
                resolve: context => data.GetAllArticles()
            );
        }
    }
}