using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScheduleViewer.Controllers
{
    public class HomeController : Controller
    {
        private JobPlanningEntities db = new JobPlanningEntities();


        public ActionResult Index()
        {
            var jobs = db.Jobs.AsNoTracking().OrderBy(x => x.start_time).ToList();

            var model = jobs.GroupBy(x => x.name)
                .Select(x =>
                new {
                    job = x.Key,
                    current = 0,
                    occurences = x.Select(y => new {
                        instanceId = y.instance_id,
                        from = ToJsTime(y.start_time),
                        to = ToJsTime(y.end_time)
                    }).ToList()
                }).ToArray();

            return View(model);
        }

        private double ToJsTime(DateTime? dt)
        {
            var st = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var t = (dt.Value.ToUniversalTime() - st);
            return t.TotalMilliseconds;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}