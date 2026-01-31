using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetSpider.Core;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Extraction;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Pipeline;

namespace ABot.Spider.Spiders
{
    public class SimpleaSpider : DotnetSpider.Spider
    {
        protected override void Initialize()
        {
            NewGuidId();
            Scheduler = new QueueDistinctBfsScheduler();
            DownloaderSettings.Type = DownloaderType.HttpClient;
            AddDataFlow(new SimpleaDataParser()).AddDataFlow(new JsonFileStorage());
            AddRequests("https://simplea.com/Articles/facing-content-supply-chain-problems");
        }

        class SimpleaDataParser : DataParser
        {
            public SimpleaDataParser()
            {
                CanParse = DataParserHelper.CanParseByRegex("simplea\\.com/Articles");
                QueryFollowRequests = DataParserHelper.QueryFollowRequestsByPath(".");
            }

            protected override Task<DataFlowResult> Parse(DataFlowContext context)
            {
                if (context.Response != null)
                {
                    context.AddItem("URL", context.Response.Request.Url);
                    context.AddItem("Title", context.GetSelectable().XPath("//title").GetValue());
                    context.AddItem("Keywords", context.GetSelectable().XPath("//meta[@name='keywords']/@content").GetValue());
                    context.AddItem("Summary", context.GetSelectable().XPath("//meta[@name='description']/@content").GetValue());
                    var pTags = context.GetSelectable().XPath("//p").Nodes();
                    int nr = 1;
                    foreach (var p in pTags)
                    {
                        context.AddItem("Paragraph " + nr, p.GetValue());
                        nr++;
                    }
                }
                return Task.FromResult(DataFlowResult.Success);
            }
        }
    }
}