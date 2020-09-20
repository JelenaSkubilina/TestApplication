﻿using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using WebSite.Core.Helpers;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDataService dataService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public const int DEFAULT_PAGE_SIZE = 4;

        public DataController(IWebHostEnvironment hostingEnvironment,
            IDataService dataService,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.dataService = dataService;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var allData = dataService.GetAll();
            var data = allData.Take(DEFAULT_PAGE_SIZE).Select(d =>
                    new DataViewModel()
                    {
                        Id = d.Id,
                        DataUrl = d.DataUrl,
                        Comments = d.Comments,
                        Title = d.Title
                    }).ToList();

            var model = new DatasViewModel()
            {
                Datas = data,
                TotalRecords = allData.Count()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GetMoreData(int page)
        {
            var allData = dataService.GetAll();
            var totalRecords = allData.Count();

            allData = allData.Skip((page - 1) * DEFAULT_PAGE_SIZE).Take(DEFAULT_PAGE_SIZE);

            var datas = allData.Select(d => new DataViewModel()
            {
                Id = d.Id,
                DataUrl = d.DataUrl,
                Comments = d.Comments,
                Title = d.Title
            }).ToList();

            var model = new DatasViewModel()
            {
                Datas = datas,
                TotalRecords = totalRecords
            };

            return Ok(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var data = dataService.GetById(id.Value);

            if (data == null)
                return NotFound();

            var model = new DataViewModel()
            {
                Id = data.Id,
                DataUrl = data.DataUrl,
                Title = data.Title,
                Comments = data.Comments
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DataViewModel data)
        {
            if (!ModelState.IsValid)
                return View(data);

           // var uploadedFile = 

            var newData = new Data()
            {
                Title = data.Title,
                Comments = data.Comments,
                DataUrl = UploadFile(data.File)
            };

            dataService.Add(newData);

            return RedirectToAction(nameof(Index));
        }

        private string UploadFile(IFormFile file)
        {
            if (file.ContentType.Length > 0 && file.ContentType.Contains("image"))
            {
                var image = new MagickImage(file.OpenReadStream());
                if (image.Width < 100 || image.Height < 100)
                {
                    //return
                    }

                if (image.Width > 200 || image.Height > 200)
                {
                    var size = new MagickGeometry(200, 200);

                    image.Resize(size);

                    var fileInfo = new FileInfo(file.FileName);

                    image.Write(fileInfo);
                }

                //await cloudBlockBlob.UploadFromFileAsync(fileInfo.Name);
                // await cloudBlockBlob.UploadFromFileAsync(ResizeImage(file).FullName);
            }
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
            bool exists = Directory.Exists(uploads);

            if (!exists)
                Directory.CreateDirectory(uploads);

            var blobStorageService = new BlobStorageService(configuration["BlobConnections:AccessKey"]);

            return blobStorageService.UploadFileToBlob(file);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var data = dataService.GetById(id.Value);

            if (data == null)
                return NotFound();

            var model = new DataViewModel()
            {
                Id = data.Id,
                Title = data.Title,
                Comments = data.Comments,
                DataUrl = data.DataUrl
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = dataService.GetById(id);
            var blobStorageService = new BlobStorageService(configuration["BlobConnections:AccessKey"]);

            blobStorageService.DeleteBlobData(data.DataUrl);
            dataService.Delete(data);

            return RedirectToAction(nameof(Index));
        }
    }
}
