using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class ConfigurationsController : Controller
    {
        public readonly IConfigurationsService configurationsService;

        public ConfigurationsController(IConfigurationsService configurationsService)
        {
            this.configurationsService = configurationsService;
        }

        public IActionResult Index()
        {
            var configurations = configurationsService.GetAllConfigurations();

            var model = configurations.Select(c => new WebSite.Models.ConfigurationListViewModel()
            {
                Id = c.Id,
                Value = c.Value,
                ConfigurationTypeId = c.ConfigurationTypeId,
                ConfigurationType = c.ConfigurationType.Name
            }).ToList();

            return View(model);
        }


        public IActionResult Update(Configuration configuration)
        {
            var model = configuration;

            configurationsService.UpdateConfigurations(configuration);

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id)
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

        public IActionResult Add()
        {
            //var model = new ConfigurationViewModel()
            //{
            //    ConfigurationTypes = configurationsService.getTypes()
            //};
            return View();
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

            configurationsService.Add(newConfiguration);//dobavitj cashe

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
