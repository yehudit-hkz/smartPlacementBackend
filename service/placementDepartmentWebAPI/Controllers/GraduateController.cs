using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.Office.Interop.Word;
using System.IO;
using placementDepartmentCOMMON;
using placementDepartmentBLL;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Configuration;

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/Graduate")]
    public class GraduateController : ApiController
    {
        [Route("GetLazyList")]
        public ApiRes<MainGraduateDto> Get(int page, int size, string sort, [FromUri]GraduateFilters filters)
        {
            return GraduateDtoManager.GraduateDtoLazyList(filters, sort, page, size);
        }

        [Route("GetAll")]
        public List<MainGraduateDto> Get()
        {
            return GraduateDtoManager.GraduateDtoList();
        }

        [Route("GetById")]
        public FullGraduateDto Get(string id)
        {
            return GraduateDtoManager.GraduateDtoById(id);
        }

        [Route("GetForJob")]
        public List<FullGraduateDto> Get(int idSubject, int idJob)
        {
            return GraduateDtoManager.GraduateDtoForJob(idSubject, idJob);
        }

        [Route("Save")]
        public FullGraduateDto Post([FromBody]FullGraduateDto graduateDto)
        {
            var userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            return GraduateDtoManager.NewGraduateDto(graduateDto,userId);
        }

        [Route("importFromExcel")]
        [HttpPost]
        public ImportRes importFromExcel()
        {
            var httpRequest = HttpContext.Current.Request;
            var file = httpRequest.Files["file"];
            Dictionary<string, string> keyValues =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    httpRequest.Form["data"]
                    );
            int startLine = int.Parse(httpRequest.Form["start"]);
            int endLine = int.Parse(httpRequest.Form["end"]);
            ImportGraduateFromExcel importGraduate = new ImportGraduateFromExcel();
            return importGraduate.execut(keyValues, file.InputStream, startLine, endLine);
            
        }

        [Route("sendActiveMSG")]
        [HttpPost]
        public List<string> Post([FromBody]List<FullGraduateDto> graduatesDto)
        {
            var userId = Int32.Parse(HttpContext.Current.User.Identity.Name);
            string email = ConfigurationManager.AppSettings["byEmail"];
            string password = ConfigurationManager.AppSettings["password"];
            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.Substring(0, url.IndexOf("/api"));

            MailManager mail = new MailManager(email, password, url);
            return mail.sendActivatedToGraduates(graduatesDto, userId);
        }

        [Route("Edit")]
        public void Put([FromBody]FullGraduateDto graduateDto)
        {
            GraduateDtoManager.GraduateDtoEditing(graduateDto);
        }

        [Route("Delete")]
        public void Delete(string id)
        {
            GraduateDtoManager.DeleteGraduateDto(id);
        }

        [Route("GetCVFile")]
        [HttpGet]
        public HttpResponseMessage GetFile(string fileName)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string folderPeth = ConfigurationManager.AppSettings["CVpath"];
            FileStream fileStream = File.OpenRead(folderPeth + "\\" + fileName);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [Route("UploadCVFile")]
        [HttpPost]
        public void UploadCVFile()
        {
            //recaived file, convert word document to ptf, save, and update linkToCV.
            var httpRequest = HttpContext.Current.Request;
            string folderPeth = ConfigurationManager.AppSettings["CVpath"];
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = folderPeth + "\\" + postedFile.FileName;
                    postedFile.SaveAs(filePath);
                    //if (Path.GetExtension(filePath) == ".doc" || Path.GetExtension(filePath) == ".docx")
                    //{
                    //    Application appWord = new Application();
                    //    var wordDocument = appWord.Documents.Open(filePath);
                    //    var nfilePath = Path.ChangeExtension(filePath, ".pdf");
                    //    wordDocument.ExportAsFixedFormat(nfilePath, WdExportFormat.wdExportFormatPDF);
                    //    wordDocument.Close();
                    //    File.Delete(filePath);
                    //    filePath = nfilePath;
                    //}
                    if (file != "file")
                        GraduateDtoManager.GraduateDtoUploudFile(file, Path.GetFileName(filePath));
                }
            }
        }
    }
}
