using System;
namespace Model
{
    public class Measurement
    {
        public string Parameter { get; set; }
        public Date Date { get; set; }
        public decimal Value { get; set; }
        public string Unit { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string SourceName { get; set; }
    }


    public class Date
    {
        public DateTime Utc { get; set; }
        public DateTime Local { get; set; }
    }
}
