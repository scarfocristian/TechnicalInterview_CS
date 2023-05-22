using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public class PermissionRepository : BaseRepository<Permissions>, IPermissionRepository
{
    public PermissionRepository(DataContext context) : base(context)
    {
    }
}