using System.ComponentModel.DataAnnotations;

namespace easyUTR.ViewModels.Staffs
{
    public class StaffCreateDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "StoreId is required.")]
        public int StoreId { get; set; }

        [Required(ErrorMessage = "JobId is required.")]
        public int JobId { get; set; }
    }
}
