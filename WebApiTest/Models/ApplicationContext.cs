﻿using Microsoft.EntityFrameworkCore;

namespace WebApiTest.Models
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) 
        {      
        }
        public DbSet<Student> Students { get; set; }
    }
}
