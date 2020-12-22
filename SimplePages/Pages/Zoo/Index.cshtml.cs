using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimplePages.Models;
using SimplePages.Services.Interfaces;

namespace SimplePages.Pages.Zoo
{
    public class Index : PageModel
    {
        public Index(IZooService zooService, ILogger<Index> logger)
        {
            ZooService = zooService;
            Logger = logger;
        }
        
        
        public IZooService ZooService { get; }
        
        
        private ILogger<Index> Logger { get; }
        
        public IEnumerable<AnimalSlot> Animals { get; private set; }
        
        public async Task OnGet()
        {
            Logger.LogInformation("Executing Zoo.Index()");
            Animals = await ZooService.GetAnimalsAsync(animal => true).ConfigureAwait(false);
        }
    }
}