using System.Data;

namespace Domain.Entities
{
    public class RolePermission : Entity
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public RolePermission(IDataRecord row) : base(row)
        {
            RoleId = int.Parse(row["role_id"].ToString());
            PermissionId = int.Parse(row["permission_id"].ToString());
        }
    }
}