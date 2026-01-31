using GraphQL;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ABot.RecommenderSystem
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Replace with actual connection details
            services.AddSingleton<Neo4jDriver>(s =>
                new Neo4jDriver("bolt://localhost:7687", "neo4j", "password"));

            services.AddScoped<RecommenderSystemData>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<RecommenderSystemSchema>();
            services.AddScoped<RecommenderSystemQuery>();
            services.AddScoped<RecommenderSystemMutation>();

            services.AddScoped<ArticleType>();
            services.AddScoped<ArticleInputType>();
            services.AddScoped<ParagraphType>();
            services.AddScoped<WordType>();

            services.AddGraphQL(options =>
            {
                options.ExposeExceptions = true;
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddDataLoader();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGraphQL<RecommenderSystemSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}