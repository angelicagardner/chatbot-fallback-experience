using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ABot.RecommenderSystem
{
    public class RecommenderSystemSchema : Schema
    {
        public RecommenderSystemSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RecommenderSystemQuery>();
            Mutation = provider.GetRequiredService<RecommenderSystemMutation>();
        }
    }
}