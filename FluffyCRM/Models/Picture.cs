using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FluffyCRM.Models
{
    /// <summary>
    /// Author: Dar Dunham
    /// Date: 2/15/16
    /// Model
    /// </summary>
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        
        public int? Size { get; set; }
        public string FileName { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string ContentType { get; set; }

        [AllowHtml]
        public string Contents { get; set; }

 

        public byte[] ImageData { get; set; }

    }

}
