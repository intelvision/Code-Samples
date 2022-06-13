// Copyright (c) Microsoft.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using FileStoragePackage.Dtos;

namespace FileStoragePackage.Services.Interfaces
{
	public interface IBlobStorageService
	{
		Task<UploadedBlobResponseDto> UploadSingleBlob(
			Stream fileStream,
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		);

		Task<BlobProperties> GetBlobInfo(
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		);

		Task DeleteSingleBlob(
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		);

		Task<Uri> GenerateSasToken(
			TimeSpan accessDuration,
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		);

		Task RenameBlob(
			string containerName,
			string oldBlobName,
			string newBlobName,
			CancellationToken cancellationToken
		);

		Task<Stream> GetBlobStream(
			string containerName,
			string blobName,
			CancellationToken cancellationToken
		);
	}
}
