using AutoMapper;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Services;
using MediatR;

namespace CleanArchitecture.Application.PermissionsFeatures.GetAllPermissions
{
    public sealed class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, List<PermissionsDto>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IKafkaService _kafkaService;
        private readonly IElasticSearchService _elasticsearchService;

        public GetAllPermissionsQueryHandler(IPermissionRepository permissionRepository, 
            IMapper mapper,
            IKafkaService kafkaService,
            IElasticSearchService elasticsearchService)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _kafkaService = kafkaService;
            _elasticsearchService = elasticsearchService;
        }

        public async Task<List<PermissionsDto>> Handle(GetAllPermissionsQuery request, 
            CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAll(cancellationToken);

            await _kafkaService.ProduceMessageAsync("operation_get");

            foreach (var permission in permissions)
            {
                await _elasticsearchService.InsertDocument(permission);
            }

            return _mapper.Map<List<PermissionsDto>>(permissions);
        }
    }
}
