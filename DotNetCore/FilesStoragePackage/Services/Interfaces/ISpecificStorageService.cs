// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileStoragePackage.Dtos;

namespace FileStoragePackage.Services.Interfaces
{
	public interface ISpecificStorageService
	{
		Task<UploadedBlobResponseDto> UpdateAccountImage(
			Stream fileStream,
			Guid specificEntityId,
			BlobOptionsDto blobOptionsDto,
			CancellationToken cancellationToken
		);

		Task RemoveAccountImage(
			Guid specificEntityId,
			BlobOptionsDto blobOptionsDto,
			CancellationToken cancellationToken
		);
	}
}
