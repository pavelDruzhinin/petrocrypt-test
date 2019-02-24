using System.Linq;
using System.Text;
using System.Web.Mvc;
using VideoApp.Models;
using VideoApp.Models.Shared;
using VideoApp.Models.ViewModel;

namespace VideoApp.Controllers
{
    public class HomeController : Controller
    {
        #region Перегруженный метод для Json
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        #endregion

        public ActionResult Video()
        {
            return View();
        }

        public JsonResult JsonData()
        {
            var viewModel = new VideoViewModel();
            viewModel.LoadData();
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRow(int id)
        {
            var viewModel = new VideoViewModel();
            viewModel.DeleteVideo(id);
            viewModel.LoadData();
            return Json(viewModel);
        }

        [HttpPost]
        public JsonResult AddRow(Video video)
        {
            var viewModel = new VideoViewModel();
            viewModel.AddVideo(video);
            viewModel.LoadData();
            return Json(viewModel.Videos.Last());
        }

        [HttpPost]
        public JsonResult UpdateRow(Video video)
        {
            var viewModel = new VideoViewModel();
            viewModel.UpdateVideo(video);
            viewModel.LoadData();
            return Json(viewModel.Videos);
        }
    }
}
