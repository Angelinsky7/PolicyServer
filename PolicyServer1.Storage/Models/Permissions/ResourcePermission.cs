using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Validations;

namespace PolicyServer1.Models {

    [ResourcePermissionValidation]
    public class ResourcePermission : Permission {

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Display(Name = "Resouce type")]
        public String ResouceType { get; set; }

        [Display(Name = "Resource")]
        public Resource Resource { get; set; }

        //public override Task<Object> EvaluateAsync(ClaimsPrincipal user) {
        //    return Task.FromResult((Object)Resource);
        //}

    }
}
