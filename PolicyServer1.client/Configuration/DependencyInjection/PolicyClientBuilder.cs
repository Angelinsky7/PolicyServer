using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection {
    public class PolicyClientBuilder : IPolicyClientBuilder {
        public PolicyClientBuilder(IServiceCollection services) {
            Services = services ?? throw new ArgumentNullException(nameof(Services));
        }

        public IServiceCollection Services { get; }
    }
}
