using FluentValidation;

namespace CleanArchitecture.Application.PermissionsFeatures.AddPermission
{
    public sealed class AddPermissionCommandValidator : AbstractValidator<AddPermissionCommand>
    {
        public AddPermissionCommandValidator()
        {
            RuleFor(x => x.EmployeeForename).NotEmpty();
            RuleFor(x => x.EmployeeSurname).NotEmpty();
            RuleFor(x => x.PermissionTypeId).NotEmpty();
            RuleFor(x => x.PermissionDate).NotEmpty();
        }
    }
}
