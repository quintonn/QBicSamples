using NHibernate.Criterion;
using QBic.Core.Data;
using System.Threading.Tasks;
using Website.Models;
using WebsiteTemplate.Data;
using WebsiteTemplate.Models;

namespace Website.Data
{
    public class UserContext : UserContextBase<PatientUser>
    {
        public UserContext(DataStore dataStore)
            : base(dataStore)
        {
        }
        public void CheckDefaultValues()
        {

        }

        public override System.Threading.Tasks.Task<PatientUser> FindUserByEmailAsync(string name)
        {
            PatientUser result;

            using (var session = DataStore.OpenSession())
            {
                result = session.CreateCriteria<PatientUser>()
                                .Add(Restrictions.Eq("Email", name).IgnoreCase())
                                .UniqueResult<PatientUser>();
            }
            return Task.FromResult(result);
        }
    }
}