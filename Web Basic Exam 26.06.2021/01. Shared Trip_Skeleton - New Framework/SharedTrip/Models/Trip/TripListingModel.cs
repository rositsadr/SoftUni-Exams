﻿using SharedTrip.Models.Trip;

namespace SharedTrip.Models
{
    public class TripListingModel
    {
        public string Id { get; init; }

        public string StartPoint { get; init; }

        public string EndPoint { get; init; }

        public string DepartureTime { get; init; }

        public int Seats { get; init; }
    }
}
