// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using System.Collections.Generic;

namespace AzureB2CPackage.Dtos.Pagination
{
	public class FilterDto
	{
		public FilterDto()
		{
			ContainsFilters = new Dictionary<string, ContainsDto>();
			EqualityFilters = new Dictionary<string, EqualsDto>();
			RangeFilters = new Dictionary<string, RangeDto>();
		}

		public IDictionary<string, ContainsDto> ContainsFilters { get; set; }

		public IDictionary<string, EqualsDto> EqualityFilters { get; set; }

		public IDictionary<string, RangeDto> RangeFilters { get; set; }

		public string SortBy { get; set; }

		public SortOrder? SortOrder { get; set; }
	}
}
