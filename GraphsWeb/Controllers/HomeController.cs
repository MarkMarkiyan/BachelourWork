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
using GraphSharpDemo.Service.Converters;

namespace GraphsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileReaderService fileReaderService;
        private readonly IConverter converter;
        private readonly IMatrixToGraphConverter matrixToGraphConverter;
        private readonly IParallelFormBuilder parallelFormBuilder;
        private readonly ExpressionToMatrixConverter expressionToMatrixConverter;
        private readonly StringToExpressionConverter stringToExpressionConverter;

        public HomeController()
        {
            expressionToMatrixConverter = new ExpressionToMatrixConverter();
            stringToExpressionConverter = new StringToExpressionConverter();
            fileReaderService = new FileReaderService();
            converter = new FileToMatrixConverter();
            matrixToGraphConverter = new MatrixToGraphConverter();
            parallelFormBuilder = new ParallelFormBuilder();
        }

        public ActionResult Index()
        {
            return View();
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
            var fileT = fileReaderService.ReadTextFromFile(file.InputStream);
            var matrix = converter.Convert(fileT);

            var graphForm = matrixToGraphConverter.Convert(matrix);
            TempData["GraphMatrix"] = matrix;

            TempData["Graph"] = graphForm;
            return RedirectToAction("ViewParallelForm");
        }

        public ActionResult ViewResultFromExtension(string extension)
        {
            var ext = stringToExpressionConverter.Convert(extension);
            var res = expressionToMatrixConverter.Convert(new List<ExpressionElement>(ext));
            return RedirectToAction("ViewParallelForm");
        }

        public ActionResult ViewParallelForm() {
            var paralellForm =  parallelFormBuilder.GetParallelForm(TempData["GraphMatrix"] as string[,]);

            if (paralellForm == null)
                RedirectToAction("RunExpression");
                return View(new ParallelFormModel {
                NodesConnections = TempData["Graph"] as List<NodesConnection>,
                ParallelForm = paralellForm
            });
        }
    }
}