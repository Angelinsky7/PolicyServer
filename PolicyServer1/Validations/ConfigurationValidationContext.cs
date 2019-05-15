using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Validations {
    public class ConfigurationValidationContext<T> where T : class {

        public T Item { get; }
        public Boolean IsValid { get; set; } = true;
        public String ErrorMessage { get; set; }

        public ConfigurationValidationContext(T item) {
            Item = item;
        }

        public void SetError(String mesage) {
            IsValid = false;
            ErrorMessage = mesage;
        }

    }
}
