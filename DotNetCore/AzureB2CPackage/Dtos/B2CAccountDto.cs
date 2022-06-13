// Copyright (c) Microsoft.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System;
using AzureB2CPackage.Dtos.Attributes;
using AzureB2CPackage.Dtos.Interfaces;

namespace AzureB2CPackage.Dtos
{
	public class B2CAccountDto : IB2CEntity
	{
		public string Id { get; set; }
		public string DisplayName { get; set; }
		public string Mail { get; set; }
		public string MobilePhone { get; set; }
		public string Country { get; set; }
		public string Password { get; set; }

		[ExtensionData] public DateTime? BirthDate { get; set; }
		[ExtensionData] public string AccountImageUrl { get; set; }
		[ExtensionData] public string OrganisationName { get; set; }
		[ExtensionData] public string PhysicalAddress { get; set; }
		[ExtensionData] public string CustomerStripeId { get; set; }
		[ExtensionData] public bool IsSuperAdmin { get; set; }
	}
}
