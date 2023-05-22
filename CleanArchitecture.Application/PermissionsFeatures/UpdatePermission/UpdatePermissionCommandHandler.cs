using AutoMapper;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Services;
using MediatR;

namespace CleanArchitecture.Application.PermissionsFeatures.UpdatePermission
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, PermissionsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IKafkaService _kafkaService;
        private readonly IElasticSearchService _elasticsearchService;

        public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork, 
            IPermissionRepository permissionRepository, 
            IMapper mapper,
            IKafkaService kafkaService,
            IElasticSearchService elasticsearchService)
        {
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _kafkaService = kafkaService;
            _elasticsearchService = elasticsearchService;
        }

        public async Task<PermissionsDto> Handle(UpdatePermissionCommand request, 
            CancellationToken cancellationToken)
        {
            var permission = new Permissions
            {
                Id = request.Id,
                EmployeeForename = request.EmployeeForename,
                EmployeeSurname = request.EmployeeSurname,
                PermissionTypeId = request.PermissionTypeId,
                PermissionDate = request.PermissionDate
            };

            _permissionRepository.Update(permission);
            await _unitOfWork.SaveAsync(cancellationToken);

            await _kafkaService.ProduceMessageAsync("operation_modify");
            await _elasticsearchService.InsertDocument(permission);

            return _mapper.Map<PermissionsDto>(permission);
        }
    }
}
