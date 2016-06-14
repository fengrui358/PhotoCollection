using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Dapper.FastCrud;

namespace PhotoCollection.Models
{
    [Table("PhotoInfos")]
    public class PhotoInfo
    {
        [Key]
        public virtual string Md5 { get; set; }

        [Key]
        [DatabaseGeneratedDefaultValue]
        public virtual long OrderNumber { get; set; }

        public virtual string Url { get; set; }
        public virtual string Type { get; set; }
        public virtual long Size { get; set; }
        public virtual string Exif { get; set; }
    }
}