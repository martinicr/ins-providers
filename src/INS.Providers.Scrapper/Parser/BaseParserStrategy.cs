namespace INS.Provider.Scrapper.Parser;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INS.Providers.Scrapper;

public abstract class BaseParserStrategy<T> : IParserStrategy where T : class
{ 
    public async Task<IEnumerable<PageElement>> Parse(Uri url, string query)
    {
        var elementsInPage = await QueryElementsInUrl(url, query);
        return ConvertTo(elementsInPage);        
    }

    public Task<IEnumerable<PageElement>> Parse(FileInfo fileInfo, string query)
    {
        throw new NotImplementedException();
    }

    protected abstract Task<IEnumerable<T>> QueryElementsInUrl(Uri url, string query);

    protected IEnumerable<PageElement> ConvertTo(IEnumerable<T> elements)
    {
        return elements.Select(e => ConvertTo(e));
    }

    protected abstract PageElement ConvertTo(T element);
}

