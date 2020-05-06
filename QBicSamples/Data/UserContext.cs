using NHibernate.Criterion;
using QBic.Core.Data;
using System.Threading.Tasks;
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
        public override System.Threading.Tasks.Task<User> FindUserByNameAsync(string name)
        {
            User result;

            using (var session = DataStore.OpenSession())
            {
                result = session.CreateCriteria<User>()
                                .Add(Restrictions.Eq("Name", name).IgnoreCase())
                                .UniqueResult<User>();
            }
            return Task.FromResult(result);
        }
    }
}