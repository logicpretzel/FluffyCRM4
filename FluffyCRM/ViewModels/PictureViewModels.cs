using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FluffyCRM.ViewModels
{
    public class ImagesView
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Contents { get; set; }
        public byte[] ImageData { get; set; }
        public string SourceType { get; set; }
        public int SourceID { get; set; }

    }

    public class PictureAddView
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Contents { get; set; }
        public byte[] ImageData { get; set; }
        public string SourceType { get; set; }
        public int SourceID { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase File { get; set; }
        public int? Size { get; set; }
        public string FileName { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string ContentType { get; set; }


    }
}
