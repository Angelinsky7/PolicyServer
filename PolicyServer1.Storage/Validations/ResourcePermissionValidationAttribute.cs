using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1.Validations {

    [AttributeUsage(AttributeTargets.Class)]
    public class ResourcePermissionValidationAttribute : ValidationAttribute {

        protected override ValidationResult IsValid(Object value, ValidationContext validationContext) {
            if (value is ResourcePermission obj) {
                if (String.IsNullOrEmpty(obj.ResouceType) && obj.Resource == null) {
                    return new ValidationResult("Resource type or resource must at leat be set.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
