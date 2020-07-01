using System;

namespace MD.ExpressionPredicateBuilder.XUnitTest.Models
{
    public class Entity1
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public DateTime? RegisterDateTimeNullable { get; set; }
        public PersianDateTime.Standard.PersianDateTime RegisterPersianDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
