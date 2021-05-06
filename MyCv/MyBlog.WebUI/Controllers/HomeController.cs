using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBlog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        CvContext c;
        private IProjectRepository _projectRepo;
        private ICertificateRepository _certificateRepo;
        private IContactRepository _contactRepo;
        private CvContext _cvContext;
        public HomeController(IProjectRepository projectRepo, ICertificateRepository certificateRepo, IContactRepository contactRepo, CvContext cvContext)
        {
            _projectRepo = projectRepo;
            _certificateRepo = certificateRepo;
            _contactRepo = contactRepo;
            _cvContext = cvContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Proje()
        {
            return View(_projectRepo.GetAll());
        }
        public IActionResult Sertifika()
        {
            return View(_certificateRepo.GetAll());
        }
        [HttpGet]
        public IActionResult Iletisim()
        {
            return View(new Contact());
        }
        [HttpPost]
        public IActionResult Iletisim(Contact model)
        {
            if (ModelState.IsValid)
            {
                _contactRepo.AddContact(model);
                TempData["message"] = $"{model.Id} numaralı mesajınız iletilmiştir";
                return RedirectToAction("Iletisim");
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login p)
        {
            var datavalue = _cvContext.Logins.FirstOrDefault(x => x.LoginName == p.LoginName && x.LoginPassword == p.LoginPassword);
            if (datavalue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.LoginName)
                };
                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Proje", "Admin");
            }
            return View();
        }
    }
}
