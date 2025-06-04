var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("Postgres")
    .WithDataVolume()
    .WithPgAdmin();
var db = postgres.AddDatabase("PRN231");

var redis = builder.AddRedis("Redis")
    .WithDataVolume()
    .WithRedisInsight();

builder.AddProject<Projects.PRN231_API>("PRN231-API")
    .WithReference(db)
    .WithReference(redis)
    .WaitFor(db)
    .WaitFor(redis);

builder.Build().Run();
