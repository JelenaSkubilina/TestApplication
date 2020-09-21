using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class ConfigurationsController : Controller
    {
        public readonly IConfigurationsService configurationsService;
        private readonly IConfigurationTypeService configurationTypeService;

        public ConfigurationsController(IConfigurationsService configurationsService,
            IConfigurationTypeService configurationTypeService)
        {
            this.configurationsService = configurationsService;
            this.configurationTypeService = configurationTypeService;
        }

        public IActionResult Index()
        {
            var configurations = configurationsService.GetAllConfigurations();

            var model = configurations.Select(c => new ConfigurationListViewModel()
            {
                Id = c.Id,
                Value = c.Value,
                ConfigurationTypeId = c.ConfigurationTypeId,
                ConfigurationType = c.ConfigurationType.Name
            }).ToList();

            return View(model);
        }


        public IActionResult Edit(int id)
        {
            var configuration = configurationsService.GetConfiguration(id);

            if (configuration == null)
                return NotFound();

            var model = new ConfigurationViewModel()
            {
                Id = configuration.Id,
                ConfigurationTypeId = configuration.ConfigurationTypeId,
                Value = configuration.Value,
                ConfigurationType = configuration.ConfigurationType.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ConfigurationViewModel model)
        {
            var configuration = configurationsService.GetById(model.Id);

            if (configuration == null)
                return NotFound();

            configuration.Value = model.Value;
            configurationsService.Update(configuration);

            return View(model);
        }

        public IActionResult Add()
        {
            var configurationTypes = configurationTypeService.GetTypes().ToList();

            var model = new ConfigurationViewModel()
            {
                ConfigurationTypes = configurationTypes.Select(t => new SelectListItem()
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ConfigurationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newConfiguration = new Configuration()
            {
                ConfigurationTypeId = model.ConfigurationTypeId,
                Value = model.Value           
            };

            configurationsService.Add(newConfiguration);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var configuration = configurationsService.GetById(id);

            if (configuration == null)
                return NotFound();

            var model = new ConfigurationViewModel()
            {
                Id = configuration.Id,
                ConfigurationTypeId = configuration.ConfigurationTypeId,
                Value = configuration.Value
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var configuration = configurationsService.GetById(id);

            configurationsService.Delete(configuration);//delete from cashe

            return RedirectToAction(nameof(Index));
        }
    }
}
