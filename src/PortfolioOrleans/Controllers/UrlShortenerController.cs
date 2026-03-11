using Microsoft.AspNetCore.Mvc;
using PortfolioOrleans.Grains;

namespace PortfolioOrleans.Controllers;

[ApiController]
[Route("")]
public class UrlShortenerController(IGrainFactory grainFactory) : ControllerBase
{
    [HttpGet]
    public IActionResult Get() =>
        Ok("Welcome to the URL shortener, powered by Orleans!");

    [HttpGet("shorten")]
    public async Task<IActionResult> Shorten([FromQuery] string url)
    {
        var host = $"{Request.Scheme}://{Request.Host.Value}";

        if (string.IsNullOrWhiteSpace(url) ||
            Uri.IsWellFormedUriString(url, UriKind.Absolute) is false)
        {
            return BadRequest($"""
                The URL query string is required and needs to be well formed.
                Consider, {host}/shorten?url=https://www.microsoft.com.
                """);
        }

        var shortenedRouteSegment = Guid.NewGuid().GetHashCode().ToString("X");

        var shortenerGrain = grainFactory.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);
        await shortenerGrain.SetUrl(url);

        var resultBuilder = new UriBuilder(host)
        {
            Path = $"/go/{shortenedRouteSegment}"
        };

        return Ok(resultBuilder.Uri);
    }

    [HttpGet("go/{shortenedRouteSegment}")]
    public async Task<IActionResult> Go(string shortenedRouteSegment)
    {
        var shortenerGrain = grainFactory.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);
        var url = await shortenerGrain.GetUrl();

        var redirectBuilder = new UriBuilder(url);

        return Redirect(redirectBuilder.Uri.ToString());
    }
}
