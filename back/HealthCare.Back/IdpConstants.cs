namespace HealthCare.Back;

static class IdpConstants
{
    //TODO: Store in application secrets

    public const string Authority = "https://localhost:5001";
    public const string ClientId = "healthcareapp";
    public const string ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
    public const string HealthCareApiScope = "healthcareapi";
    public const string ResponseType = "code";
    public const string OfflineAccess = "offline_access";

}
