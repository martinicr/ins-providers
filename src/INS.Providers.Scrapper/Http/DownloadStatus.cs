namespace INS.Provider.Scrapper.Http;

using System;

public record DownloadStatus(DateTime DownloadedDate, Uri Source, string FilePath);

