using CleanArchitecture.Application.PermissionsFeatures.AddPermission;
using FluentValidation;

namespace CleanArchitecture.Application.PermissionsFeatures.UpdatePermission
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.EmployeeForename).NotEmpty();
            RuleFor(x => x.EmployeeSurname).NotEmpty();
            RuleFor(x => x.PermissionTypeId).NotEmpty();
            RuleFor(x => x.PermissionDate).NotEmpty();
        }
    }
}
