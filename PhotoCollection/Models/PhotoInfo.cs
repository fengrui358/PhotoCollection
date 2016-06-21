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

        public virtual string Model { get; set; }

        public virtual string GPSLatitude { get; set; }

        public virtual string GPSLongitude { get; set; }

        public virtual string BDLatitude { get; set; }

        public virtual string BDLongitude { get; set; }
        public virtual string Address { get; set; }
        public virtual string Country { get; set; }
        public virtual string Province { get; set; }
        public virtual string City { get; set; }
        public virtual string District { get; set; }
        public virtual string Street { get; set; }

        public virtual string Exif { get; set; }

        [DatabaseGeneratedDefaultValue]
        public virtual DateTime CreateTime { get; set; }
    }
}