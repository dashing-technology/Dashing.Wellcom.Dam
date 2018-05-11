using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashing.Wellcom.Dam.Models
{
    public class WellcomProduct
    {
        public string Descr { get; set; }
        public string Size { get; set; }
        public string ThemeTags { get; set; }
        public string CanopyPublic { get; set; }
        public string LegalCode { get; set; }
        public HeroMedia HeroMedia { get; set; }
        public string Rrp { get; set; }
        public string LegalCodeName { get; set; }
        public string Story { get; set; }
        public string EolDate { get; set; }
        public string ExposureDate { get; set; }
        public string Style { get; set; }
        public string Gtin { get; set; }
        public string Uuid { get; set; }
        public string Dimension { get; set; }
        public string Weight { get; set; }
        public string Volume { get; set; }
        public Status Status { get; set; }
        public string Colour { get; set; }
        public string Code { get; set; }
    }

    public class Status
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class HeroMedia
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string OriginalFilename { get; set; }
        public string Uuid { get; set; }
        public string Thumb { get; set; }
        public string Full { get; set; }
        public string Original { get; set; }
    }
}
