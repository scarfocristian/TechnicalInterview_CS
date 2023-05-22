using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.UnitTest.Common
{
    public class PermissionsDataFake
    {
        public List<Permissions> Permissions { get; set; }

        public PermissionsDataFake()
        {
            this.Permissions = this.GetPermissionsTest();
        }

        private List<Permissions> GetPermissionsTest()
        {
            List<Permissions> list = new List<Permissions>();
            list.Add(new Permissions
            {
                Id = 1,
                EmployeeForename = "A",
                EmployeeSurname = "AS",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023, 5, 18)
            });

            list.Add(new Permissions
            {
                Id = 2,
                EmployeeForename = "B",
                EmployeeSurname = "BS",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023, 5, 18)
            });

            list.Add(new Permissions
            {
                Id = 3,
                EmployeeForename = "C",
                EmployeeSurname = "CS",
                PermissionTypeId = 1,
                PermissionDate = new DateTime(2023, 5, 18)
            });

            return list;
        }
    }
}
