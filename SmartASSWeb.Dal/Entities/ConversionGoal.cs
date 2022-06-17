using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SmartASSWeb.Dal.Entities
{
    
    public class ConversionGoal : EntityBase
    {

        [Key]
        public int Id { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("period")]
        public string Period { get; set; }
        [JsonProperty("conversionValue")]
        public string ConversionValue { get; set; }

        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}
