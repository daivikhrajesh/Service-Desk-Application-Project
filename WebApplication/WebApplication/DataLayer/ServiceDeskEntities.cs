using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.DataLayer
{
    public class ServiceDeskEntities:DbContext
    {
        public ServiceDeskEntities() : base("name=dbconnection")
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Assign_Ticket> Assign_Tickets { get; set; }
        public DbSet<Role> Roles { get; set; }



    }
}