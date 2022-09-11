namespace INS.Provider.Scrapper;

using Moq;
using Moq.Protected;


internal static class MockHttpMessageHandler<T> 
{
    // internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
    internal static Mock<HttpMessageHandler> SetupBasicGetResourceList()
    {

        var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK) {
            Content = new StringContent("<html><head></head><body></body>")
        };

        mockResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;

    }    
}