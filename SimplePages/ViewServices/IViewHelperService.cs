using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimplePages.Models.GymStats;

namespace SimplePages.ViewServices
{
    public interface IViewHelperService
    {
        List<SelectListItem> GetExerciseNameListItems();
    }
}