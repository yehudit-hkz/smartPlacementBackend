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
        [Route("GetAll")]
        public List<GraduateDto> Get()
        { 
            return GraduateDtoManager.GraduateDtoList();
        }

        [Route("GetById")]
        public GraduateDto Get(string id)
        {
           return GraduateDtoManager.GraduateDtoById(id);
        }

        [Route("GetBySubject")]
        public List<GraduateDto> Get(SubjectDto subjectDto)
        {
            return GraduateDtoManager.GraduateDtoBySubject(subjectDto);
        }

        [Route("Save")]
        public void Post([FromBody]GraduateDto graduateDto)
        {
            GraduateDtoManager.NewGraduateDto(graduateDto);
        }

        [Route("Edit")]
        public void Put(/*string id,*/ [FromBody]GraduateDto graduateDto)
        {
            //why id? for case that id is changed
            GraduateDtoManager.GraduateDtoEditing(graduateDto);
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
                }
            }
        }

        [Route("Delete")]
        public void Delete(string id)
        {
            GraduateDtoManager.DeleteGraduateDto(id);
        }
    }
}
