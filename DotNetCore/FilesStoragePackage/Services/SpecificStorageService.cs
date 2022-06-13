// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileStoragePackage.Dtos;
using FileStoragePackage.Services.Interfaces;

namespace FileStoragePackage.Services
{
	public class SpecificStorageService : ISpecificStorageService
	{
		private readonly IBlobStorageService _blobStorageService;

		public SpecificStorageService(
			IBlobStorageService blobStorageService
			)
		{
			_blobStorageService = blobStorageService;
		}

		public Task<UploadedBlobResponseDto> UpdateAccountImage(
			Stream fileStream,
			Guid specificEntityId,
			BlobOptionsDto blobOptionsDto,
			CancellationToken cancellationToken
			)
		{
			return _blobStorageService.UploadSingleBlob(
				fileStream,
				"specifics",
				$"{specificEntityId}/{blobOptionsDto.FileName}.{blobOptionsDto.FileExtension.Replace(".", "")}",
				cancellationToken
			);
		}

		public Task RemoveAccountImage(
			Guid specificEntityId,
			BlobOptionsDto blobOptionsDto,
			CancellationToken cancellationToken
			)
		{
			return _blobStorageService.DeleteSingleBlob(
				"specifics",
				$"{specificEntityId}/{blobOptionsDto.FileName}.{blobOptionsDto.FileExtension.Replace(".", "")}",
				cancellationToken
			);
		}
	}
}
