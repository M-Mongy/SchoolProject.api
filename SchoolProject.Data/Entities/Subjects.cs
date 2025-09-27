using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Subjects: GeneralLocalizableEntity
    {
        public Subjects()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            // FIX: Corrected typo "DepartmetsSubjects" to "DepartmentSubjects"
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }
        [StringLength(500)]
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }
        public int? Period { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        [InverseProperty("Subject")]
        // FIX: Corrected typo in property name
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}
