namespace AspNetCoreIdentityAspNetMembershipImplementation
{
    public enum MembershipPasswordFormat
    {
        // The password is stored in cleartext in the database.
        Clear = 0,
        // The password is cryptographically hashed and stored in the database.
        Hashed = 1,
        // The password is encrypted using reversible encryption (using <machineKey>) and stored in the database.
        Encrypted = 2,
    }

    public enum MembershipPasswordCompatibilityMode
    {
        Framework20 = 0,
        Framework40 = 1,
    }
}