namespace INS.Provider.Scrapper.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDownloader
{
    Task<DownloadStatus> DownloadAsync(Uri url);

}
