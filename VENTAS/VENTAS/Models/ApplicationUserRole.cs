using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VENTAS.Models
{
   
        public class ApplicationUserRole : IdentityUserRole<string>
        {
            public ApplicationUserRole() : base()
            {

            }

            public virtual ApplicationUser User { get; set; }

            public virtual ApplicationRole Role { get; set; }
        }
    
}
