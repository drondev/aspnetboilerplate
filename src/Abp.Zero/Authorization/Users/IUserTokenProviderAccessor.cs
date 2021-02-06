using System;
using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Users
{
    public interface IUserTokenProviderAccessor
    {
        IUserTokenProvider<TUser, Guid> GetUserTokenProviderOrNull<TUser>() 
            where TUser : AbpUser<TUser>;
    }
}