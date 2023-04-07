using Lang.Dictionary.App.Services;
using Lang.Dictionary.Web.Extensions;
using Lang.Dictionary.Web.Models.WordsLists;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lang.Dictionary.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class WordsListsController : Controller
    {
        private readonly WordsListsService _wordsListsService;

        public WordsListsController(WordsListsService wordsListsService)
        {
            _wordsListsService = wordsListsService;
        }

        public async Task<IActionResult> Index()
        {
            var wordsLists = await _wordsListsService.GetLists(ownerId: User.GetUserId());
            return View(new WordsListsViewModel { WordsLists = wordsLists });
        }
    }
}
