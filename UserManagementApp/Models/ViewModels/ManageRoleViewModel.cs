﻿using System.Runtime.CompilerServices;

namespace UserManagementApp.Models.ViewModels
{
    public class ManageRoleViewModel
    {
        public string RoleName { get; set; }
        public List<RolesToReturnViewModel> RolesToReturn { get; set; }= new List<RolesToReturnViewModel>();
    }
}
