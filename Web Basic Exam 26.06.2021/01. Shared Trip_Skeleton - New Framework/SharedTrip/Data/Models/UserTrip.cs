namespace SharedTrip.Data.Models
{
    public class UserTrip
    {
        public string TripId { get; set; }

        public Trip Trip { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}