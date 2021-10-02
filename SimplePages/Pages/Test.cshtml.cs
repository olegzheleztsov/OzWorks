using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimplePages.Pages;

public class TestModel : PageModel
{
    public string Message { get; private set; } = "PageModel in C#";

    public void OnGet()
    {
        Message += $" Server time is {DateTime.Now}";
    }
}
