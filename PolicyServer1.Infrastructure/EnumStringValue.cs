using System;
using System.Reflection;

namespace PolicyServer1.Infrastructure {
    public class EnumStringValue : Attribute {
        public String Value { get; }

        public EnumStringValue(String value) {
            Value = value;
        }

    }

    public static class EnumStringHelper {
        public static String GetStringValue(Enum value) {
            String output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            EnumStringValue[] attrs = fi.GetCustomAttributes(typeof(EnumStringValue), false) as EnumStringValue[];
            if (attrs.Length > 0) {
                output = attrs[0].Value;
            }

            return output;
        }
    }

}
