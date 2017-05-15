using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using AutoGraph.Service.Converters;
using GraphsExtensibility.Converters;
using GraphsExtensibility.Models;
using GraphsService.Converters;

namespace GraphsWeb.Controllers
{
    public class GraphDataApiController : ApiController
    {
        private readonly IFileReaderService fileReaderService;
        private readonly IConverter converter;
        private readonly IMatrixToGraphConverter matrixToGraphConverter;

        public GraphDataApiController()
        {
                fileReaderService = new FileReaderService();
                converter = new FileToMatrixConverter();
                matrixToGraphConverter = new MatrixToGraphConverter();
        }

        [HttpGet]
        public List<NodesConnection> GetExampleGraph()
        {
            var file = fileReaderService.ReadFile(@"D:\projects\BachalourWork\Parallel_algorithm\BachelorWork\Test.txt");
            var matrix = converter.Convert(file);
            var graphForm = matrixToGraphConverter.Convert(matrix);
            return graphForm;
        }
    }
}