﻿using EFCore.Toolkit;
using EFCore.Toolkit.Contracts;
using ToolkitSample.Model;

namespace ToolkitSample.DataAccess.Contracts.Repository
{
    public interface IEmployeeReadOnlyRepository : IReadOnlyRepository<Employee>
    {
    }
}