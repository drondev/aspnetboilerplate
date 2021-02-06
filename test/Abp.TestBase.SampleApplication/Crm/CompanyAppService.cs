using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;

namespace Abp.TestBase.SampleApplication.Crm
{
    public class CompanyAppService : CrudAppService<Company, CompanyDto, Guid>
    {
        public CompanyAppService(IRepository<Company, Guid> repository) : base(repository)
        {
            GetPermissionName = "GetCompanyPermission";
            GetAllPermissionName = "GetAllCompaniesPermission";
            CreatePermissionName = "CreateCompanyPermission";
            UpdatePermissionName = "UpdateCompanyPermission";
            DeletePermissionName = "DeleteCompanyPermission";
        }
    }
}
