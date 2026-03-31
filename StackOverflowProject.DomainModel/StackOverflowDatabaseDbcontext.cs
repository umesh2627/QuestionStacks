using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StackOverflowProject.DomainModel
{
    public class StackOverflowDatabaseDbcontext:DbContext
        
    {
        public DbSet<Category> categories{get;set; }

        public DbSet<User> users { get;set; }

        public DbSet<Question> Questions { get;set; }

        public DbSet<Answer> Answers { get;set; }

        public DbSet<Vote> Votes { get;set; }
    }
}
