using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.Models;

namespace PolicyServer1.Validations {
    public interface IPolicyConfigurationValidator {
        Task ValidateAsync(ConfigurationValidationContext<Policy> context);
    }
}
