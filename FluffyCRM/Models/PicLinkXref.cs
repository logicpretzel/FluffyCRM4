using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class PicLinkXref
    {
        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        public string SourceType { get; set; }
        public int? SourceID { get; set; }
        public int? PictureID { get; set; }
        [DisplayName("Created")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDate { get; set; }

    }
}