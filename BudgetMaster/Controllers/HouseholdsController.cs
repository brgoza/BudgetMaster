using Microsoft.AspNetCore.Mvc;

namespace BudgetMaster.Controllers
{
    public class HouseholdsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
