// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AzureB2CPackage.Dtos;
using AzureB2CPackage.Dtos.Pagination;

namespace AzureB2CPackage.Services.Interfaces
{
	public interface IB2CAccountService
	{
		Task<B2CAccountDto> CreateAccount(B2CAccountDto b2CAccountDto, CancellationToken cancellationToken);
		Task<B2CAccountDto> UpdateAccount(B2CAccountDto b2CAccountDto, CancellationToken cancellationToken);
		Task<B2CAccountDto> GetAccountById(Guid id, CancellationToken cancellationToken);

		Task<IEnumerable<B2CAccountDto>> GetAccounts(
			FilterDto filterDto,
			PaginationDto paginationDto,
			CancellationToken cancellationToken
		);
	}
}
