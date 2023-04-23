using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Avance.NET.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public int TeacherID { get; set; }


        //Navigation properties för att skapa relationer mellan tabeller i databasen.
        public virtual Teacher Teacher { get; set; }
    }
}
