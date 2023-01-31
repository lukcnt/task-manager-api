using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using task_manager_api.Models;

namespace task_manager_api.Context
{
    public class OrganizerContext : DbContext
    {
        public OrganizerContext(DbContextOptions<OrganizerContext> options) : base(options)
        {

        }

        public DbSet<TaskAt> Tasks { get; set; }
    }
}