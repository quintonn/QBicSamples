using Unity;
using NHibernate.Criterion;
using System;
using System.Linq;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Models;
using WebsiteTemplate.SiteSpecific.DefaultsForTest;
using WebsiteTemplate.Utilities;
using BasicAuthentication.Users;
using Microsoft.Owin.Security.DataProtection;
using QBicSamples.Models;
using Microsoft.AspNet.Identity;
using NHibernate;

namespace QBicSamples.SiteSpecific
{
    public class Startup : ApplicationStartup
    {
        private IUnityContainer Container { get; set; }

        public Startup(DataService dataService, IUnityContainer container)
            : base(dataService)
        {
            Container = container;
        }

        public override void RegisterUnityContainers(IUnityContainer container)
        {
            //throw new NotImplementedException();
        }

        public override void SetupDefaults()
        {
            // This code will create an admin user, user role and add all new menu items to the admin user roles, even if you create new ones later.

            // It is recommended to change your admin email and password asap.
            

            // Get a UserManager for CustomUser class, this is used to add, find, and work with our custom users.
            var customUserContext = Container.Resolve<CustomUserContext>();
            var dataProtectionProvider = Container.Resolve<IDataProtectionProvider>();
            var customUserManager = new CoreUserManager(customUserContext, dataProtectionProvider);

            using (var session = DataService.OpenSession())
            {
                SetupDefaultUsersAndMenus(session);

                // Create a CustomUser if one doesn't exist
                var bob = session.QueryOver<CustomUser>().Where(x => x.Email == "bob@example.com").SingleOrDefault();
                if (bob == null) // only create this user if it doesn't already exist
                {
                    bob = new CustomUser()
                    {
                        Address = "The big house in the big street",
                        Age = 50,
                        Email = "bob@example.com",
                        EmailConfirmed = true, // if email is not confirmed, we can't log in, until we confirm the account, but since we're using example.com as the email, we by-pass this.
                    };
                    var result = customUserManager.CreateAsync(bob, "password"); // set password to "password"
                    result.Wait(); // this method should be async, but currently it's not. Might fix...

                    if (!result.Result.Succeeded)
                    {
                        throw new Exception("Unable to create custom user: " + "Bob");
                        // you can do something else with this error, log it, etc.
                    }
                }

                session.Flush();
            }
        }

        private void SetupDefaultUsersAndMenus(ISession session)
        {
            var userManager = Container.Resolve<DefaultUserManager>();

            var adminUser = session.CreateCriteria<User>()
                                                   .Add(Restrictions.Eq("UserName", "Admin"))
                                                   .UniqueResult<User>();
            if (adminUser == null)
            {
                adminUser = new User(false)
                {
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    UserName = "Admin",
                };
                var result = userManager.CreateAsync(adminUser, "password");
                result.Wait();

                if (!result.Result.Succeeded)
                {
                    throw new Exception("Unable to create user: " + "Admin");
                }
            }

            var adminRole = session.CreateCriteria<UserRole>()
                                   .Add(Restrictions.Eq("Name", "Admin"))
                                   .UniqueResult<UserRole>();
            if (adminRole == null)
            {
                adminRole = new UserRole()
                {
                    Name = "Admin",
                    Description = "Administrator"
                };
                DataService.SaveOrUpdate(session, adminRole);
            }

            var adminRoleAssociation = session.CreateCriteria<UserRoleAssociation>()
                                              .CreateAlias("User", "user")
                                              .CreateAlias("UserRole", "role")
                                              .Add(Restrictions.Eq("user.Id", adminUser.Id))
                                              .Add(Restrictions.Eq("role.Id", adminRole.Id))
                                              .UniqueResult<UserRoleAssociation>();
            if (adminRoleAssociation == null)
            {
                adminRoleAssociation = new UserRoleAssociation()
                {
                    User = adminUser,
                    UserRole = adminRole
                };
                DataService.SaveOrUpdate(session, adminRoleAssociation);
            }

            var systemMenu = session.QueryOver<Menu>().Where(m => m.Name == "System").SingleOrDefault();
            if (systemMenu == null)
            {
                systemMenu = new Menu()
                {
                    Name = "System",
                    Position = -1
                };
                DataService.SaveOrUpdate(session, systemMenu);
            }

            var menuList1 = session.CreateCriteria<Menu>()
                                   .Add(Restrictions.Eq("Event", (int)EventNumber.ViewUsers))
                                   .List<Menu>();

            if (menuList1.Count == 0)
            {
                var menu1 = new Menu()
                {
                    Event = EventNumber.ViewUsers,
                    Name = "Users",
                    Position = 0,
                    ParentMenu = systemMenu
                };

                DataService.SaveOrUpdate(session, menu1);
            }

            var menuList2 = session.CreateCriteria<Menu>()
                                   .Add(Restrictions.Eq("Event", (int)EventNumber.ViewMenus))
                                   .List<Menu>();

            if (menuList2.Count == 0)
            {
                var menu2 = new Menu()
                {
                    Event = EventNumber.ViewMenus,
                    Name = "Menus",
                    Position = 1,
                    ParentMenu = systemMenu
                };
                DataService.SaveOrUpdate(session, menu2);
            }

            var userRoleMenu = session.CreateCriteria<Menu>()
                                      .Add(Restrictions.Eq("Event", (int)EventNumber.ViewUserRoles))
                                      .UniqueResult<Menu>();
            if (userRoleMenu == null)
            {
                userRoleMenu = new Menu()
                {
                    Event = EventNumber.ViewUserRoles,
                    Name = "User Roles",
                    Position = 2,
                    ParentMenu = systemMenu
                };
                DataService.SaveOrUpdate(session, userRoleMenu);
            }

            var settingsMenu = session.CreateCriteria<Menu>()
                                      .Add(Restrictions.Eq("Event", (int)EventNumber.ModifySystemSettings))
                                      .UniqueResult<Menu>();
            if (settingsMenu == null)
            {
                settingsMenu = new Menu()
                {
                    Event = EventNumber.ModifySystemSettings,
                    Name = "Settings",
                    Position = 3,
                    ParentMenu = systemMenu
                };
                DataService.SaveOrUpdate(session, settingsMenu);
            }

            var allEvents = EventService.EventList.Where(e => e.Value.ActionType != EventType.InputDataView).Select(e => e.Value.GetEventId())
                                      .ToList();

            var eras = session.CreateCriteria<EventRoleAssociation>()
                              .CreateAlias("UserRole", "role")
                              .Add(Restrictions.Eq("role.Id", adminRole.Id))
                              .List<EventRoleAssociation>()
                              .ToList();

            if (eras.Count != allEvents.Count)
            {
                eras.ForEach(e =>
                {
                    DataService.TryDelete(session, e);
                });
                session.Flush();
                foreach (var evt in allEvents)
                {
                    var era = new EventRoleAssociation()
                    {
                        Event = evt,
                        UserRole = adminRole
                    };
                    DataService.SaveOrUpdate(session, era);
                }
            }
        }
    }
}