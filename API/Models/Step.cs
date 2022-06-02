using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Step
    {
        public Step() { }
        public long Id { get; set; }
        public int StepId { get; set; }
        public string Instruction { get; set; }
    }
}
