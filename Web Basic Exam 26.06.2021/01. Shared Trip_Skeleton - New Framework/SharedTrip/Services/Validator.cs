using SharedTrip.Models;
using SharedTrip.Models.Trip;
using System;
using System.Collections.Generic;
using System.Globalization;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateTripAdding(AddingTripModel trip)
        {
            var errors = new List<string>();

            var isDepartureDate = DateTime.TryParseExact(trip.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var departureTime);

            if(trip.StartPoint == null)
            {
                errors.Add("Missing start point.");
            }

            if (trip.EndPoint == null)
            {
                errors.Add("Missing end point.");
            }

            if (!isDepartureDate)
            {
                errors.Add("Invalid date.It should be in format: dd.MM.yyyy HH: mm");
            }

            if (trip.Seats < SeatMinValue || trip.Seats > SeatMaxValue)
            {
                errors.Add($"Seats should be between {SeatMinValue} and {SeatMaxValue}");
            }

            if (trip.Description == null || trip.Description.Length>DescriptionMaxLength)
            {
                errors.Add($"Description should be with maximum {DescriptionMaxLength} symbols.");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }
    }
}
