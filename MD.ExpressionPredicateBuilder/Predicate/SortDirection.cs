using System;
using System.Runtime.Serialization;

namespace MD.ExpressionPredicateBuilder.Predicate
{
	[Serializable]
	[DataContract]
	public enum SortDirection
	{
		[EnumMember]
		Ascending = 1,

		[EnumMember]
		Descending = 2
	}

}
