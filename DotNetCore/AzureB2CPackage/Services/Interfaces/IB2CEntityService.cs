// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using AzureB2CPackage.Dtos.Interfaces;

namespace AzureB2CPackage.Services.Interfaces
{
	public interface IB2CEntityService<TEntity> where TEntity : IB2CEntity
	{
		string CreateExtensionName(string name);
		string GetQueryParamByPropertyName(string propertyName);
		string FormSelectQuery();
	}
}
