using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DevBooks.Web.Models
{
    public class RoleEvaluator
    {
        public bool CanEdit
        {
            get
            {
                return Roles.IsUserInRole("Admin") ||
                        Roles.IsUserInRole("Manager") ||
                        Roles.IsUserInRole("User");
            }
        }

        public bool CanDelete
        {
            get
            {
                return Roles.IsUserInRole("Admin") ||
                        Roles.IsUserInRole("Manager");
            }
        }
    }
}