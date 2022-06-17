using System.ComponentModel.DataAnnotations;

namespace SmartASSWeb.Dal.Entities
{
    
    public class CompanyType : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}