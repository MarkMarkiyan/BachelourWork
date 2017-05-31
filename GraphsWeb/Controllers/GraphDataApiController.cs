using System.Collections.Generic;
using System.Web.Http;
using AutoGraph.Service.Converters;
using GraphsExtensibility.Converters;
using GraphsExtensibility.Models;
using GraphsService.Converters;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using GraphSharpDemo.Extensibility;
using GraphsService;
using Newtonsoft.Json.Linq;

namespace GraphsWeb.Controllers
{
    public class GraphDataApiController : ApiController
    {
        private readonly IMatrixToGraphConverter matrixToGraphConverter;
        private readonly IParallelFormBuilder parallelFormBuilder;

        public GraphDataApiController()
        {
            parallelFormBuilder = new ParallelFormBuilder();
            matrixToGraphConverter = new MatrixToGraphConverter();
        }

        [HttpPost]
        public ParallelFormModel GetOptimizedParalelForm([FromBody] JObject model) {
            var connections = model["Connections"].ToObject<List<NodesConnection>>();
            int cuncurency = model["Cuncurency"].ToObject<int>();
            var matrix = matrixToGraphConverter.ConvertGraphToMatrix(connections);
            var paralellForm = parallelFormBuilder.GetOptimizedParallelForm(matrix, cuncurency);

            return new ParallelFormModel
            {
                NodesConnections = connections,
                ParallelForm = paralellForm
            };
        }

        [HttpGet]
        public HttpResponseMessage GetExampleGraph()
        {
            Stream stream = new FileStream(@"E:\марк\BachelourWork\Test.txt", FileMode.OpenOrCreate);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentLength = stream.Length;
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "foo.bin";
            return result;
        }
    }
}