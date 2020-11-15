using AutoMapper;
using LinksShare.Models;
using LinksShare.Models.Dto;
using LinksShare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace LinksShare.Controllers
{

    //[AutoValidateAntiforgeryToken]
    [Authorize]
    public class LinksController : Controller
    {
        private readonly ILogger<LinksController> _logger;
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;

        public LinksController(ILogger<LinksController> logger, ILinkService linkService, IMapper mapper)
        {
            _logger = logger;
            _linkService = linkService;
            _mapper = mapper;
        }

        public async Task<ViewResult> Index()
        {
            var olehLinks = await _linkService.GetUserLinksAsync(User.Identity.Name).ConfigureAwait(false);
            return View(olehLinks);
        }

        [HttpGet]
        public ViewResult CreateLink()
        {
            return View(new CreateLinkDto { 
                UserName = User.Identity.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLink([FromForm]CreateLinkDto createLinkDto)
        {
            if(ModelState.IsValid)
            {
                var linkInfo = _mapper.Map<LinkInfo>(createLinkDto);
                var success = await _linkService.AddLinkToUserAsync(createLinkDto.UserName, linkInfo).ConfigureAwait(false);
                if(!success)
                {
                    _logger.LogInformation($"Error of adding link to user: {JsonConvert.SerializeObject(createLinkDto)}");
                    return View();
                } else
                {
                    return RedirectToAction(nameof(Index));
                }
            } else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveLink(string user, int id)
        {
            var result = await _linkService.DeleteUserLink(user, id).ConfigureAwait(false);
            if(!result)
            {
                _logger.LogError($"Error of removing link: {id} for user: {user}");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditLink(string user, int id)
        {
            var userLinks = await _linkService.GetUserLinksAsync(user).ConfigureAwait(false);
            if(userLinks == null)
            {
                _logger.LogError($"Links not found for user: {user}");
                return RedirectToAction(nameof(Index));
            }
            var linkToEdit = userLinks.Links?.FirstOrDefault(uLink => uLink.LinkId == id) ?? null;
            if(linkToEdit == null)
            {
                _logger.LogError($"Link info with id: {id} not found for user: {user}");
                return RedirectToAction(nameof(Index));
            }
            var editLinkDto = _mapper.Map<EditLinkDto>(linkToEdit);
            editLinkDto.UserName = user;
            return View(editLinkDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditLink([FromForm]EditLinkDto editLinkDto)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var linkToEdit = _mapper.Map<LinkInfo>(editLinkDto);
            var success = await _linkService.UpdateUserLinkAsync(editLinkDto.UserName, linkToEdit).ConfigureAwait(false);
            if(!success)
            {
                _logger.LogError($"Error of updating link {JsonConvert.SerializeObject(editLinkDto)}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
