using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {

    //TODO(demarco): Should be abstract
    public class Permission {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [StringLength(200, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [Required]
        [Display(Name = "Decision Strategy")]
        public DecisionStrategy DecisionStrategy { get; set; }

        public ICollection<Policy> Policies { get; set; } = new HashSet<Policy>();
        
    }

}