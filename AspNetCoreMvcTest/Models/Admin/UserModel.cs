namespace AspNetCoreMvcTest.Models.Admin
{
    using System;
    using System.Collections.Generic;

    public class UserModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}