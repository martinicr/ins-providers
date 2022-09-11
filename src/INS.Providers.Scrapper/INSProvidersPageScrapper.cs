namespace INS.Provider.Scrapper;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using INS.Provider.Scrapper.Parser;

using Microsoft.Extensions.Logging;

public class INSProvidersPageScrapper : BaseScrapper
{
    public INSProvidersPageScrapper(ILogger<INSProvidersPageScrapper> logger, IParserStrategy parserStrategy) : base(logger, parserStrategy)
    {

    }




}

