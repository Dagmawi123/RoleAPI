using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrgRoles.Models
{
    public class Role
    {
        public Guid Id { get; set; }
                
        public string Name { get; set; }
 
        public string Description { get; set; }
        
        public Guid? ParentId { get; set; } = null;
       
        public bool isCandidate { get; set; } = false;

        public Role? Parent { get; set; }

    }
}
