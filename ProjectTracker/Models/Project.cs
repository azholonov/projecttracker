using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        public Company CustomerCompany { get; set; }
        public Company ExecutorCompany { get; set; }
        public Employee Manager { get; set; }
        public virtual ICollection<Employee> Employees {get;set;}
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Priority Priority { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Project ()
        {
            Employees = new List<Employee>();
        }
    }
}