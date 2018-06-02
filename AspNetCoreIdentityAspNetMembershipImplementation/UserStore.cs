namespace AspNetCoreIdentityAspNetMembershipImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using AspNetCoreIdentityAspNetMembershipImplementation.Model;

    public class UserStore : IUserStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>
    {
        /// <param name="utility">Can be null.</param>
        public UserStore(MembershipContext db, string applicationName, IUtility utility)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.applicationName = applicationName ?? throw new ArgumentNullException(nameof(applicationName));
            this.utility = utility ?? new Utility();
        }
        private readonly MembershipContext db;
        private readonly string applicationName;
        private readonly IUtility utility;

        public IQueryable<ApplicationUser> Users {
            get {
                var response = db.AspnetUsers.OrderBy(c => c.UserName).Where(c => c.Application.ApplicationName == applicationName)
                    .Select(c => new ApplicationUser
                    {
                        UserName = c.UserName,
                        Email = c.AspnetMembership.Email,
                        PasswordFormat = c.AspnetMembership.PasswordFormat,
                        PasswordHash = c.AspnetMembership.Password,
                        PasswordQuestion = c.AspnetMembership.PasswordQuestion,
                        PasswordSalt = c.AspnetMembership.PasswordSalt,
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
                return response;
            }
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            Guid applicationId;
            var tnx = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
            var application = await db.AspnetApplications.SingleOrDefaultAsync(c => c.ApplicationName == applicationName, cancellationToken);
            if (application == null)
            {
                applicationId = Guid.NewGuid();
                var newApplication = new AspnetApplications
                {
                    ApplicationId = applicationId,
                    ApplicationName = applicationName,
                    LoweredApplicationName = applicationName?.ToLowerInvariant(),
                };
                await db.AspnetApplications.AddAsync(newApplication, cancellationToken);
            }
            else
            {
                applicationId = application.ApplicationId;
            }
            Guid userId = Guid.NewGuid();
            var newUser = new AspnetUsers
            {
                ApplicationId = applicationId,
                UserId = userId,
                UserName = user.UserName,
                LoweredUserName = user.NormalizedUserName,
                IsAnonymous = false,
                LastActivityDate = user.LastActivityDate,
            };
            await db.AspnetUsers.AddAsync(newUser, cancellationToken);

            var newMembership = new AspnetMembership
            {
                ApplicationId = applicationId,
                UserId = userId,
                Password = user.PasswordHash,
                PasswordSalt = utility.GenerateSalt(),
                Email = user.Email,
                LoweredEmail = user.Email?.ToLowerInvariant(),
                PasswordQuestion = user.PasswordQuestion,
                IsApproved = user.IsApproved,
                IsLockedOut = user.IsLockedOut,
                CreateDate = user.CreationDate,
                LastLoginDate = user.LastLoginDate,
                LastPasswordChangedDate = user.LastPasswordChangedDate,
                LastLockoutDate = user.LastLockoutDate,
            };
            await db.AspnetMembership.AddAsync(newMembership, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            tnx.Commit();
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            //nop
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var response = await Users.SingleOrDefaultAsync(c => c.UserId == Guid.Parse(userId), cancellationToken);
            return response;
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var response = await Users.SingleOrDefaultAsync(c => c.UserName == normalizedUserName, cancellationToken);
            return response;
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.NormalizedUserName == normalizedName);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.UserName = userName);
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #region IUserRoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var response = await db.AspnetUsersInRoles
                .Where(c => c.UserId == user.UserId)
                .Select(c => c.Role.RoleName)
                .ToListAsync();
            return response;
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
