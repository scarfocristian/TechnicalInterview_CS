using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class PermissionTypes : BaseEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
