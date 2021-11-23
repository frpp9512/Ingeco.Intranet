using Ingeco.Intranet.Data.Interfaces;
using Ingeco.Intranet.Data.Models;
using Ingeco.Intranet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SmartB1t.Security.Extensions.AspNetCore;
using SmartB1t.Security.WebSecurity.Local.Interfaces;
using SmartB1t.Web.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsManagementRepository _repository;
        private readonly IAccountSecurityRepository _accountsRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string _imageTmpFolder;
        private readonly string _videoTmpFolder;
        private readonly string _postsFolder;

        public PostsController(IPostsManagementRepository repository, IAccountSecurityRepository accountsRepository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _accountsRepository = accountsRepository;
            _hostEnvironment = hostEnvironment;
            _imageTmpFolder = Path.Combine(hostEnvironment.WebRootPath, "img", "tmp", "posts", "images");
            _videoTmpFolder = Path.Combine(hostEnvironment.WebRootPath, "img", "tmp", "posts", "videos");
            _postsFolder = Path.Combine(hostEnvironment.WebRootPath, "posts");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> IndexAsync(Guid id)
        {
            var post = await _repository.GetPostAsync(id, true, true, true);
            if (post != null)
            {
                if (!post.Public
                    && !((User.Identity.IsAuthenticated
                          && User.IsInRole("Webmaster"))
                          || (User.Identity.IsAuthenticated
                               && User.IsInRole("Publisher")
                               && post.PostedById == User.GetId())))
                {
                    return RedirectToAction("Index", "Home");
                }
                var comments = await _repository.GetCommentsForPostAsync(post.Id, 1);
                var vm = post.GetViewModel();
                vm.TotalCommentsCount = await _repository.GetTotalCommentsCountForPostAsync(post.Id);
                vm.Comments = comments.Select(c =>
                {
                    var cvm = c.GetViewModel();
                    cvm = GetTotalRepliesCount(cvm);
                    return cvm;
                });
                return View(vm);
            }
            return BadRequest("La publicación no existe.");
        }

        private CommentViewModel GetTotalRepliesCount(CommentViewModel comment)
        {
            comment.TotalRepliesCount = _repository.GetTotalRepliesForCommentAsync(comment.Id).GetAwaiter().GetResult();
            if (comment.Replies?.Any() == true)
            {
                var repliesUpdated = new List<CommentViewModel>();
                foreach (var reply in comment.Replies)
                {
                    repliesUpdated.Add(GetTotalRepliesCount(reply));
                }
                comment.Replies = repliesUpdated;
            }
            return comment;
        }

        [Authorize(Roles = "Webmaster,PostEditor")]
        [HttpGet]
        public async Task<IActionResult> List(int page = 1, int postsPerPage = 10)
        {
            // Todo: Load the total amount of comments of every post.
            var posts = await _repository.GetPostsForPageAsync(page, postsPerPage);
            var viewModel = new PostsPageViewModel
            {
                PageNumber = page,
                PageTotal = await _repository.GetTotalPostsPagesAsync(postsPerPage),
                PostsList = posts.Select(p => p.GetViewModel(_repository.GetTotalCommentsCountForPostAsync(p.Id).GetAwaiter().GetResult())),
                PostsPerPage = postsPerPage
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Webmaster,PostEditor")]
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ClearTmpFolders();
            var categories = await _repository.GetCategoriesAsync();
            var categoriesVMs = categories.Select(c => c.GetViewModel());
            var vm = new CreatePostViewModel
            {
                Categories = categoriesVMs
            };
            return View(vm);
        }

        [Authorize(Roles = "Webmaster,PostEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreatePostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var categorySelected = await _repository.GetCategoryAsync(viewModel.CategorySelected);
                var newPost = new Post
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Category = categorySelected,
                    PostedById = User.GetId(),
                    Created = DateTime.Now,
                    Body = viewModel.Body,
                    TagsLine = string.Join(";", viewModel.GetTags()),
                    Public = false
                };

                await _repository.CreatePostAsync(newPost);

                var postDirectory = Directory.CreateDirectory(Path.Combine(_postsFolder, newPost.Id.ToString())).FullName;

                var media = new List<WebMedia>();
                foreach (var image in viewModel.GetImageRecords())
                {
                    var newMediaId = Guid.NewGuid();
                    var filename = Path.Combine(postDirectory, newMediaId.ToString());
                    System.IO.File.Move(image.filename, filename);
                    var mediaImage = new WebMedia
                    {
                        Id = newMediaId,
                        Filename = filename,
                        Description = image.description,
                        IsCover = image.isCover,
                        ViewId = image.id,
                        MediaType = WebMediaType.Picture,
                        PostId = newPost.Id
                    };
                    media.Add(mediaImage);
                }
                foreach (var video in viewModel.GetVideoRecords())
                {
                    var newMediaId = Guid.NewGuid();
                    var filename = Path.Combine(postDirectory, newMediaId.ToString());
                    System.IO.File.Move(video.filename, filename);
                    var mediaVideo = new WebMedia
                    {
                        Id = newMediaId,
                        Filename = video.filename,
                        Description = video.description,
                        IsCover = video.isCover,
                        ViewId = video.id,
                        MediaType = WebMediaType.Video,
                        PostId = newPost.Id
                    };
                    media.Add(mediaVideo);
                }
                await _repository.CreateWebMediaAsync(media);
                TempData.SetModelCreated<Post, Guid>(newPost.Id);
                ClearTmpFolders();
                return RedirectToAction("List");
            }
            return View(viewModel);
        }

        [HttpPost]
        [RequestSizeLimit(524288000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        public async Task<IActionResult> UploadTmpMedia(string mediaType, IFormFile mediaFile)
        {
            if (string.IsNullOrEmpty(mediaType))
            {
                return BadRequest("Debe de especificarse el tipo de medio.");
            }
            if (mediaFile is null)
            {
                return BadRequest("El fichero no puede ser nulo.");
            }
            var tmpId = Guid.NewGuid();
            var fileName = Path.Combine(mediaType switch { "image" => _imageTmpFolder, "video" => _videoTmpFolder, _ => "" }, $"{tmpId}");
            using var file = new FileStream(fileName, FileMode.Create);
            await mediaFile.CopyToAsync(file);
            return System.IO.File.Exists(fileName)
                ? Ok(new { message = "Se ha subido correctamente el fichero al servidor.", filename = fileName, tmpId })
                : BadRequest("Ha ocurrido un error en el servidor mientras se subía el fichero.");
        }

        private void ClearTmpFolders()
        {
            var imageTmpFiles = Directory.GetFiles(_imageTmpFolder);
            for (int i = 0; i < imageTmpFiles.Length; i++)
            {
                System.IO.File.Delete(imageTmpFiles[i]);
            }

            var videoTmpFiles = Directory.GetFiles(_videoTmpFolder);
            for (int i = 0; i < videoTmpFiles.Length; i++)
            {
                System.IO.File.Delete(videoTmpFiles[i]);
            }
        }

        [HttpGet]
        public IActionResult PostTmpImage(string tmpId)
        {
            var imagePath = Path.Combine(_imageTmpFolder, tmpId);
            if (System.IO.File.Exists(imagePath))
            {
                byte[] pictureBytes = System.IO.File.ReadAllBytes(imagePath);
                var ms = new MemoryStream(pictureBytes);
                return new FileStreamResult(ms, new MediaTypeHeaderValue("image/jpeg"))
                {
                    FileDownloadName = $"postimage_{tmpId}"
                };
            }
            return BadRequest("La imágen no existe en el servior.");
        }

        [HttpGet]
        public IActionResult PostTmpVideo(string tmpId)
        {
            var videoPath = Path.Combine(_videoTmpFolder, tmpId);
            if (System.IO.File.Exists(videoPath))
            {
                byte[] pictureBytes = System.IO.File.ReadAllBytes(videoPath);
                var ms = new MemoryStream(pictureBytes);
                return new FileStreamResult(ms, new MediaTypeHeaderValue("video/mp4"))
                {
                    FileDownloadName = $"postvideo_{tmpId}"
                };
            }
            return BadRequest("El video no existe en el servior.");
        }

        public IActionResult Media(Guid id)
        {
            var media = _repository.GetMediaAsync(id).Result;
            if (media is not null)
            {
                byte[] mediaBytes = System.IO.File.ReadAllBytes(media.Filename);
                var ms = new MemoryStream(mediaBytes);
                return new FileStreamResult(ms, new MediaTypeHeaderValue(media.MediaType == WebMediaType.Picture ? "image/jpeg" : "video/mp4"))
                {
                    FileDownloadName = media.Id.ToString()
                };
            }
            return BadRequest("El medio no existe.");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DoCommentAsync(Guid postId, string commentText, bool isReply = false, Guid? replyTo = null)
        {
            if (!string.IsNullOrEmpty(commentText))
            {
                var user = await _accountsRepository.GetUserAsync(User.GetId());
                Comment comment = new()
                {
                    Created = DateTime.Now,
                    PostId = postId,
                    IsReply = false,
                    Text = commentText,
                    User = user
                };
                if (isReply)
                {
                    if (await _repository.ExistCommentInPostAsync(postId, replyTo.Value))
                    {
                        comment.IsReply = true;
                        comment.RepliedToId = replyTo;
                    }
                    else
                    {
                        return BadRequest("El comentario a responder no existe en la publicación.");
                    }
                }
                await _repository.CreateCommentAsync(comment);
                var commentVm = comment.GetViewModel();
                return PartialView("_Comment", commentVm);
            }
            return BadRequest("El comentario debe de llevar");
        }

        [HttpPost]
        public async Task<IActionResult> LoadRepliesForCommentAsync(Guid postId, Guid commentId)
        {
            if (! await _repository.ExistCommentInPostAsync(commentId, postId))
            {
                var comments = await _repository.GetRepliesForCommentAsync(commentId);
                var commentsVMs = comments.Select(c =>
                {
                    var cvm = c.GetViewModel();
                    cvm = GetTotalRepliesCount(cvm);
                    return cvm;
                });
                return PartialView("_CommentList", commentsVMs);
            }
            return BadRequest("El comentario especificado no existe o no pertenece a la publicación.");
        }
    }
}