using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Created On:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateDt { get; set; }

        [Display(Name = "Project:")]
        [DefaultValue(0)]
        public int? ProjectId { get; set; }

        [ScaffoldColumn(false)]
        [DefaultValue(0)]
        public int? EntityId { get; set; }

        [StringLength(255)]
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        [Display(Name = "Detail:")]
        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }

        [Display(Name = "Sprint:")]
        public int? SprintId { get; set; }

        [ScaffoldColumn(false)]
        [DefaultValue(false)]
        public bool? HasChildren { get; set; }

        [ScaffoldColumn(false)]
        [DefaultValue(false)]
        public bool? HasPrereqs { get; set; }

        [Display(Name = "Exp. Start Dt:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? ExpStartDt { get; set; }

        [Display(Name = "Exp. Stop Dt:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? ExpStopDt { get; set; }

        [Display(Name = "Started On:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? StartDt { get; set; }

        [Display(Name = "Completed Date:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? CompletedDt { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Budgeted Hours:")]
        public decimal? BudgetHours { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Actual Hours:")]
        public decimal? ActualHours { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? ModifyDt { get; set; }

        [ScaffoldColumn(false)]
        [DefaultValue(0)]
        public int? ModifyBy { get; set; }

        [DefaultValue(0)]
        [ScaffoldColumn(false)]
        public int? ParentId { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Category:")]
        public int? CatId { get; set; }

        [DefaultValue(0)]
        [ScaffoldColumn(false)]
        public int? TaskId { get; set; }


        [DefaultValue(0)]
        public int? StatusId { get; set; }

        [DefaultValue(false)]
        [ScaffoldColumn(false)]
        public int? DeleteId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DeletedDt { get; set; }

        [DefaultValue(0)]
        [ScaffoldColumn(false)]
        public bool? ArchivedInd { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ArchivedDt { get; set; }

    }


    public class CardAddViewModel
    {

        [Display(Name = "Project:")]
        public int? ProjectId { get; set; }

        [Display(Name = "EntityId:")]
        public int? EntityId { get; set; }

        [StringLength(255)]
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        [Display(Name = "Detail:")]
        public string Detail { get; set; }

        [Display(Name = "Approx. Hours:")]
        public decimal? BudgetHours { get; set; }

        [Display(Name = "Sprint:")]
        public int? SprintId { get; set; }

        [Display(Name = "Exp. Start Dt:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpStartDt { get; set; }

        [Display(Name = "Exp. Stop Dt:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpStopDt { get; set; }

        [Display(Name = "Category:")]
        public int? CatId { get; set; }


        [Display(Name = "Status:")]
        public int? StatusId { get; set; }

    }

    public class CardViewModel
    {


        [StringLength(255)]
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        [Display(Name = "Detail:")]
        public string Detail { get; set; }

        [Display(Name = "Approx. Hours:")]
        public decimal? BudgetHours { get; set; }

        [Display(Name = "Sprint:")]
        public int? SprintId { get; set; }

        [Display(Name = "Category:")]
        public int? CatId { get; set; }

        [Display(Name = "Status:")]
        public int? StatusId { get; set; }

    }



}