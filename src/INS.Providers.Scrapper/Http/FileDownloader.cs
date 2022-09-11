namespace INS.Provider.Scrapper.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using INS.Provider.Scrapper.IO;

internal class FileDownloader : IDownloader
{
    private readonly HttpClient _httpClient;
    private readonly IFileHandler _fileHandler;

    public FileDownloader() : this(new HttpClient(), new LocalDiskFileHandler())
    {

    }

    public FileDownloader(IFileHandler fileHandler) : this(new HttpClient(), fileHandler)
    {

    }

    public FileDownloader(HttpClient httpClient) : this(httpClient, new LocalDiskFileHandler())
    {

    }

    public FileDownloader(HttpClient httpClient, IFileHandler fileHandler)
    {

        if (null == httpClient)
        {
            throw new ArgumentException("HttpClient has to be provided");
        }

        if (null == fileHandler)
        {
            throw new ArgumentException("IFileHandler implementation has to be provided");
        }

        this._httpClient = httpClient;
        this._fileHandler = fileHandler;
    }

    public async Task<DownloadStatus> DownloadAsync(Uri url)
    {
        if (null == url)
        {
            throw new ArgumentException("A valid Url has to be provided");
        }
        
        // TODO: Polly
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var filePath = await _fileHandler.CreateFile(response.Content.ReadAsStreamAsync);

        return new DownloadStatus(DateTime.Now, url, filePath);
    }

}
