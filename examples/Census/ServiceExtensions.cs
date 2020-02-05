using Microsoft.Extensions.DependencyInjection;
using System;

namespace Runly.Examples.Census
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddCensusProcesses(this IServiceCollection services)
		{
			services.AddHttpClient<IDownloader, HttpDownloader>((s, client) =>
			{
				// We can use the passed-in config to configure any of our dependencies,
				// in this case the HttpDownlaoder needs the baseUrl.
				var config = s.GetRequiredService<CensusConfig>();
				client.BaseAddress = new Uri(config.BaseUrl);
			});

			// We are pretending this fake dependency is not thread-safe. We can register
			// it as scoped and we will get a new instance in the process for each thread
			// it creates. If we change the registration to Transient, we will get a new
			// instance in the process for each item that it processes.
			services.AddScoped<IDatabase, FakeDatabase>();

			return services;
		}
	}
}
