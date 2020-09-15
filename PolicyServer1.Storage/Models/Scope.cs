using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyServer1.Models {
    public class Scope {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public String Name { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Display name")]
        public String DisplayName { get; set; }
        
        [Url]
        public String IconUri { get; set; }

    }
}