// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace AzureB2CPackage.Dtos.Pagination
{
	public enum SortOrder
	{
		/// <summary>The default. No sort order is specified.</summary>
		/// <value>-1</value>
		/// <returns>-1</returns>
		Unspecified = -1,

		/// <summary>Rows are sorted in ascending order.</summary>
		/// <value>0</value>
		/// <returns>0</returns>
		Ascending = 0,

		/// <summary>Rows are sorted in descending order.</summary>
		/// <value>1</value>
		/// <returns>1</returns>
		Descending = 1
	}
}
