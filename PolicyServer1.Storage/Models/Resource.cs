using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyServer1.Models {
    public class Resource {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Display name")]
        public String DisplayName { get; set; }

        [StringLength(200, ErrorMessage = "{0} max length must be between {1}.")]
        [Display(Name = "Type")]
        public String Type { get; set; }
        public ICollection<String> Uris { get; set; } = new HashSet<String>();
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();

        [Url(ErrorMessage = "{0} must be an url")]
        public String IconUri { get; set; }
         
        //public Boolean UserManageAccessEnabled { get; set; }
        //public Dictionary<String, Object> Attributes { get; set; }

    }
}
