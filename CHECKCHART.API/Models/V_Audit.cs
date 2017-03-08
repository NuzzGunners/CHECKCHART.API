using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Models
{
    public class V_Audit
    {
        [Key]
        public Guid Id { get; set; }
        public string An { get; set; }
        public string Doctor { get; set; }
        public string Doctorname { get; set; }
        public string Doctormaster { get; set; }
        public string Doctormastername { get; set; }
        public string Doctorconsult { get; set; }
        public string Doctorconsultname { get; set; }
        public int Category { get; set; }
        public string Categoryname { get; set; }
        public decimal? Rwbefore { get; set; }
        public decimal? Rwafter { get; set; }
        public string Coder { get; set; }
        public string Codername { get; set; }
        public string Ward { get; set; }
        public string Wardname { get; set; }
        public decimal? Los { get; set; }
        public string Nurse { get; set; }
        public string Nursename { get; set; }
    }
}