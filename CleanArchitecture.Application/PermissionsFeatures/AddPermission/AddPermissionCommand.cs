using CleanArchitecture.Application.DTOs;
using MediatR;

namespace CleanArchitecture.Application.PermissionsFeatures.AddPermission
{
    public class AddPermissionCommand : IRequest<PermissionsDto>
    {
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
