using Orleans.TestingHost;
using PortfolioOrleans.Grains;

namespace PortfolioOrleans.Tests.Grains;

[Collection(nameof(ClusterCollection))]
public class UrlShortenerGrainTests(ClusterFixture fixture)
{
    private readonly InProcessTestCluster _cluster = fixture.Cluster;

    [Fact]
    public async Task SetUrl_AndGetUrl_ReturnsStoredUrl()
    {
        var shortCode = "ABC123";
        var fullUrl = "https://learn.microsoft.com";

        var grain = _cluster.Client.GetGrain<IUrlShortenerGrain>(shortCode);
        await grain.SetUrl(fullUrl);

        var result = await grain.GetUrl();

        Assert.Equal(fullUrl, result);
    }

    [Fact]
    public async Task GetUrl_WithoutSetUrl_ReturnsEmptyString()
    {
        var shortCode = "XYZ789";

        var grain = _cluster.Client.GetGrain<IUrlShortenerGrain>(shortCode);
        var result = await grain.GetUrl();

        Assert.Equal("", result);
    }

    [Fact]
    public async Task SetUrl_UpdatesExistingUrl()
    {
        var shortCode = "UPDATE";
        var firstUrl = "https://first.example.com";
        var secondUrl = "https://second.example.com";

        var grain = _cluster.Client.GetGrain<IUrlShortenerGrain>(shortCode);
        await grain.SetUrl(firstUrl);
        await grain.SetUrl(secondUrl);

        var result = await grain.GetUrl();

        Assert.Equal(secondUrl, result);
    }
}
