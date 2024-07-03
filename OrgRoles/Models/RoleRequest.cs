namespace OrgRoles.Models
{
    public class RoleRequest
    {
        //public RoleRequest() { }
        public string Name {  get; set; }
        public string Description { get; set; }
        public Guid? ParentID { get; set; }
    }
    public record RoleDto(string Name);
}
