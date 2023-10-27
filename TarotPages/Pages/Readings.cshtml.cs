using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TarotPages.Pages;

public class ReadingsModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public ReadingsModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}