using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection {
    public class PolicyServerBuilder : IPolicyServerBuilder {

        public PolicyServerBuilder(IServiceCollection services) {
            Services = services ?? throw new ArgumentNullException(nameof(Services));
        }

        public IServiceCollection Services { get; }
    }
}
