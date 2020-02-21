using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public class SheduleUnit
    {
        public Module BasedOn { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
