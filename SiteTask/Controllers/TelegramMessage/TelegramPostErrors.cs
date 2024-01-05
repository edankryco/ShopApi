using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace SiteTask.Controllers.TelegramMessage;

public interface ITelegramPostErrors
{
    public Task<IActionResult> PostErrors(string error, string authorizationId, string url, string tail);
}

public class TelegramPostErrors : ControllerBase, ITelegramPostErrors
{
    private HttpClient _httpClient;

    [HttpPost]
    public async Task<IActionResult> PostErrors(string error, string authorizationId, string url, string tail)
    {
        var restClient = new RestClient(url);
        var request = new RestRequest(tail, Method.Post);

        // request.AddHeader("Secret-Id", authorizationId);
        request.AddParameter(error, ParameterType.RequestBody);

        var response = await restClient.ExecuteAsync(request);

        var data = response.Content;

        if (data == null)
        {
            return NoContent();
        }
        
        return Ok(data);
    }
}