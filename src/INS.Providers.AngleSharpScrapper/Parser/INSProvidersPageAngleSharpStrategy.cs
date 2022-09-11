namespace INS.Provider.AngleSharpScrapper.Parser;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AngleSharp.Dom;
using AngleSharp;

using INS.Provider.Scrapper.Parser;
using INS.Providers.AngleSharpScrapper.Extensions;
using INS.Providers.Scrapper;

public class INSProvidersPageAngleSharpStrategy : BaseParserStrategy<IElement>
{

    private readonly HttpClient _httpClient;

    public INSProvidersPageAngleSharpStrategy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected override PageElement ConvertTo(IElement element)
    {
        return element.ToPageElement();
    }

    protected override async Task<IEnumerable<IElement>> QueryElementsInUrl(Uri uri, string query)
    {
        Url url = Url.Convert(uri);
        IConfiguration config = Configuration.Default.WithDefaultLoader().With(_httpClient);
        IBrowsingContext context = BrowsingContext.New(config);
        IDocument document = await context.OpenAsync(url);
        return document.QuerySelectorAll(query);
    }
}
