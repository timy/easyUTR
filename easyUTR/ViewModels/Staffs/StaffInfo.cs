using Microsoft.Extensions.Logging.Abstractions;

namespace easyUTR.ViewModels.Staffs
{
    public class StaffInfo
    {
        public string StaffId { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string StoreName { get; set; } = null!;

        public string JobName { get; set; } = null!;

        public int? JobLevel { get; set; }
    }
}
