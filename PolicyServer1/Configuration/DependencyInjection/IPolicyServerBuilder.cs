using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection {
    public interface IPolicyServerBuilder {
        IServiceCollection Services { get; }
    }
}
