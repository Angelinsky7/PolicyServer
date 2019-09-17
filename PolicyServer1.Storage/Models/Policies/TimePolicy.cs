using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServer1.Models {
    public class TimePolicy : Policy {

        public Nullable<DateTime> NotBefore { get; set; }
        public Nullable<DateTime> NotOnOrAfter { get; set; }

        public TimePolicyRange DayOfMonth { get; set; }
        public TimePolicyRange Month { get; set; }
        public TimePolicyRange Year { get; set; }
        public TimePolicyRange Hour { get; set; }
        public TimePolicyRange Minute { get; set; }

        public override Task<Boolean> EvaluateAsync(IEvaluatorRequest request) {
            DateTime dateTime = DateTime.UtcNow;
            Boolean result = true;

            if (NotBefore != null && dateTime < NotBefore.Value) { result = false; }
            if (result != false && (NotOnOrAfter != null && dateTime >= NotOnOrAfter.Value)) { result = false; }
            if (result != false && (DayOfMonth != null && (dateTime.Day < DayOfMonth.From || dateTime.Day >= DayOfMonth.To))) { result = false; }
            if (result != false && (Month != null && (dateTime.Month < Month.From || dateTime.Month >= Month.To))) { result = false; }
            if (result != false && (Year != null && (dateTime.Year < Year.From || dateTime.Year >= Year.To))) { result = false; }
            if (result != false && (Hour != null && (dateTime.Hour < Hour.From || dateTime.Hour >= Hour.To))) { result = false; }
            if (result != false && (Minute != null && (dateTime.Minute < Minute.From || dateTime.Minute >= Minute.To))) { result = false; }

            request.Result = result;
            return base.EvaluateAsync(request);
        }
    }

    public class TimePolicyRange {
        public Int32 From { get; set; }
        public Int32 To { get; set; }
    }

}
