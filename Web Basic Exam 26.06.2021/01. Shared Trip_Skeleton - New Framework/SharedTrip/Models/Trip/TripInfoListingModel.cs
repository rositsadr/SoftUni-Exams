namespace SharedTrip.Models.Trip
{
    public class TripInfoListingModel
    {
        public string Id { get; set; }
        public string StartPoint { get; init; }
        public string EndPoint { get; init; }
        public string DepartureTime { get; init; }
        public string Seats { get; init; }
        public string Description { get; init; }
        public string ImagePath { get; init; }               
    }
}
