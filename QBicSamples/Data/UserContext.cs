using NHibernate.Criterion;
using QBic.Core.Data;
using System.Threading.Tasks;
using Website.Models;
using WebsiteTemplate.Data;
using WebsiteTemplate.Models;

namespace Website.Data
{
    public class UserContext : UserContextBase<User>
    {
        public UserContext(DataStore dataStore)
            : base(dataStore)
        {
        }
        public override System.Threading.Tasks.Task<User> FindUserByEmailAsync(string name)
        {
            User result;

            using (var session = DataStore.OpenSession())
            {
                result = session.CreateCriteria<User>()
                                .Add(Restrictions.Eq("Email", name).IgnoreCase())
                                .UniqueResult<User>();
            }
            return Task.FromResult(result);
        }
    }
}