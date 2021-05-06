using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebUI.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private IProjectRepository _projectRepo;
        private ICertificateRepository _certificateRepo;
        private IContactRepository _contactRepo;
        public AdminController(IProjectRepository projectRepo, ICertificateRepository certificateRepo, IContactRepository contactRepo)
        {
            _projectRepo = projectRepo;
            _certificateRepo = certificateRepo;
            _contactRepo = contactRepo;
        }
        public IActionResult Proje(string q)
        {
            var query = _projectRepo.GetAll();
            if (!string.IsNullOrEmpty(q))
            {
                query = _projectRepo.GetAll().Where(i=>i.Description.Contains(q));
                var count = query.Count();
                TempData["countmessage"] = $"{ count } adet kayıt bulundu.";
                return View(query);
            }
            return View(query);
        }
        public IActionResult Sertifika(string q)
        {
            var query = _certificateRepo.GetAll();
            if (!string.IsNullOrEmpty(q))
            {
                query = _certificateRepo.GetAll().Where(i => i.CertificateName.Contains(q));
                var countcer = query.Count();
                TempData["countcermessage"] = $"{ countcer } adet kayıt bulundu.";
                return View(query);
            }
            return View(query);
        }
        public IActionResult Iletisim()
        {
            return View(_contactRepo.GetAll());
        }
        public IActionResult ProDelete(int id,string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\projeler", filename);
            if (System.IO.File.Exists(path))
            {
                _projectRepo.DeleteProject(id);
                System.IO.File.Delete(path);
                TempData["promessage"] = $"{id} numaralı kayıt silindi.";
            }
            return RedirectToAction("Proje");
        }
        public IActionResult CerDelete(int id,string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\sertifika", filename);
            if (System.IO.File.Exists(path))
            {
                _certificateRepo.DeleteCertificate(id);
                System.IO.File.Delete(path);
                TempData["cermessage"] = $"{id} numaralı kayıt silindi.";
            }
            return RedirectToAction("Sertifika");
        }
        public IActionResult ConDelete(int id)
        {
            _contactRepo.DeleteContact(id);
            return RedirectToAction("Iletisim");
        }
        [HttpGet]
        public IActionResult ProCreate()
        {
            return View(new Project());
        }
        [HttpPost]
        public async Task<IActionResult> ProCreate(Project model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\projeler", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.ImageUrl = file.FileName;
                    }
                }
                _projectRepo.AddProject(model);
                TempData["promessage"] = $"{model.Description} eklendi.";
                return RedirectToAction("Proje");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CerCreate()
        {
            return View(new Certificate());
        }
        [HttpPost]
        public async Task<IActionResult> CerCreate(Certificate model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\sertifika", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.ImageUrl = file.FileName;
                    }
                }
                _certificateRepo.AddCertificate(model);
                TempData["cermessage"] = $"{model.CertificateName} eklendi.";
                return RedirectToAction("Sertifika");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ProEdit(int id)
        {
            return View(_projectRepo.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> ProEdit(Project model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\projeler", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.ImageUrl = file.FileName;
                    }
                }
                _projectRepo.UpdateProject(model);
                TempData["message"] = $"{model.Description} güncellendi.";
                return RedirectToAction("Proje");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CerEdit(int id)
        {
            return View(_certificateRepo.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CerEdit(Certificate model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\sertifika", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.ImageUrl = file.FileName;
                    }
                }
                _certificateRepo.UpdateCertificate(model);
                TempData["message"] = $"{model.CertificateName} güncellendi.";
                return RedirectToAction("Sertifika");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
