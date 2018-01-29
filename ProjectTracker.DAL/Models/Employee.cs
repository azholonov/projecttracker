using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public Employee()
        {
            Projects = new List<Project>();
        }
    }
}