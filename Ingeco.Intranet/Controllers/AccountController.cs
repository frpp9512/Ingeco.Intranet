using Ingeco.Intranet.Data.Models;
using Ingeco.Intranet.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SmartB1t.Security.Extensions.AspNetCore;
using SmartB1t.Security.WebSecurity.Local;
using SmartB1t.Security.WebSecurity.Local.Interfaces;
using SmartB1t.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        #region Private members

        private readonly IAccountSecurityRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string _profileTmpFolder;
        private readonly string _profileDefaultPath;

        #endregion

        #region Constructor

        public AccountController(IAccountSecurityRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
            _profileTmpFolder = Path.Combine(_hostEnvironment.WebRootPath, "img", "tmp");
            _profileDefaultPath = Path.Combine(_hostEnvironment.WebRootPath, "img", "layout", "default-profile-pic.jpg");
        }

        #endregion

        #region Auth

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(model: new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _repository.GetUserAsync(viewModel.Email, true);
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
            return Redirect(viewModel.ReturnUrl);
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync(string returnUrl = "/")
        {
            await HttpContext.SignOutAsync();
            return RedirectPermanent(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        #region Management

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int page = 1, int usersPerPage = 5, bool includeInactive = true)
        {
            RemoveTempDirectory();

            var calculateIndexes = new Func<(int, int)>(() => ((page - 1) * usersPerPage, (page - 1) * usersPerPage + usersPerPage));
            int usersCount = await _repository.GetUsersCount(includeInactive);

            (int, int) indexes = calculateIndexes();
            if (usersCount < indexes.Item1)
            {
                page = 1;
                indexes = calculateIndexes();
            }

            var users = await _repository.GetUsersAsync(indexes.Item1, indexes.Item2, true);

            foreach (User user in users)
            {
                if (user.ProfilePicture is not null)
                {
                    using var stream = new MemoryStream(user.ProfilePicture);
                    var image = Image.FromStream(stream);
                    image.Save(GetTempPhotoPath(user.Id.ToString()), ImageFormat.Jpeg);
                }
            }

            var totalPages = (int)Math.Ceiling((decimal)usersCount / usersPerPage);
            var vm = new AccountManagamentViewModel
            {
                Users = users,
                PagesCount = totalPages == 0 ? 1 : totalPages,
                CurrentPage = page,
                UsersPerPage = usersPerPage,
                UsersCount = usersCount
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
                var user = viewModel.GetModel();

                if (!string.IsNullOrEmpty(viewModel.ProfilePictureId) && ExistsTempPhoto(viewModel.ProfilePictureId))
                {
                    var image = Image.FromFile(GetTempPhotoPath(viewModel.ProfilePictureId));
                    using var memStream = new MemoryStream();
                    image.Save(memStream, ImageFormat.Jpeg);
                    user.ProfilePicture = memStream.ToArray();
                    image.Dispose();
                }

                if (viewModel.RolesSelected.Length > 0)
                {
                    var userRoles = new List<UserRole>();
                    foreach (string selectedRole in viewModel.RolesSelected)
                    {
                        var role = await _repository.GetRoleAsync(new Guid(selectedRole));
                        if (role is not null)
                        {
                            userRoles.Add(new UserRole
                            {
                                Role = role
                            });
                        }
                    }
                    user.Roles = userRoles;
                    await _repository.CreateUserAsync(user, viewModel.Password);
                    TempData.SetModelCreated<User, Guid>(user.Id);
                    return RedirectToActionPermanent("Index");
                }
                else
                {
                    ModelState.AddModelError("NoRolesSelected", "No se ha seleccionado ningún rol a desempeñar por el usuario.");
                }
            }
            viewModel.RoleList = await GetRoleViewModelsAsync();
            return View(viewModel);
        }

        private bool ExistsTempPhoto(string fileId) 
            => System.IO.File.Exists(GetTempPhotoPath(fileId));

        private string GetTempPhotoPath(string fileId) 
            => Path.Combine(_profileTmpFolder, $"{fileId}.jpg");

        private static IEnumerable<RoleViewModel> GetRoleViewModels(IEnumerable<Role> roles) 
            => roles.Select(r => new RoleViewModel
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description
        });

        [RequestFormLimits(MultipartBodyLengthLimit = 5242880)]
        [RequestSizeLimit(5242880)]
        public async Task<IActionResult> UploadTempUserPhoto(IFormFile profilephoto)
        {
            RemoveTempDirectory();
            if (profilephoto is not null)
            {
                string fileId = Guid.NewGuid().ToString();
                string fileName = Path.Combine(_profileTmpFolder, $"{fileId}.jpg");
                using var file = new FileStream(fileName, FileMode.Create);
                await profilephoto.CopyToAsync(file);
                return System.IO.File.Exists(fileName)
                    ? Ok(new { url = $"{Url.Action("ProfileTempPhoto")}?fileId={fileId}", fileId })
                    : BadRequest("Error creando fichero en el servidor.");
            }
            return BadRequest(new { errorMessage = "Error no esperado." });
        }

        private void RemoveTempDirectory()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_profileTmpFolder);
            foreach (string file in files)
            {
                System.IO.File.Delete(file);
            }
        }

        public FileStreamResult ProfileTempPhoto(string fileId)
        {
            string fileName = Path.Combine(_profileTmpFolder, $"{fileId}.jpg");
            byte[] pictureBytes = System.IO.File.ReadAllBytes(System.IO.File.Exists(fileName) ? fileName : _profileDefaultPath);
            var ms = new MemoryStream(pictureBytes);
            return new FileStreamResult(ms, new MediaTypeHeaderValue("image/jpg"))
            {
                FileDownloadName = "Profile.jpg"
            };
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(string id)
        {
            var user = await _repository.GetUserAsync(new Guid(id), true);
            var vm = user.GetEditViewModel();
            vm.RoleList = await GetRoleViewModelsAsync();
            if (user.ProfilePicture is not null)
            {
                using var stream = new MemoryStream(user.ProfilePicture);
                var image = Image.FromStream(stream);
                image.Save(GetTempPhotoPath(user.Id.ToString()), ImageFormat.Jpeg);
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _repository.GetUserAsync(new Guid(viewModel.Id), true);

                if (viewModel.ProfilePictureId != user.Id.ToString() 
                    && !string.IsNullOrEmpty(viewModel.ProfilePictureId) 
                    && ExistsTempPhoto(viewModel.ProfilePictureId))
                {
                    var image = Image.FromFile(GetTempPhotoPath(viewModel.ProfilePictureId));
                    using var memStream = new MemoryStream();
                    image.Save(memStream, ImageFormat.Jpeg);
                    user.ProfilePicture = memStream.ToArray();
                    image.Dispose();
                }

                user.Fullname = viewModel.Fullname;
                user.Department = viewModel.Department;
                user.Position = viewModel.Position;

                if (viewModel.RolesSelected.Length > 0)
                {
                    var rolesToRemove = new List<Role>();
                    foreach (var userRole in user.Roles)
                    {
                        var roleId = viewModel.RolesSelected.FirstOrDefault(r => userRole.Role.Id.ToString() == r);
                        if (string.IsNullOrEmpty(roleId))
                        {
                            rolesToRemove.Add(userRole.Role);
                        }
                    }

                    rolesToRemove.ForEach(r => _repository.RemoveRoleFromUserAsync(user, r).Wait());

                    foreach (var selectedRole in viewModel.RolesSelected)
                    {
                        var userRole = user.Roles.FirstOrDefault(ur => ur.Role.Id.ToString() == selectedRole);
                        if (userRole is null)
                        {
                            var role = _repository.GetRoleAsync(new Guid(selectedRole)).Result;
                            _repository.AssignRoleToUserAsync(user, role).Wait();
                        }
                    }

                    await _repository.UpdateUserAsync(user);

                    TempData.SetModelUpdated<User, Guid>(new Guid(viewModel.Id));
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("NoRolesSelected", "No se ha seleccionado ningún rol a desempeñar por el usuario.");
                }
                
            }
            viewModel.RoleList = await GetRoleViewModelsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Activate(string id)
        {
            var user = await _repository.GetUserAsync(new Guid(id));
            if (user is null)
            {
                return BadRequest("El usuario no existe.");
            }
            user.Active = true;
            await _repository.UpdateUserAsync(user);
            return Ok($"El usuario {user.Fullname} ha sido activado satisfactoriamente.");
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(string id)
        {
            var user = await _repository.GetUserAsync(new Guid(id));
            if (user is null)
            {
                return BadRequest("El usuario no existe.");
            }
            user.Active = false;
            await _repository.UpdateUserAsync(user);
            return Ok($"El usuario {user.Fullname} ha sido desactivado satisfactoriamente.");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _repository.GetUserAsync(new Guid(id));
            if (user is null)
            {
                return BadRequest("El usuario no existe.");
            }
            user.PermanentDeactivation = true;
            user.Active = false;
            _repository.UpdateUserAsync(user).Wait();
            return Ok($"El usuario {user.Fullname} ha sido eliminado satisfactoriamente.");
        }

        #endregion
    }
}