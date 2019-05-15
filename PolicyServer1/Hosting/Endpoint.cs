using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PolicyServer1.Hosting {
    public class Endpoint {

        public String Name { get; set; }
        public PathString Path { get; set; }
        public Type Handler { get; set; }

        public Endpoint(String name, String path, Type handlerType) {
            Name = name;
            Path = path;
            Handler = handlerType;
        }

    }
}
