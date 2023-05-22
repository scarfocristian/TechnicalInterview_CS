using CleanArchitecture.Application.DTOs;
using MediatR;

namespace CleanArchitecture.Application.PermissionsFeatures.GetAllPermissions
{
    public class GetAllPermissionsQuery : IRequest<List<PermissionsDto>>
    {
    }
}
