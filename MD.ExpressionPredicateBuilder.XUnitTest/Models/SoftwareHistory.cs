using System;
using System.ComponentModel.DataAnnotations;

namespace MD.ExpressionPredicateBuilder.XUnitTest.Models
{
    public class SoftwareHistory
    {
        [Key]
        public long Id { get; set; }

        public string CreatedBy { get; set; }
        public string ClientIP { get; set; }
        public string Path { get; set; }
        public string RequestQueryString { get; set; }
        public string RequestBody { get; set; }
        public DateTime RequestDateTime { get; set; }
        public PersianDateTime.Standard.PersianDateTime RequestDateTimePersian => new PersianDateTime.Standard.PersianDateTime(RequestDateTime);
        public string ResponseBody { get; set; }
        public int ResponseStatusCode { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public PersianDateTime.Standard.PersianDateTime ResponseDateTimePersian => new PersianDateTime.Standard.PersianDateTime(ResponseDateTime);
        public TimeSpan ResponseTimeSpan => ResponseDateTime - RequestDateTime;
        public bool ServiceResultSucceed { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
    }
}
