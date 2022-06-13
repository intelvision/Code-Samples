// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System.Linq;
using System.Reflection;
using AzureB2CPackage.Dtos;
using AzureB2CPackage.Dtos.Attributes;
using AzureB2CPackage.Dtos.Interfaces;
using AzureB2CPackage.Services.Interfaces;

namespace AzureB2CPackage.Services
{
	public class B2CEntityService<TEntity> : IB2CEntityService<TEntity> where TEntity : IB2CEntity
	{
		private readonly AzureB2CSettings _settingsAccessor;

		public B2CEntityService(AzureB2CSettings settingsAccessor)
		{
			_settingsAccessor = settingsAccessor;
		}

		public string CreateExtensionName(string name)
		{
			return
				$"extension_{_settingsAccessor.B2CExtensionsApplicationClientId.Replace("-", string.Empty)}_{name}";
		}

		public string FormSelectQuery()
		{
			return string.Join(",", typeof(TEntity)
				.GetProperties()
				.Select(x =>
					GetQueryParamByPropertyInfo(x)
				)
			);
		}

		public string GetQueryParamByPropertyInfo(PropertyInfo propertyInfo)
		{
			return propertyInfo.IsDefined(typeof(ExtensionDataAttribute), false)
				? CreateExtensionName(propertyInfo.Name)
				: propertyInfo.Name;
		}
		public string GetQueryParamByPropertyName(string propertyName)
		{

			return typeof(TEntity).GetProperty(propertyName).IsDefined(typeof(ExtensionDataAttribute), false)
				? CreateExtensionName(propertyName)
				: propertyName;
		}
	}
}
