using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection {
    public interface IPolicyClientBuilder {
        IServiceCollection Services { get; }
    }
}
