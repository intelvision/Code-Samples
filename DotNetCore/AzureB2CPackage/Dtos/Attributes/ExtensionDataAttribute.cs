// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System;
using System.Runtime.CompilerServices;

namespace AzureB2CPackage.Dtos.Attributes
{
	public class ExtensionDataAttribute : Attribute
	{
		private readonly string _propertyName;

		public ExtensionDataAttribute([CallerMemberName] string propertyName = null)
		{
			_propertyName = propertyName;
		}
	}
}
