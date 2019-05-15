using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using PolicyServer1.Extensions;

namespace PolicyServer1.Models {
    public static class HashExtensions {

        public static String Sha256(this String input) {
            if (input.IsMissing()) return String.Empty;

            using (SHA256 sha = SHA256.Create()) {
                Byte[] bytes = Encoding.UTF8.GetBytes(input);
                Byte[] hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public static Byte[] Sha256(this Byte[] input) {
            if (input == null) {
                return null;
            }

            using (SHA256 sha = SHA256.Create()) {
                return sha.ComputeHash(input);
            }
        }

        public static String Sha512(this String input) {
            if (input.IsMissing()) return String.Empty;

            using (SHA512 sha = SHA512.Create()) {
                Byte[] bytes = Encoding.UTF8.GetBytes(input);
                Byte[] hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

    }
}
