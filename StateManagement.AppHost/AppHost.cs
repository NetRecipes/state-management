using CommunityToolkit.Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var redisPassword = builder.AddParameter("RedisPassword", true);
var redisPort = builder.AddParameter("RedisPort");

var portValue = await redisPort.Resource.GetValueAsync(CancellationToken.None);

var redis = builder
    .AddRedis("redis", int.Parse(portValue!), redisPassword)
    .WithRedisInsight();

var servicea = builder
    .AddProject<Projects.ServiceA>("service-a")
    .WithExternalHttpEndpoints()
    .WaitFor(redis)
    .WithDaprSidecar(new DaprSidecarOptions
    {
        ResourcesPaths = [Path.Combine("..", "components")]
    });

builder.Build().Run();
