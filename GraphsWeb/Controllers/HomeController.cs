using AutoGraph.Service.Converters;
using GraphsExtensibility.Converters;
using GraphSharpDemo.Extensibility;
using GraphsService.Converters;
using GraphsService;
using System.IO;
using System.Web;
using System.Web.Mvc;
using GraphsExtensibility.Models;
using System.Collections.Generic;

namespace GraphsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileReaderService fileReaderService;
        private readonly IConverter converter;
        private readonly IMatrixToGraphConverter matrixToGraphConverter;

        private readonly IParallelFormBuilder parallelFormBuilder;

        public HomeController()
        {
            fileReaderService = new FileReaderService();
            converter = new FileToMatrixConverter();
            matrixToGraphConverter = new MatrixToGraphConverter();
            parallelFormBuilder = new ParallelFormBuilder();
        }

        public ActionResult Index()
        {
            if (TempData.ContainsKey("Graph"))
            {
                var graph = TempData["Graph"];
                return View(graph);
            }
            var file = fileReaderService.ReadFile(@"E:\марк\BachelourWork\Test.txt");
            var matrix = converter.Convert(file);
            var graphForm = matrixToGraphConverter.Convert(matrix);

            return View(graphForm);
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

        public ActionResult RunExpression()
        {
            return View();
        }


        public ActionResult ViewResult(HttpPostedFileBase file)
        {
            string path = "";

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                file.SaveAs(path);
            }

            var fileT = fileReaderService.ReadFile(path);
            var matrix = converter.Convert(fileT);

            var graphForm = matrixToGraphConverter.Convert(matrix);
            TempData["GraphMatrix"] = matrix;

            TempData["Graph"] = graphForm;
            // redirect back to the index action to show the form once again
            return RedirectToAction("ViewParallelForm");
        }

        public ActionResult ViewParallelForm() {
            var paralellForm =  parallelFormBuilder.GetParallelForm(TempData["GraphMatrix"] as int[,]);
                return View(new ParallelFormModel {
                NodesConnections = TempData["Graph"] as List<NodesConnection>,
                ParallelForm = paralellForm
            });
        }
    }
}