// Copyright (c) Microsoft.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using FileStoragePackage.Dtos;
using FileStoragePackage.Services.Interfaces;

namespace FileStoragePackage.Services
{
	public class BlobStorageService : IBlobStorageService
	{
		private readonly BlobServiceClient _blobServiceClient;

		public BlobStorageService(BlobServiceClient blobServiceClient)
		{
			_blobServiceClient = blobServiceClient;
		}

		public virtual async Task<UploadedBlobResponseDto> UploadSingleBlob(
			Stream fileStream,
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		)
		{
			var container = _blobServiceClient.GetBlobContainerClient(containerName);
			await container.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);

			await container.GetBlobClient(blobName).UploadAsync(fileStream, true, cancellationToken);

			return new UploadedBlobResponseDto { BlobUrl = container.GetBlobClient(blobName).Uri.ToString() };
		}

		public virtual async Task<BlobProperties> GetBlobInfo(
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		)
		{
			var container = _blobServiceClient.GetBlobContainerClient(containerName);
			await container.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);

			return await container.GetBlobClient(blobName).GetPropertiesAsync(cancellationToken: cancellationToken);
		}

		public virtual async Task<Uri> GenerateSasToken(
			TimeSpan accessDuration,
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		)
		{
			// Create a SAS token that's also valid for 7 days.
			var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
			var blobClient = blobContainerClient.GetBlobClient(blobName); // blob name

			// Get a user delegation key for the Blob service that's valid for 2 hours.
			var userDelegationKey = await _blobServiceClient.GetUserDelegationKeyAsync(
				DateTimeOffset.UtcNow.AddHours(-1),
				DateTimeOffset.UtcNow.AddHours(2),
				cancellationToken);

			var sasBuilder = new BlobSasBuilder()
			{
				BlobContainerName = blobClient.BlobContainerName,
				BlobName = blobClient.Name,
				Resource = "b", // b for blob, c for container
				StartsOn = DateTimeOffset.UtcNow.AddHours(-1),
				ExpiresOn = DateTimeOffset.UtcNow.Add(accessDuration),
			};

			sasBuilder.SetPermissions(BlobSasPermissions.Read); // read permissions
																// Add the SAS token to the container URI.
			var blobUriBuilder = new BlobUriBuilder(blobClient.Uri)
			{
				Sas = sasBuilder.ToSasQueryParameters(userDelegationKey, _blobServiceClient.AccountName)
			};

			return blobUriBuilder.ToUri();
		}

		public virtual async Task DeleteSingleBlob(
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		)
		{
			var container = _blobServiceClient.GetBlobContainerClient(containerName);

			await container.DeleteBlobIfExistsAsync(blobName, cancellationToken: cancellationToken);
		}

		public virtual Task<Stream> GetBlobStream(
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		)
		{
			var container = _blobServiceClient.GetBlobContainerClient(containerName);

			var client = container.GetBlobClient(blobName);
			return client.OpenReadAsync(cancellationToken: cancellationToken);
		}

		public async Task RenameBlob(
			string containerName,
			string oldBlobName,
			string newBlobName,
			CancellationToken cancellationToken
		)
		{
			var container = _blobServiceClient.GetBlobContainerClient(containerName);

			var oldBlob = container.GetBlobClient(oldBlobName);
			var newBlob = container.GetBlobClient(newBlobName);

			using (var stream = new MemoryStream())
			{
				await oldBlob.DownloadToAsync(stream, cancellationToken);

				stream.Seek(0, SeekOrigin.Begin);
				await newBlob.UploadAsync(stream, cancellationToken);

				//copy metadata here if you need it too

				await oldBlob.DeleteAsync(cancellationToken: cancellationToken);
			}
		}
	}
}
