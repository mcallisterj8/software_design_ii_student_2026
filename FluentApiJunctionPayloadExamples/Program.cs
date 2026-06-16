using Microsoft.Extensions.DependencyInjection;
using FluentApiJunctionPayloadExamples.Data;

ServiceProvider _serviceProvider;

// Create service collection
var services = new ServiceCollection();

services.AddDbContext<ApplicationDbContext>();