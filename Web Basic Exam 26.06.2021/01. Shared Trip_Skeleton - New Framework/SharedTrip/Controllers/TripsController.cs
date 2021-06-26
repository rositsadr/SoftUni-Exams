using Microsoft.EntityFrameworkCore;
using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using SharedTrip.Models.Trip;
using SharedTrip.Services;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController:Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;

        public TripsController(ApplicationDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var trips = data.Trips
                .Select(t => new TripListingModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Seats = t.Seats,
                })
                .ToList();

            return View(trips);
        }
        
        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var tripsId = data.Trips.Select(t => t.Id).ToList();

            if (!tripId.Contains(tripId))
            {
                return BadRequest();
            }    

            var trip = data.Trips
                .Find(tripId);

            return View(trip);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddingTripModel model)
        {
            var errors = validator.ValidateTripAdding(model);

            if (errors.Any())
            {
                //return Error(errors);
                return Redirect("/Trips/Add");
            }

            var trip = new Trip
            {
                DepartureTime = model.DepartureTime,
                EndPoint = model.EndPoint,
                StartPoint = model.StartPoint,
                Seats = model.Seats,
                Description = model.Description,
                ImagePath = model.ImagePath
            };

            data.Trips.Add(trip);
            data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var tripsId = data.Trips.Select(t => t.Id).ToList();

            if (!tripId.Contains(tripId))
            {
                return BadRequest();
            }

            var trip = data.Trips
                .Include(t => t.UserTrips)
                .Where(t => t.Id == tripId)
                .FirstOrDefault();

            if(trip.Seats>0)
            {
                if (!trip.UserTrips.Select(u => u.UserId).Contains(User.Id))
                {
                    var userTrip = new Data.Models.UserTrip
                    {
                        UserId = User.Id,
                        TripId = tripId
                    };

                    data.UsersTrips.Add(userTrip);
                    data.SaveChanges();

                    trip.Seats--;
                    data.SaveChanges();
                }
                else
                {
                    //return Error("You are already booked for this trip.");
                    return Redirect($"/Trips/Details?tripId={tripId}");
                }
            }
            return Redirect("/Trips/All");
        }
    }
}
