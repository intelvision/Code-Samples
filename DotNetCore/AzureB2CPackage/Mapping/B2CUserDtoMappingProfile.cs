// Copyright (c) Microsoft.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using AzureB2CPackage.Dtos;
using AzureB2CPackage.Dtos.Attributes;
using Microsoft.Graph;

namespace AzureB2CPackage.Mapping
{
	public class B2CUserDtoMappingProfile : Profile
	{
		public B2CUserDtoMappingProfile()
		{
			CreateMap<B2CAccountDto, User>()
				.ForMember(dist => dist.AdditionalData, opt => opt.MapFrom(src =>
						typeof(B2CAccountDto).GetProperties()
							.Where(prop => prop.IsDefined(typeof(ExtensionDataAttribute), false))
							.Select(x => x.Name)
							.Select(propertyName =>
								new KeyValuePair<string, object>(
									CreateExtensionName(propertyName),
									src.GetType().GetProperty(propertyName).GetValue(src, null)
								))
							.ToDictionary(x => x.Key, x => x.Value)
					)
				)
				.ForMember(dist => dist.AccountEnabled, opt => opt.MapFrom(src => true))
				;

			CreateMap<User, B2CAccountDto>()
				.ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dist => dist.PhysicalAddress,
					opt => opt.MapFrom(src =>
						src.AdditionalData
							.GetOrDefault(CreateExtensionName(nameof(B2CAccountDto.PhysicalAddress)))
							.ToString()))
				.ForMember(dist => dist.OrganisationName,
					opt => opt.MapFrom(src =>
						src.AdditionalData
							.GetOrDefault(CreateExtensionName(nameof(B2CAccountDto.OrganisationName)))
							.ToString()))
				.ForMember(dist => dist.AccountImageUrl,
					opt => opt.MapFrom(src =>
						src.AdditionalData
							.GetOrDefault(CreateExtensionName(nameof(B2CAccountDto.AccountImageUrl)))
							.ToString()))
				.ForMember(dist => dist.BirthDate,
					opt => opt.MapFrom(src =>
						src.AdditionalData
							.GetOrDefault(CreateExtensionName(nameof(B2CAccountDto.BirthDate)))
							.ToString()))

				.ForMember(dist => dist.IsSuperAdmin,
					opt => opt.MapFrom(src =>
						src.AdditionalData
							.GetOrDefault(CreateExtensionName(nameof(B2CAccountDto.IsSuperAdmin)))
							.ToString()))
				.ForMember(dist => dist.CustomerStripeId,
					opt => opt.MapFrom(src =>
						src.AdditionalData
							.GetOrDefault(CreateExtensionName(nameof(B2CAccountDto.CustomerStripeId)))
							.ToString()))
				;
		}

		private static string CreateExtensionName(string name)
		{
			return
				$"extension_{Environment.GetEnvironmentVariable("B2C_EXTENSION_CLIENT_ID")?.Replace("-", string.Empty)}_{name}";
		}
	}
}
