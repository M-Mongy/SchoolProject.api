using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class DepartmentSubject
    {
        // FIX: Removed the conflicting [Key] attributes.
        public int DID { get; set; }
        public int SubID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubID")]
        // FIX: Corrected the InverseProperty to match the fixed property name in Subjects.cs
        [InverseProperty("DepartmentSubjects")]
        public virtual Subjects? Subject { get; set; }
    }
}
