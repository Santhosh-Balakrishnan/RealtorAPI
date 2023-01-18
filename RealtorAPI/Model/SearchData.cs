namespace RealtorAPI.Model
{
    public class SearchData
    {
        public string Category { get; set; }
        public Range Area { get; set; }
        public Range Price { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int NoOfBeds { get; set; }
        public int NoOfBaths { get; set; }
        public int? AgentId { get; set; }

        public SearchData()
        {
            NoOfBaths = 1;
            NoOfBeds = 1;
            Area = new Range();
            Price = new Range();
        }
    }
    public class Range
    {
        public double Min { get; set; } = double.MinValue;
        public double Max { get; set; }=double.MaxValue;
    }
    
}
