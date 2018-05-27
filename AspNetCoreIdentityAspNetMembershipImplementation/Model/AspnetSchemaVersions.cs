using System;
using System.Collections.Generic;

namespace AspNetCoreIdentityAspNetMembershipImplementation.Model
{
    public partial class AspnetSchemaVersions
    {
        public string Feature { get; set; }
        public string CompatibleSchemaVersion { get; set; }
        public bool IsCurrentVersion { get; set; }
    }
}
