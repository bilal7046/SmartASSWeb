using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;
using SmartASSWeb.ViewModels;

namespace SmartASSWeb.Controllers
{
    public class EnterpriseController 
        : ControllerBase
    {
        private readonly IFirebaseService _service;
        private readonly IFirebaseAdminService _firebaseAdminService;
        private readonly IUniqueCodeGenerator _codeGenerator;

        public EnterpriseController(IFirebaseService service, IFirebaseAdminService firebaseAdminService, IUniqueCodeGenerator codeGenerator)
        {
            _service = service;
            _firebaseAdminService = firebaseAdminService;
            _codeGenerator = codeGenerator;
        }


        public async Task<ActionResult> ManageEnterprise()
        {
            var enterprises = await _service.GetCollection<Enterprise>(FirestoreTableStore.Enterprises);
            return View(enterprises);
        }
        public async Task<ActionResult> ManageEnterpriseUsers()
        {
            return View();
        }

        public async Task<ActionResult> SubmitEnterprise()
        {
            var obj = new Enterprise { EnterpriseId = _codeGenerator.Generate(8) };
            var model = new EnterpriseViewModel
            {
                Enterprise = obj
            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> SubmitEnterprise(EnterpriseViewModel model)
        {
            var exists = await _service.Get<BusinessCard>(FirestoreTableStore.Enterprises, model.Enterprise.EnterpriseId);
            if (exists == null)
            {
                var response = await ImportEnterpriseUsers();
                if (response.IsSuccessful)
                {
                    await _service.Add(model.Enterprise.EnterpriseId, FirestoreTableStore.Enterprises, model.Enterprise);
                }
                else
                {
                    return Json(response);
                }
            }
            else
            {
                await _service.Update(model.Enterprise.EnterpriseId, FirestoreTableStore.Enterprises, model.Enterprise.ToDictionary());
            }

            return Json(new Response{IsSuccessful = true, Message = "Imported Successfully."});
        }

        private async Task<Response> ImportEnterpriseUsers()
        {
            try
            {
                var enterpriseUsers = new List<EnterpriseUserViewModel>();
                if (Request.Files.Count == 0)
                {
                    return new Response
                    {
                        IsSuccessful = false,
                        Message = "No import data attached!"
                    };
                }

                //var file = Request.Files[0];
                var excelPath = System.Web.HttpContext.Current.Server.MapPath(@"~\App_Data\" + Request.Files[0].FileName);
                Request.Files[0].SaveAs(excelPath);
                using (var stream = System.IO.File.Open(excelPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var i = 0;
                        do
                        {
                            while (reader.Read())
                            {
                                if (i == 0)
                                {
                                    i++;
                                    continue;
                                }

                                if (reader.GetString(0) == null) continue;
                                enterpriseUsers.Add(new EnterpriseUserViewModel
                                {
                                    Email = reader.GetString(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Gender = reader.GetString(3),
                                    Phone = reader.GetString(4),
                                    Mobile = reader.GetString(5),
                                    EmployeeCode = reader.GetString(6)
                                });
                            }
                        } while (reader.NextResult());
                    }
                }
                System.IO.File.Delete(excelPath);

                var sb = new StringBuilder();
                //And new users and save new profiles if do not exists into SmartASS platform
                foreach (var user in enterpriseUsers)
                {
                    var response = await _firebaseAdminService.RegisterUser(user.Email, user.Phone, user.Mobile, user.FirstName, user.LastName, user.Gender, user.EmployeeCode);
                    if (!response.IsSuccessful)
                    {
                        sb.AppendLine($"'{user.Email}' failed to import: {response.Message}");
                    }
                }

                if (sb.Length > 0)
                {
                    return new Response
                    {
                        IsSuccessful = false,
                        Message = "Import Failed: \n" + sb.ToString()
                    };
                }

                return new Response
                {
                    IsSuccessful = true,
                    Message = "Enterprise Users imported successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<ActionResult> ViewEnterprise(string enterpriseId)
        {
            var enterprise = await _service.Get<Enterprise>(FirestoreTableStore.Enterprises, enterpriseId);
            return View(enterprise);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEnterprise(string enterpriseId)
        {
            await _service.Delete(enterpriseId, FirestoreTableStore.Enterprises);

            return RedirectToAction("ManageEnterprise");
        }

        public async Task<ActionResult> ViewEnterpriseUser(string enterpriseId)
        {
            var enterprise = await _service.Get<Enterprise>(FirestoreTableStore.Enterprises, enterpriseId);
            return View(enterprise);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEnterpriseUser(string userId)
        {
            await _service.Update(userId, FirestoreTableStore.UserProfiles, "enterpriseId", null);

            return RedirectToAction("ManageEnterpriseUsers");
        }

        public ActionResult DownloadExcelTemplate()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(@"~\Templates\Enterprise-Uptake.xlsx");
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(path, contentType, Path.GetFileName(path));
        }

    }
}