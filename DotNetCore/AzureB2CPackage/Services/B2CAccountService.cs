// Copyright (c) Microsoft.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AzureB2CPackage.Constants;
using AzureB2CPackage.Dtos;
using AzureB2CPackage.Dtos.Pagination;
using AzureB2CPackage.Services.Interfaces;
using Microsoft.Graph;

namespace AzureB2CPackage.Services
{
	public class B2CAccountService : IB2CAccountService
	{
		private readonly IB2CEntityService<B2CAccountDto> _b2CEntityService;
		private readonly GraphServiceClient _graphClient;
		private readonly IMapper _mapper;
		private readonly AzureB2CSettings _settingsAccessor;

		public B2CAccountService(
			GraphServiceClient graphClient,
			IB2CEntityService<B2CAccountDto> b2CEnityService,
			AzureB2CSettings settingsAccessor,
			IMapper mapper
		)
		{
			_graphClient = graphClient;
			_b2CEntityService = b2CEnityService;
			_settingsAccessor = settingsAccessor;
			_mapper = mapper;
		}

		public async Task<B2CAccountDto> CreateAccount(
			B2CAccountDto b2CAccountDto,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var user = _mapper.Map<User>(b2CAccountDto);

				user.Identities =
					new List<ObjectIdentity>
					{
						new()
						{
							SignInType = SignInTypes.EmailAddress,
							Issuer = _settingsAccessor.GraphTenant,
							IssuerAssignedId = b2CAccountDto.Mail
						}
					};
				user.PasswordProfile = new PasswordProfile
				{
					Password = b2CAccountDto.Password,
					ForceChangePasswordNextSignIn = true
				};

				var createdUser = await _graphClient.Users.Request()
					.Select(_b2CEntityService.FormSelectQuery())
					.AddAsync(user, cancellationToken);

				return _mapper.Map<B2CAccountDto>(createdUser);
			}
			catch (Exception e) when (e.Message.Contains("Request_BadRequest") && e.Message.Contains("A password must be specified to create a new user"))
			{
				throw new Exception("A password must be specified to create a new user");
			}
			catch (Exception e) when (e.Message.Contains("Request_BadRequest") && e.Message.Contains("Another object with the same value for property proxyAddresses already exists"))
			{
				throw new Exception("Account with such an email already exists");
			}
			catch (Exception e)
			{
				throw new Exception("Unknown error", e);
			}
		}

		public async Task<B2CAccountDto> UpdateAccount(
			B2CAccountDto b2CAccountDto,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var user = _mapper.Map<User>(b2CAccountDto);

				await _graphClient.Users[b2CAccountDto.Id].Request()
					.Select(_b2CEntityService.FormSelectQuery())
					.UpdateAsync(user, cancellationToken);

				var updatedUser = await _graphClient.Users[b2CAccountDto.Id]
					.Request()
					.Select(_b2CEntityService.FormSelectQuery())
					.GetAsync(cancellationToken);

				return _mapper.Map<B2CAccountDto>(updatedUser);
			}
			catch (Exception e) when (e.Message.Contains("Request_ResourceNotFound"))
			{
				throw new Exception("Account for this tenant is not found");
			}
			catch (Exception e) when (e.Message.Contains("Request_BadRequest"))
			{
				throw new Exception("Account with such an email already exists");
			}
		}

		public async Task<B2CAccountDto> GetAccountById(
			Guid id,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var user = await _graphClient.Users[id.ToString()]
					.Request()
					.Select(_b2CEntityService.FormSelectQuery())
					.GetAsync(cancellationToken);

				return _mapper.Map<B2CAccountDto>(user);
			}
			catch (Exception e) when (e.Message.Contains("Request_ResourceNotFound"))
			{
				throw new Exception("Account for this tenant is not found");
			}
		}

		public async Task<IEnumerable<B2CAccountDto>> GetAccounts(
			FilterDto filterDto,
			PaginationDto paginationDto,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var getUsersRequest = _graphClient.Users
					.Request()
					.Select(_b2CEntityService.FormSelectQuery())
					// .OrderBy(filterDto.SortBy) // NOT SUPPORTED AS FOR 03.12.2021
					// .Skip(paginationDto.Skip ?? 0) // NOT SUPPORTED AS FOR 03.12.2021
					.Top(paginationDto.Take);

				foreach (var filterDtoContainsFilter in filterDto.ContainsFilters)
				{
					var key = _b2CEntityService.GetQueryParamByPropertyName(filterDtoContainsFilter.Key);
					// contains method is not supported as for 03.12.2021
					getUsersRequest.Filter(
						$"startsWith({key},'{filterDtoContainsFilter.Value.Value}')");
				}

				foreach (var filterDtoEqualityFilter in filterDto.EqualityFilters)
				{
					var key = _b2CEntityService.GetQueryParamByPropertyName(filterDtoEqualityFilter.Key);
					getUsersRequest.Filter($"{key} eq '{filterDtoEqualityFilter.Value.Value}'");
				}

				var users = await getUsersRequest
					.GetAsync(cancellationToken);

				return _mapper.Map<IEnumerable<B2CAccountDto>>(users);
			}
			catch (Exception e) when (e.Message.Contains("Request_ResourceNotFound"))
			{
				throw new Exception("Account for this tenant is not found");
			}
		}
	}
}
