using SharedTrip.Models;
using SharedTrip.Models.Trip;
using System.Collections.Generic;

namespace SharedTrip.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUserRegistration(RegisterUserModel user);

        ICollection<string> ValidateTripAdding(AddingTripModel trip);
    }
}
