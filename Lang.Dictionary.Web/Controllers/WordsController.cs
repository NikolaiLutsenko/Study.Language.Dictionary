using Lang.Dictionary.App.Models.Words;
using Lang.Dictionary.App.Services;
using Lang.Dictionary.App.Settings;
using Lang.Dictionary.Web.Extensions;
using Lang.Dictionary.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace Lang.Dictionary.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class WordsController : Controller
    {
        private readonly WordsService _wordsService;
        private readonly LanguageSettings _langOptions;

        public WordsController(WordsService wordsService, IOptions<LanguageSettings> langOptions)
        {
            _wordsService = wordsService;
            _langOptions = langOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            var words = await _wordsService.GetDictionary(ownerId: User.GetUserId());
            return View(new WordsViewModel { Words = words });
        }



        [HttpGet]
        public IActionResult Create()
        {
            SetLanguages();
            return View(new CreateWordViewModel
            {
                FromLanguageId = User.GetBaseLanguageId(),
                ToLanguageId = User.GetStudyLanguageId()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateWordViewModel model)
        {
            SetLanguages();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _wordsService.SaveWord(
                model.Id,
                from: new Word(_langOptions.Get(model.FromLanguageId.Value), model.FromValue),
                to: new Word(_langOptions.Get(model.ToLanguageId.Value), model.ToValue),
                User.GetUserId());


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var getWordResult = await _wordsService.GetWord(id, User.GetUserId());
            if (!getWordResult.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            var word = getWordResult.Value;
            SetLanguages(word.From.Language.Id, word.To.Language.Id);

            return View(nameof(Create),new CreateWordViewModel
            {
                Id = id,
                FromValue = word.From.Value,
                ToValue = word.To.Value,
                FromLanguageId = word.From.Language.Id,
                ToLanguageId = word.To.Language.Id
            });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _wordsService.DeleteWord(id, User.GetUserId());

            return RedirectToAction(nameof(Index));
        }

        private void SetLanguages(int? defaultBaseId = null, int? defaultStudyId = null)
        {
            defaultBaseId ??= User.GetBaseLanguageId();
            defaultStudyId ??= User.GetStudyLanguageId();

            ViewBag.FromLanguages = _langOptions.Languages
                            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == defaultBaseId })
                            .ToList();

            ViewBag.ToLanguages = _langOptions.Languages
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == defaultStudyId })
                .ToList();
        }
    }
}
