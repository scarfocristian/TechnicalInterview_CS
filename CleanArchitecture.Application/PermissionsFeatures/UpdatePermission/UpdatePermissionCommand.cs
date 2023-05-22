using CleanArchitecture.Application.DTOs;
using MediatR;

namespace CleanArchitecture.Application.PermissionsFeatures.UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<PermissionsDto>
    { 
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
