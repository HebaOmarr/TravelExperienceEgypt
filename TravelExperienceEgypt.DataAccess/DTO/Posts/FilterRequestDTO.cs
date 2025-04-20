namespace TravelExperienceEgypt.DataAccess.DTO.Posts
{
    public class FilterRequestDTO
    {
        public int? GovermantateId { get; set; }
        public int? CategoryId { get; set; }

        //If User Enter 100 it mean that Price will cost 100 at most (Explination of How filteration work)
        public decimal? Price { get; set; }

        //Rate is From 1 to 5
        //If User Enter 3 ,I will Get him all post that Rate is 3,2,1 Sort des (Explination of How filteration work)
        public float? Rate { get; set; }
    }
}
