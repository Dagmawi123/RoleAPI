using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrgRoles.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [ForeignKey("ParentId")]
        public Guid? ParentId { get; set; }

        public Role? Parent { get; set; }

    }
}
