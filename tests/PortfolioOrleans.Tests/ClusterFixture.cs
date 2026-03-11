using Orleans.TestingHost;

namespace PortfolioOrleans.Tests;

public class ClusterFixture : IAsyncLifetime
{
    public InProcessTestCluster Cluster { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var builder = new InProcessTestClusterBuilder();
        builder.ConfigureSilo((_, siloBuilder) =>
        {
            siloBuilder.AddMemoryGrainStorage("urls");
        });

        Cluster = builder.Build();
        await Cluster.DeployAsync();
    }

    public async Task DisposeAsync() =>
        await Cluster.DisposeAsync();
}

[CollectionDefinition(nameof(ClusterCollection))]
public class ClusterCollection : ICollectionFixture<ClusterFixture>
{
}
