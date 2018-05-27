namespace AspNetCoreIdentityAspNetMembershipImplementation
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using AspNetCoreIdentityAspNetMembershipImplementation.Model;

    public class UserStore : IUserStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    {
        public UserStore(MembershipContext db, string applicationName)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.applicationName = applicationName ?? throw new ArgumentNullException(nameof(applicationName));
        }
        private readonly MembershipContext db;
        public readonly string applicationName;

        public IQueryable<ApplicationUser> Users {
            get {
                return db.AspnetUsers.OrderBy(c => c.UserName).Where(c => c.Application.ApplicationName == applicationName)
                    .Select(c => new ApplicationUser
                    {
                        UserName = c.UserName,
                        Email = c.AspnetMembership.Email,
                        PasswordQuestion = c.AspnetMembership.PasswordQuestion,
                        Comment = c.AspnetMembership.Comment,
                        IsApproved = c.AspnetMembership.IsApproved,
                        CreationDate = c.AspnetMembership.CreateDate,
                        LastLoginDate = c.AspnetMembership.LastLoginDate,
                        LastActivityDate = c.LastActivityDate,
                        LastPasswordChangedDate = c.AspnetMembership.LastPasswordChangedDate,
                        UserId = c.UserId,
                        IsLockedOut = c.AspnetMembership.IsLockedOut,
                        LastLockoutDate = c.AspnetMembership.LastLockoutDate,
                    });
            }
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            //nop
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
