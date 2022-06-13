// Copyright (c) Microsoft.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System;
using Azure.Identity;
using Azure.Storage.Blobs;
using FileStoragePackage.Dtos;
using FileStoragePackage.Services;
using FileStoragePackage.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileStoragePackage
{
	public static class BlobStorageDependencyInjectionContainer
	{
		public static IServiceCollection AddBlobStorage(this IServiceCollection services, string storageUrl, AzureB2CSettings azureSettings)
		{
			services.AddTransient(sp =>
			{
				var cred = new ClientSecretCredential(
					azureSettings.GraphTenantId,
					azureSettings.GraphClientId,
					azureSettings.GraphClientSecret
				);
				return new BlobServiceClient(new Uri(storageUrl), cred);
			});

			services.AddTransient<IBlobStorageService, BlobStorageService>();
			services.AddTransient<ISpecificStorageService, SpecificStorageService>();

			return services;
		}
	}
}
