using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NameAPI;
using NameAPI.Models;

namespace WebUI.Controllers
{
    public class ServiceController : Controller
    {
        //Instansierar nameList globalt för samtliga metoder i klassen 
        List<NameModel> nameList = new List<NameModel>();

        public async Task<ActionResult> Index()
        {
            //Default request när sidan laddas
            nameList = await NameService.GetNameList(10);

            return View(nameList);
        }

        /*
         * HttpPost används för att enbart POST requests kan skickas till metoden
         * Async metod som tar emot formulärdata från klienten och anropar NameService metoder beroende på
         * parametervärdenas eventuella null värden och returnerar en ny lista
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateRequest(NameType? nameType, NameGender? nameGender, int limit)
        {
            if (nameType == null && nameGender == null)
            {
                nameList = await NameService.GetNameList(limit);

                return View("Index", nameList);
            }
            else if (nameType == null && nameGender != null)
            {
                nameList = await NameService.GetNameList(nameGender, limit);

                return View("Index", nameList);
            }
            else if (nameGender == null && nameType != null)
            {
                nameList = await NameService.GetNameList(nameType, limit);

                return View("Index", nameList);
            }
            else
            {
                nameList = await NameService.GetNameList(nameType, nameGender, limit);

                return View("Index", nameList);
            }
            
        }

        /*
         * Samma som ovan men anropas med ett AJAX anrop från klienten istället för formulär
         * AJAX request tas emot av metoden och anropar GetNameList med givna parametrar och en JSON array returneras
         */
        [HttpPost]
        public async Task<JsonResult> AjaxRequest(NameType? nameType, NameGender? nameGender, int limit)
        {
            if (nameType == null && nameGender == null)
            {
                nameList = await NameService.GetNameList(limit);

                return Json(nameList);
            }
            else if (nameType == null && nameGender != null)
            {
                nameList = await NameService.GetNameList(nameGender, limit);

                return Json(nameList);
            }
            else if (nameGender == null && nameType != null)
            {
                nameList = await NameService.GetNameList(nameType, limit);

                return Json(nameList);
            }
            else
            {
                nameList = await NameService.GetNameList(nameType, nameGender, limit);

                return Json(nameList);
            }

        }
    }
}