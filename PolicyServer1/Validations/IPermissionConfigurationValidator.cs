using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace PolicyServer1.Validations {
    public interface IPermissionConfigurationValidator {

        Task ValidateAsync(ConfigurationValidationContext<Permission> context);

    }
}
