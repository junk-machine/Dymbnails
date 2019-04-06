using System.Web.Mvc;
using Dymbnails.Logic;
using Dymbnails.Web.Models.Dymbnails;
using Dymbnails.Web.Helpers;

namespace Dymbnails.Web.Controllers
{
    public class DymbnailsController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexModel();
            model.Items = Facade.Dymbnails.GetList();
            return View(model);
        }

        #region GetUrl

        public ActionResult GetUrl(int id) {
            var model = new GetUrlModel();
            model.DymbnailId = id;
            model.Variables = Facade.Variables.GetList(id);
            if (model.Variables.Count == 0)
                return Preview(DymbnailsHelper.GetUrl(id));
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetUrl(int dymbnailId, FormCollection collection) {
            const string dymbnailIdKey = "dymbnailId";
            //try {
                var variables = new object[(collection.Keys.Count - 1) *2];
                var variablesIndex = 0;
                foreach (string key in collection) {
                    if (key == dymbnailIdKey) continue;
                    variables[variablesIndex] = key;
                    variables[variablesIndex + 1] = collection[key];
                    variablesIndex += 2;
                }
                return Preview(DymbnailsHelper.GetUrl(dymbnailId, variables));
            /*} catch {
                return View();
            }*/
        }

        #endregion GetUrl

        private ActionResult Preview(string url) {
            ViewData.Model = url;
            return View("Preview");
        }

        #region Edit

        public ActionResult Edit(int? id)
        {
            Entities.Dymbnail model;
            if (id > 1000) {
                model = Facade.Dymbnails.Get(id.Value);
            } else {
                model = new Entities.Dymbnail();
                model.Content = DymbnailsHelper.DefaultContentTemplate;
            }
            return View(model);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection collection)
        {
            //try {
                var entity = new Entities.Dymbnail { 
                    ID = int.Parse(collection["id"]),
                    Title = collection["txtTitle"],
                    Description = collection["txtDescription"],
                    Content = collection["txtContent"]
                };
                if (entity.ID > 1000) {
                    Update(entity);
                } else {
                    Create(entity);
                }

                //return RedirectToAction("EditVariables", new { id = entity.ID });
                return RedirectToAction("Index");
            /*} catch {
                return View();
            }*/
        }

        private void Create(Entities.Dymbnail entity) {

        }

        private void Update(Entities.Dymbnail entity) {
            Facade.Dymbnails.Update(entity);
        }

        #endregion Edit
    }
}
