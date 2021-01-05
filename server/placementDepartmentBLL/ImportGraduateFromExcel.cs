using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;
using placementDepartmentCOMMON;
using placementDepartmentDAL;

namespace placementDepartmentBLL
{
    public class ImportGraduateFromExcel
    {
        private static List<CityDto> cities;
        public ImportGraduateFromExcel()
        {
            cities = CityManager.CityList();
        }

        private int findCItyIdByName(string cityName)
        {
            int id = cities.Where(c => c.name == cityName).Select(c => c.Id).FirstOrDefault();
            return id;
        }
        private bool convertHEBoolToBool(string HEBool)
        {
            return HEBool == "כן" || HEBool == "בוגר" ? true : false;
        }
        public ImportRes execut(Dictionary<string, string> keyValues,//Dictionary<key -name of field, value - char of column>
                           Stream fileStream,
                           int rowStart,
                           int rowEnd /*include*/)
        {

            WorkBook workbook = WorkBook.Load(fileStream);
            WorkSheet sheet = workbook.WorkSheets[0];

            List<int> errLine = new List<int>();
            
            FullGraduateDto graduateDto;
            List<FullGraduateDto> fullGraduateDtos = new List<FullGraduateDto>();
            for (int i = rowStart; i <= rowEnd; i++)
            {
                graduateDto = new FullGraduateDto();
                try
                {
                    foreach (var item in keyValues)
                    {
                        switch (item.Key)
                        {
                            case "City":
                                graduateDto.City = new CityDto();
                                graduateDto.City.Id = findCItyIdByName(sheet[item.Value + i].Value.ToString());
                                break;
                            case "Branch":
                                graduateDto.Branch = new BranchDto();
                                graduateDto.Branch.Id = int.Parse(item.Value);
                                break;
                            case "Expertise":
                                graduateDto.Expertise = new ExpertiseDto();
                                graduateDto.Expertise.Id = int.Parse(item.Value);
                                break;
                            case "gender":
                                graduateDto.gender = item.Value;
                                break;
                            case "didGraduate":
                                graduateDto.didGraduate = bool.Parse(item.Value);
                                break;
                            case "hasDiploma":
                            case "isWorkerInProfession":
                            case "placedByThePlacementDepartment":
                            case "hasExperience":
                                graduateDto.GetType().GetProperty(item.Key).SetValue(
                                    graduateDto,
                                    convertHEBoolToBool(sheet[item.Value + i].Value.ToString())
                                    );
                                break;
                            case "phone":
                                graduateDto.phone = sheet[item.Value + i].Value.ToString().Replace("-", "");
                                break;
                            default:
                                graduateDto.GetType().GetProperty(item.Key).SetValue(
                                    graduateDto, Convert.ChangeType(
                                       sheet[item.Value + i].Value,
                                       graduateDto.GetType().GetProperty(item.Key).PropertyType
                                        )
                                    );
                                break;
                        }
                    }
                    graduateDto.isActive = false;
                    GraduateManager.NewGraduate(graduateDto);
                    fullGraduateDtos.Add(graduateDto);
                }
                catch (Exception)
                {
                    errLine.Add(i);
                }
            }

            return new ImportRes() {
                errLine = errLine,
                readLine = fullGraduateDtos
            };
        }

    }
    public class ImportRes
    {
        public List<int> errLine { get; set; }
        public List<FullGraduateDto> readLine { get; set; }
    }
}
