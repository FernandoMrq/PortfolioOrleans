using Orleans.Runtime;
using PortfolioOrleans.Models;

namespace PortfolioOrleans.Grains;

public sealed class UrlShortenerGrain(
    [PersistentState(stateName: "url", storageName: "urls")]
    IPersistentState<UrlDetails> state)
    : Grain, IUrlShortenerGrain
{
    public async Task SetUrl(string fullUrl)
    {
        state.State = new UrlDetails
        {
            ShortenedRouteSegment = this.GetPrimaryKeyString(),
            FullUrl = fullUrl
        };

        await state.WriteStateAsync();
    }

    public Task<string> GetUrl() =>
        Task.FromResult(state.State?.FullUrl ?? "");
}
