using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Data.Models
{
    public class Trip
    {
        /*•	Has an Id – a string, Primary Key
            •	Has a StartPoint – a string (required)
            •	Has a EndPoint – a string (required)
            •	Has a DepartureTime – a datetime (required) 
            •	Has a Seats – an integer with min value 2 and max value 6 (required)
            •	Has a Description – a string with max length 80 (required)
            •	Has a ImagePath – a string
            •	Has UserTrips collection – a UserTrip type
            */
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        [Range(SeatMinValue,SeatMaxValue)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();
    }
}
