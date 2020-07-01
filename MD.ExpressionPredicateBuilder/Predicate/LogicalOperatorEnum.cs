using System;
using System.Runtime.Serialization;

namespace MD.ExpressionPredicateBuilder.Predicate
{
	[Serializable]
	[DataContract]
	public enum LogicalOperatorEnum
	{
		[EnumMember]
		And = 1,

		[EnumMember]
		Or = 2,

		[EnumMember]
		None = 3
	}
}
