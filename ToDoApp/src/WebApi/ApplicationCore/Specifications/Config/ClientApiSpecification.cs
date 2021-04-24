using ApplicationCore.Entities.Config;

namespace ApplicationCore.Specifications.Config
{
    public class ClientApiSpecification : SpecificationQuery<ClientApi>
    {
        public ClientApiSpecification()
        {
            ApplyOrderBy(c => c.ClientId);
        }

        public ClientApiSpecification(bool isActive)
            : base(c => c.IsActive == isActive)
        {
            ApplyOrderBy(c => c.ClientId);
        }

        public ClientApiSpecification(string clientId, string clientSecret)
            : base(c => c.ClientId == clientId && c.ClientSecret == clientSecret)
        {
            ApplyOrderBy(c => c.ClientId);
        }
    }
}
