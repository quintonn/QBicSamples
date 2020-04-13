using log4net;
using QBicSamples.SiteSpecific;
using QBic.Core.Utilities;
using QBicSamples.Models.Samples;
using System;
using Unity;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.BackEnd.BackgroundProcessing
{
    public class SampleBackgroundProcessing : BackgroundEvent
    {
        private DataService DataService { get; set; }

        private static ILog Logger = SystemLogger.GetLogger<SampleBackgroundProcessing>();

        public SampleBackgroundProcessing(IUnityContainer container, DataService dataService)
            : base(container)
        {
            DataService = dataService;
        }

        public override bool RunImmediatelyFirstTime => true; // if true, will run before calculate next run time method is run

        public override string Description => "Sample background processing";

        public override DateTime CalculateNextRunTime(DateTime? lastRunTime)
        {
            Logger.Info("Calculating next run time for sample background process");
            return DateTime.Now.AddMinutes(10); // This method can access the database or do anything to determine when to run next.
            // For the sample, it will just run every 1 hour
        }

        public override void DoWork()
        {
            Logger.Info("Doing sample background processing work");
            // In this example we will just add a new SampleModel every 10 minutes.
            // It will also add a category called "Special" if it does not exist
            using (var session = DataService.OpenSession())
            {
                var specialCat = session.QueryOver<SampleCategory>().Where(c => c.Description == "Special").SingleOrDefault();
                if (specialCat == null) // create if does not exist
                {
                    Logger.Info("Special category does not exist, adding it");
                    specialCat = new SampleCategory()
                    {
                        Description = "Special"
                    };
                    DataService.SaveOrUpdate(session, specialCat);
                    session.Flush(); // Must flush so the db is updated
                }

                var modelCount = session.QueryOver<SampleModel>().RowCount() + 1;
                var newItem = new SampleModel()
                {
                    Category = specialCat,
                    DateValue = DateTime.Now,
                    LongTestValue = "Auto #" + modelCount,
                    NumberValue = modelCount,
                    StringValue = "Auto #" + modelCount
                };
                DataService.SaveOrUpdate(session, newItem);

                session.Flush();
            }

            Logger.Info("Sample background processing complete");
        }

        public override EventNumber GetId()
        {
            return MenuNumber.SampleBackgroundProcess;
        }
    }
}