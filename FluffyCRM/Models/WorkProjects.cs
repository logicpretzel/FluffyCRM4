using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class WorkProject
    {   [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [DisplayName("Product/Solution")]
        public int? ProdId { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [DisplayName("Project Type")]
        public int? ProjType { get; set; }

        [StringLength(8000)]
        public string Description { get; set; }

        [DisplayName("Created By")]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Completed")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CompletedDate { get; set; }

        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? LocalTime { get; set; }

    }
}