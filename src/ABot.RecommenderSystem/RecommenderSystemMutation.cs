using GraphQL.Types;
using ABot.RecommenderSystem.DataTypes;

namespace ABot.RecommenderSystem
{
    public class RecommenderSystemMutation : ObjectGraphType
    {
        public RecommenderSystemMutation(RecommenderSystemData data)
        {
            Name = "Mutation";

            Field<ArticleType>(
                "createArticle",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ArticleInputType>> { Name = "article" }),
                resolve: context =>
                {
                    var article = context.GetArgument<Article>("article");
                    return data.SaveArticle(article);
                }
            );
        }
    }
}