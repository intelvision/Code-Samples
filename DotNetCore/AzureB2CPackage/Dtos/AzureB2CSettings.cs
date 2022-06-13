// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace AzureB2CPackage.Dtos
{
	public class AzureB2CSettings
	{
		public string GraphClientId { get; set; }
		public string B2CExtensionsApplicationClientId { get; set; }
		public string B2CSignInPolicy { get; set; }
		public string GraphTenantId { get; set; }
		public string GraphClientSecret { get; set; }
		public string GraphTenant { get; set; }
		public string GraphAuthority { get; set; }
	}
}
