// Copyright (c) Microsoft.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System.Net.Http.Headers;
using System.Reflection;
using AzureB2CPackage.Dtos;
using AzureB2CPackage.Services;
using AzureB2CPackage.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace AzureB2CPackage
{
	public static class GraphDependencyInjectionContainer
	{
		public static IServiceCollection AddGraph(this IServiceCollection services, AzureB2CSettings settingAccessor)
		{
			var scopes = new[] { "https://graph.microsoft.com/.default" };

			services.AddScoped(sp => new GraphServiceClient(new DelegateAuthenticationProvider(
				async requestMessage =>
				{
					var confidentialClientApplication = ConfidentialClientApplicationBuilder
						.Create(settingAccessor.GraphClientId)
						.WithTenantId(settingAccessor.GraphTenantId)
						.WithClientSecret(settingAccessor.GraphClientSecret)
						.Build();

					// Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
					var authResult = await confidentialClientApplication
						.AcquireTokenForClient(scopes)
						.ExecuteAsync();

					// Add the access token in the Authorization header of the API request.
					requestMessage.Headers.Authorization =
						new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
				})));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddSingleton(_ => settingAccessor);
			services.AddTransient<IB2CAccountService, B2CAccountService>();
			services.AddTransient(typeof(IB2CEntityService<>), typeof(B2CEntityService<>));

			return services;
		}
	}
}
