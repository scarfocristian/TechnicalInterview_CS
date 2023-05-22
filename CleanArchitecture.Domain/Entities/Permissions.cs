using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities
{
    public class Permissions : BaseEntity
    {
        public int Id { get; set; }

        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }

        [ForeignKey("PermissionTypes")]
        public int PermissionTypeId { get; set; }

        public DateTime PermissionDate { get; set; }

        public virtual PermissionTypes PermissionTypes { get; set; }
    }
}
