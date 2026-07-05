using AppointmentBookingAPI.CurrentUser;
using AppointmentBookingAPI.Modules.Appointment.Specialist;
using AppointmentBookingAPI.Modules.Auth.User;

namespace AppointmentBookingAPI.Helpers
{
    public class OwnershipService
    {
        private readonly CurrentUserService _currentUserService;
        private readonly UserService _userService;
        private readonly SpecialistService _specialistService;

        

        public OwnershipService(
        CurrentUserService currentUserService,
        UserService userService,
        SpecialistService specialistService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
            _specialistService = specialistService;
        }

        public int GetCurrentPersonID()
        {
            int userID = _currentUserService.GetUserID();
            string currentUser = _currentUserService.GetUsername();

            var user = _userService.GetByID(userID, currentUser);

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            return user.PersonID;
        }

        public int GetCurrentSpecialistID()
        {
            int personID = GetCurrentPersonID();
            string currentUser = _currentUserService.GetUsername();

            var specialist = _specialistService.GetByPersonID(personID, currentUser);

            if (specialist == null)
                throw new UnauthorizedAccessException("Specialist not found.");

            return specialist.SpecialistID;
        }


    }
}
