using System;
using System.Runtime.Serialization;

namespace MD.ExpressionPredicateBuilder.Predicate
{
	[Serializable]
	[DataContract]
	public class Condition
	{
		[DataMember]
		public Guid Id { get; internal set; }

		[DataMember]
		public string EntityTypeName { get; set; }

		[DataMember]
		public ConditionTree Tree { get; set; }
	}
}
