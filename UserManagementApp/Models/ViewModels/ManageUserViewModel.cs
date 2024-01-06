namespace UserManagementApp.Models.ViewModels
{
    public class ManageUserViewModel
    {
        public string RoleName { get; set; }
        public List<UserToReturnViewModel> TableData { get; set; } = new List<UserToReturnViewModel>();
        public UserToReturnViewModel UserDetail { get; set; }
    }
}
