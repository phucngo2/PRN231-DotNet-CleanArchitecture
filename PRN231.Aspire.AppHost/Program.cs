var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("Postgres")
    .WithDataVolume()
    .WithPgAdmin();
var db = postgres.AddDatabase("PRN231");

builder.AddProject<Projects.PRN231_API>("PRN231-API")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
