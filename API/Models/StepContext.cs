using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class StepContext : DbContext
    {
        public StepContext(DbContextOptions<StepContext> stepOptions) : base(stepOptions)
    {
    }

    public DbSet<Step> Step { get; set; }
}
}
