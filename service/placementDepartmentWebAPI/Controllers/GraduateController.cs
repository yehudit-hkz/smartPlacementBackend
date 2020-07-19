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

namespace placementDepartmentWebAPI.Controllers
{
    [RoutePrefix("api/Graduate")]
    public class GraduateController : ApiController
    {
        [Route("GetLazyList")]
        public ApiRes<MainGraduateDto> Get(int page, int size, string sort, [FromUri]GraduateFilters filters)
        {
            try
            {
                return GraduateDtoManager.GraduateDtoLazyList(filters, sort, page, size);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("GetAll")]
        public List<FullGraduateDto> Get()
        {
            try
            {
                return GraduateDtoManager.GraduateDtoList();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("GetById")]
        public FullGraduateDto Get(string id)
        {
            try
            { 
                return GraduateDtoManager.GraduateDtoById(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }

        [Route("GetForJob")]
        public List<FullGraduateDto> Get(int idSubject, int idJob)
        {
            try
            {
                return GraduateDtoManager.GraduateDtoForJob(idSubject, idJob);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("Save")]
        public void Post([FromBody]FullGraduateDto graduateDto)
        {
            try
            {
                GraduateDtoManager.NewGraduateDto(graduateDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Route("importFromExcel")]
        public void Post([FromBody]Dictionary<string,string> keyValues )
        {
           // GraduateDtoManager.NewGraduateDto(graduateDto);
        }

        [Route("Edit")]
        public void Put(/*string id,*/ [FromBody]FullGraduateDto graduateDto)
        {
            try
            {
                //why id? for case that id is changed
                GraduateDtoManager.GraduateDtoEditing(graduateDto);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        [Route("UploadCVFile")]
        [HttpPost]
        public void UploadCVFile()
        {
            //recaived file, convert word document to ptf, save, and update linkToCV.
            var httpRequest = HttpContext.Current.Request;
            string temp = "~/ResumeFile/";
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath(temp + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    if (Path.GetExtension(filePath)==".doc" || Path.GetExtension(filePath)==".docx") {
                        Application appWord = new Application();
                        var wordDocument = appWord.Documents.Open(filePath);
                        wordDocument.ExportAsFixedFormat(Path.ChangeExtension(filePath, ".pdf"), WdExportFormat.wdExportFormatPDF);
                        wordDocument.Close();
                        File.Delete(filePath);
                    }
                    if (file != "file")
                        GraduateDtoManager.GraduateDtoUploudFile(file, "ResumeFile/"+ postedFile.FileName);
                }
            }
        }

        [Route("Delete")]
        public void Delete(string id)
        {
            try
            {
                GraduateDtoManager.DeleteGraduateDto(id);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
