namespace AspNetCoreIdentityAspNetMembershipImplementation
{
    using System;

    public class ApplicationUser
    {
        public virtual string PasswordHash { get; set; }
        public virtual string NormalizedUserName { get; set; }

        //
        // Summary:
        //     Gets or sets application-specific information for the membership user.
        //
        // Returns:
        //     Application-specific information for the membership user.
        public virtual string Comment { get; set; }
        //
        // Summary:
        //     Gets the date and time when the user was added to the membership data store.
        //
        // Returns:
        //     The date and time when the user was added to the membership data store.
        public virtual DateTime CreationDate { get; set; }
        //
        // Summary:
        //     Gets or sets the e-mail address for the membership user.
        //
        // Returns:
        //     The e-mail address for the membership user.
        public virtual string Email { get; set; }
        //
        // Summary:
        //     Gets or sets whether the membership user can be authenticated.
        //
        // Returns:
        //     true if the user can be authenticated; otherwise, false.
        public virtual bool IsApproved { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether the membership user is locked out and unable
        //     to be validated.
        //
        // Returns:
        //     true if the membership user is locked out and unable to be validated; otherwise,
        //     false.
        public virtual bool IsLockedOut { get; set; }
        //
        // Summary:
        //     Gets whether the user is currently online.
        //
        // Returns:
        //     true if the user is online; otherwise, false.
        //
        // Exceptions:
        //   T:System.PlatformNotSupportedException:
        //     This method is not available. This can occur if the application targets the .NET
        //     Framework 4 Client Profile. To prevent this exception, override the method, or
        //     change the application to target the full version of the .NET Framework.
        public virtual bool IsOnline { get; set; }
        //
        // Summary:
        //     Gets or sets the date and time when the membership user was last authenticated
        //     or accessed the application.
        //
        // Returns:
        //     The date and time when the membership user was last authenticated or accessed
        //     the application.
        public virtual DateTime LastActivityDate { get; set; }
        //
        // Summary:
        //     Gets the most recent date and time that the membership user was locked out.
        //
        // Returns:
        //     A System.DateTime object that represents the most recent date and time that the
        //     membership user was locked out.
        public virtual DateTime LastLockoutDate { get; set; }
        //
        // Summary:
        //     Gets or sets the date and time when the user was last authenticated.
        //
        // Returns:
        //     The date and time when the user was last authenticated.
        public virtual DateTime LastLoginDate { get; set; }
        //
        // Summary:
        //     Gets the date and time when the membership user's password was last updated.
        //
        // Returns:
        //     The date and time when the membership user's password was last updated.
        public virtual DateTime LastPasswordChangedDate { get; set; }
        //
        // Summary:
        //     Gets the password question for the membership user.
        //
        // Returns:
        //     The password question for the membership user.
        public virtual string PasswordQuestion { get; set; }
        //
        // Summary:
        //     Gets the name of the membership provider that stores and retrieves user information
        //     for the membership user.
        //
        // Returns:
        //     The name of the membership provider that stores and retrieves user information
        //     for the membership user.
        public virtual string ProviderName { get; set; }
        //
        // Summary:
        //     Gets the user identifier from the membership data source for the user.
        //
        // Returns:
        //     The user identifier from the membership data source for the user.
        public virtual Guid UserId { get; set; }
        //
        // Summary:
        //     Gets the logon name of the membership user.
        //
        // Returns:
        //     The logon name of the membership user.
        public virtual string UserName { get; set; }
    }
}
