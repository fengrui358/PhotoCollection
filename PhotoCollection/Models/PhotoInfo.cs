using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhotoCollection.Models
{
    [Table("PhotoInfos")]
    public class PhotoInfo
    {
        public virtual string Url { get; set; }
        public virtual string Type { get; set; }
        public virtual long Size { get; set; }
        public virtual string Exif { get; set; }
    }
}