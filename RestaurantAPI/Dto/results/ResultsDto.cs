namespace RestaurantAPI.Dto
{
    public class ResultsDto
    {
        public int statusCode { get; set; }
        public string? msg { get; set; }
        public IResult? data { get; set; }
    }
}
