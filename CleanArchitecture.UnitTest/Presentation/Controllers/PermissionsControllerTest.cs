using CleanArchitecture.Application.PermissionsFeatures.AddPermission;
using CleanArchitecture.Application.PermissionsFeatures.UpdatePermission;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.UnitTest.Common;
using CleanArchitecture.WebAPI.Controllers;
using FakeItEasy;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.UnitTest.Presentation.Controllers
{
    public class PermissionsControllerTest
    {
        private PermissionsDataFake _permissionsDataFake;
        private ILogger<PermissionsController> _logger;
        private IMediator _mediator;
        private PermissionsController _permissionsController;

        public PermissionsControllerTest()
        {
            _permissionsDataFake = new PermissionsDataFake();
            _logger = A.Fake<ILogger<PermissionsController>>();
            _mediator = A.Fake<IMediator>();
            _permissionsController = new PermissionsController(_mediator, _logger);
        }

        [Fact]
        public async Task GetAllPermissions_ShouldReturnListPermissions()
        {
            var permissionRepository = new Mock<IPermissionRepository>();

            permissionRepository.Setup(_ => _.GetAll(new CancellationToken()))
                .ReturnsAsync(_permissionsDataFake.Permissions);

            var result = await _permissionsController.GetAll(new CancellationToken());

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Add_WithoutEmployeeForename_ShouldError()
        {
            var validation = new AddPermissionCommandValidator();

            AddPermissionCommand permission = new AddPermissionCommand()
            {
                EmployeeSurname = "B",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023,5,18)
            };

            var result = validation.TestValidate(permission);

            result.ShouldHaveValidationErrorFor(c => c.EmployeeForename);
        }

        [Fact]
        public async Task AddPermission_ShouldPermission()
        {
            AddPermissionCommand permission = new AddPermissionCommand()
            {
                EmployeeForename = "A",
                EmployeeSurname = "B",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023, 5, 18)
            };

            var createResponse = await _permissionsController.Create(permission, new CancellationToken());

            createResponse.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Update_WithoutId_ShouldError()
        {
            var validation = new UpdatePermissionCommandValidator();

            UpdatePermissionCommand permission = new UpdatePermissionCommand()
            {
                EmployeeForename = "A",
                EmployeeSurname = "B",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023, 5, 18)
            };

            var result = validation.TestValidate(permission);

            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public async Task UpdatePermission_ShouldPermission()
        {
            UpdatePermissionCommand permission = new UpdatePermissionCommand()
            {
                Id = 1,
                EmployeeForename = "A",
                EmployeeSurname = "B",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023, 5, 18)
            };

            var updateResponse = await _permissionsController.Update(1, permission, new CancellationToken());

            updateResponse.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdatePermission_WithDifferentId_ShouldError()
        {
            UpdatePermissionCommand permission = new UpdatePermissionCommand()
            {
                Id = 2,
                EmployeeForename = "A",
                EmployeeSurname = "B",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023, 5, 18)
            };

            var updateResponse = await _permissionsController.Update(1, permission, new CancellationToken());

            updateResponse.Result.Should().BeOfType<BadRequestResult>();
        }
    }
}
