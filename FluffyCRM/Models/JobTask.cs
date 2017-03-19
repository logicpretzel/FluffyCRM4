using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class JobTask
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(8000)]
        public string Description { get; set; }

        [DefaultValue(0)]
        public int? ProdId { get; set; }

        [DefaultValue(0)]
        public int? ProjectId { get; set; }

        [DefaultValue(0)]
        public int? ParentTaskId { get; set; }

        [DefaultValue(0)]
        public int? Level { get; set; }

        [DefaultValue(0)]
        public int? ClientId { get; set; }
        
        [StringLength(128)]
        public string ContactUserId { get; set; }

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

        public DateTime? LocalTime { get; set; }


    }
}