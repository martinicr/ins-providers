using INS.Provider.Scrapper.Http;

namespace INS.Provider.Scrapper;


public interface IINSProvidersPageScrapper
{
    //Task<DownloadStatus> GetFromUrlAsync(Uri url);

    Task Parse(Uri url, string query);

    Task Parse(FileInfo fileInfo, string query);

}