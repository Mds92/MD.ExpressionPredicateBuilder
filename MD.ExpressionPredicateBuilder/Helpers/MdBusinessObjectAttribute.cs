using System;

namespace MD.ExpressionPredicateBuilder.Helpers
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MdBusinessObjectFlagAttribute : Attribute
	{
		public Type EntityType { get; set; }
	}
}