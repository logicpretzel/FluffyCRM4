using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public string FileType  { get; set; }
        public long FileSize { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public byte[] File { get; set; }

    }

    public class FileUploadModel
    {
        [DisplayName("Select File For Upload")]
        public HttpPostedFileBase File { get; set; }
        [StringLength(255)]
        public string Title { get; set; }

    }

    /* 
     * http://www.itorian.com/2014/03/single-file-upload-to-multiple-file.html 
     * 
     * 
    */
}