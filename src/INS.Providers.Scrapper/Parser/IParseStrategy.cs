namespace INS.Provider.Scrapper.Parser;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INS.Providers.Scrapper;

public interface IParserStrategy
{
    Task<IEnumerable<PageElement>> Parse(Uri url, string query);

    Task<IEnumerable<PageElement>> Parse(FileInfo fileInfo, string query);
}
