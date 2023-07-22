using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IntroAPI.EF.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Dob { get; set; }

        [ForeignKey("Catagories")]
        public int CatId { get; set; }

        [JsonIgnore]
        public virtual Catagory Catagories { get; set; }
    }
}