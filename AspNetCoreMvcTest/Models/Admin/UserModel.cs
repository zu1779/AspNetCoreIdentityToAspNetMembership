namespace AspNetCoreMvcTest.Models.Admin
{
    using System.Collections.Generic;

    using AspNetCoreIdentityAspNetMembershipImplementation;

    public class UserModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}