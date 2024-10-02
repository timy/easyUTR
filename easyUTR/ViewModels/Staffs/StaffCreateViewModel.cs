using Microsoft.AspNetCore.Mvc.Rendering;

namespace easyUTR.ViewModels.Staffs
{
    public class StaffCreateViewModel
    {
        public StaffCreateDTO SubmitDTO { get; set; } = new StaffCreateDTO();
        public SelectList StoreList { get; set; }
        public SelectList JobList { get; set; }
    }
}
