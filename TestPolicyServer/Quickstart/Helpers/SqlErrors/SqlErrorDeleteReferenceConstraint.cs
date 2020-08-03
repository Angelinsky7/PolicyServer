using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TestPolicyServer.Quickstart {
    public class SqlErrorDeleteReferenceConstraint : ISqlError {

        private static readonly Regex _testConstraintRegex = new Regex("The DELETE statement conflicted with the REFERENCE constraint \"([a-zA-Z0-9_]+)\"", RegexOptions.Compiled);

        public Int32 SqlServerErrorNumber => 547;

        public String Formatter(SqlException ex, IReadOnlyList<EntityEntry> entitiesNotSaved) {
            String message = ex.Errors[0].Message;
            MatchCollection matches = _testConstraintRegex.Matches(message);

            if (matches.Count == 0) { return null; }

            //currently the entitiesNotSaved is empty for unique constraints - see https://github.com/aspnet/EntityFrameworkCore/issues/7829
            var entityDisplayName = entitiesNotSaved.Count == 1
                ? entitiesNotSaved.Single().Entity.GetType().Name
                : matches[0].Groups[1].Value;

            var returnError = $"Cannot delete this entity because another entity is referencing it. Please unlink all entities using this one before deleting it.";

            //Int32 openingBadValue = message.IndexOf("(");
            //if (openingBadValue > 0) {
            //    String dupPart = message.Substring(openingBadValue + 1,
            //        message.Length - openingBadValue - 3);
            //    returnError += $" Duplicate value was '{dupPart}'.";
            //}

            return returnError;
        }

    }
}
