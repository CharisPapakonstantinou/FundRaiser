using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FundRaiser.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using FundRaiser.Common.Models;
using FundRaiser.Common.Interfaces;
using System.Threading.Tasks;

namespace FundRaiser.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectService _projectService;

        public HomeController(ILogger<HomeController> logger, IProjectService projectService)
        {
            _logger = logger;
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjects(1, 10);
            return View(projects);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}