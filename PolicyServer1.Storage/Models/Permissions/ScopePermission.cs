using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace PolicyServer1.Models {
    public class ScopePermission : Permission {

        [Display(Name = "Resource")]
        [Required]
        public Resource Resource { get; set; }
        public ICollection<Scope> Scopes { get; set; } = new HashSet<Scope>();

    }
}
