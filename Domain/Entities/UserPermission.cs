using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentApp.Security;

namespace StudentApp.Domain.Entities
{
    public class UserPermission
    {
        
        public int UserId { get; set; }
        public Permission PermissionId { get; set; }
    }
}
