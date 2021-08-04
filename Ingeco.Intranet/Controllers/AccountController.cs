using Ingeco.Intranet.Data.Models;
using Ingeco.Intranet.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartB1t.Security.Extensions.AspNetCore;
using SmartB1t.Security.WebSecurity.Local;
using SmartB1t.Security.WebSecurity.Local.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace Ingeco.Intranet.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountSecurityRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string _profileTmpFolder;
        private readonly string _profileTmpPath;
        private readonly string _profileDefaultPath;

        public AccountController(IAccountSecurityRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
            _profileTmpFolder = Path.Combine(_hostEnvironment.WebRootPath, "img", "tmp");
            _profileTmpPath = Path.Combine(_profileTmpFolder, "profiletmp.jpg");
            _profileDefaultPath = Path.Combine(_hostEnvironment.WebRootPath, "img", "layout", "default-profile-pic.jpg");
        }

        #region Auth

        [HttpGet]
        public IActionResult Login(string returnUrl)
            => View(model: new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _repository.GetUserAsync(viewModel.Email);
                if (user != null)
                {
                    if (user.Active)
                    {
                        if (await _repository.AuthenticateUser(user, viewModel.Password))
                        {
                            await user.SignInAsync(HttpContext, Constants.AUTH_SCHEME, viewModel.RememberSession);
                            return !string.IsNullOrEmpty(viewModel.ReturnUrl) ? Redirect(viewModel.ReturnUrl) : Redirect("/");
                        }
                        else
                        {
                            ModelState.AddModelError("Contraseña incorrecta", "La contraseña es incorrecta.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Usuario desactivado", $"El usuario {user.Email} se encuentra desactivado. Contacte al administrador de la web para más información.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Usuario desconocido", "El usuario no existe.");
                }
            }
            return View(viewModel);
        }

        public IActionResult Logout(string returnUrl = "/")
        {
            return RedirectPermanent(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AccessDenied()
        {
            return Ok();
        }

        #endregion

        #region Management

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int page = 1, int usersPerPage = 10)
        {
            var users = await _repository.GetUsersAsync(Range.All);
            var totalPages = (int)Math.Round((decimal)users.Count() / usersPerPage, 0, MidpointRounding.AwayFromZero);
            if (page > totalPages)
            {
                page = 1;
            }
            var vm = new AccountManagamentViewModel
            {
                Users = users,
                PagesCount = totalPages,
                CurrentPage = page,
                UsersPerPage = usersPerPage
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            RemoveTempDirectory();
            var vm = new CreateUserViewModel
            {
                RoleList = await GetRoleViewModelsAsync()
            };
            return View(vm);
        }

        private async Task<IEnumerable<RoleViewModel>> GetRoleViewModelsAsync()
        {
            var roles = await _repository.GetRolesAsync();
            var vmRoles = GetRoleViewModels(roles);
            return vmRoles;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreateUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToActionPermanent("Index");
            }
            viewModel.RoleList = await GetRoleViewModelsAsync();
            return View(viewModel);
        }

        private static IEnumerable<RoleViewModel> GetRoleViewModels(IEnumerable<Role> roles)
        {
            return roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            });
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public async Task<IActionResult> UploadTempUserPhoto(IFormFile profilephoto)
        {
            RemoveTempDirectory();
            if (profilephoto is not null)
            {
                var fileId = Guid.NewGuid().ToString();
                var fileName = Path.Combine(_profileTmpFolder, $"{fileId}.jpg");
                using var file = new FileStream(fileName, FileMode.Create);
                await profilephoto.CopyToAsync(file);
                if (System.IO.File.Exists(fileName))
                {
                    return Ok(new { url = $"{Url.Action("ProfileTempPhoto")}?fileId={fileId}", fileId = fileId });
                }
                else
                {
                    return BadRequest("Error creando fichero en el servidor.");
                }
            }
            return BadRequest(new { errorMessage = "Error no esperado." });
        }

        private void RemoveTempDirectory()
        {
            var files = Directory.EnumerateFiles(_profileTmpFolder);
            foreach (var file in files)
            {
                System.IO.File.Delete(file);
            }
        }

        public FileStreamResult ProfileTempPhoto(string fileId)
        {
            var fileName = Path.Combine(_profileTmpFolder, $"{fileId}.jpg");
            var pictureBytes = System.IO.File.ReadAllBytes(System.IO.File.Exists(fileName) ? fileName : _profileDefaultPath);
            var ms = new MemoryStream(pictureBytes);
            return new FileStreamResult(ms, new MediaTypeHeaderValue("image/jpg"))
            {
                FileDownloadName = "Profile.jpg"
            };
        }

        #endregion
    }
}