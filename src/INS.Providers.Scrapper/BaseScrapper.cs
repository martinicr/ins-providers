namespace INS.Provider.Scrapper;

using INS.Provider.Scrapper.Parser;

using Microsoft.Extensions.Logging;

public abstract class BaseScrapper : IINSProvidersPageScrapper
{

    protected ILogger _logger;
    protected IParserStrategy _parserStrategy;

    protected BaseScrapper(ILogger<BaseScrapper> logger, IParserStrategy parserStrategy)
    {

        if (null == logger)
        {
            throw new ArgumentException("Logger instance has to be provided");
        }

        if (null == parserStrategy)
        {
            throw new ArgumentException("Parse Strategy instance has to be provided");
        }

        _logger = logger;
        _parserStrategy = parserStrategy;

        _logger.LogDebug("Scrapper Loaded Sucessfully!");
    }

    //public async Task<DownloadStatus> GetFromUrlAsync(Uri url)
    //{
    //    if (null == url)
    //    {
    //        throw new ArgumentException("Url has to be provided");
    //    }

    //    return await _downloader.DownloadAsync(url);
    //}

    public async Task Parse(Uri url, string query)
    {
        var nodes = await _parserStrategy.Parse(url, query);
        if (nodes.Any())
        {
            foreach (var item in nodes)
            {
                Console.WriteLine($"{item.ResourceUrl.OriginalString}");
            }
        }
    }

    public async Task Parse(FileInfo fileInfo, string query)
    {
        await _parserStrategy.Parse(fileInfo, query);
    }

}