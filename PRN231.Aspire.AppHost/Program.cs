var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PRN231_API>("prn231-api");

builder.Build().Run();
