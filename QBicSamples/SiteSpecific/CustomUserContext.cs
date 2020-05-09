using NHibernate.Criterion;
using QBic.Core.Data;
using QBicSamples.Models;
using System.Threading.Tasks;
using WebsiteTemplate.Data;

namespace QBicSamples.SiteSpecific
{
    public class CustomUserContext : UserContextBase<CustomUser>
    {
        public CustomUserContext(DataStore dataStore)
            : base(dataStore)
        {
        }

        // We need to override this function, because in our CustomUser class, UserName points to Email and won't be a field in the DataBase.
        // So this method fixes that and tells NHibernate to use the email when searching for a user by their name.
        public override Task<CustomUser> FindUserByNameAsync(string name)
        {
            CustomUser result;

            using (var session = DataStore.OpenSession())
            {
                result = session.CreateCriteria<CustomUser>()
                                .Add(Restrictions.Eq("Email", name).IgnoreCase())
                                .UniqueResult<CustomUser>();
            }
            return Task.FromResult(result);
        }
    }
}