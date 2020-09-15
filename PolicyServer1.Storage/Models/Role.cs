using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace PolicyServer1.Models {

    public class Role {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public String Name { get; set; }
        public String Description { get; set; }

        public ICollection<Role> Parents { get; set; } = new HashSet<Role>();

        //public Int32 Id { get; set; }
        //public String Name { get; set; }
        //public Int32 PolicyId { get; set; }
        //public String Description { get; set; }
        //public Boolean Enabled { get; set; } = true;

        //public List<Role> Parents { get; internal set; } = new List<Role>();
        //public List<Permission> Permissions { get; internal set; } = new List<Permission>();


        //public List<String> IdentityRoles { get; internal set; } = new List<String>();
        //public List<String> Subjects { get; internal set; } = new List<String>();
    }

    //public class PolicyRole : Role {

    //}

}