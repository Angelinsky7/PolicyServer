using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.ResponseHandling {
    public interface IDiscoveryResponseGenerator {
        Task<Dictionary<String, Object>> CreateDiscoveryDocumentAsync(String baseUrl, String issuerUri);
    }
}
