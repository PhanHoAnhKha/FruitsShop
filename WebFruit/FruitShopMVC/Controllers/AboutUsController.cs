using Microsoft.AspNetCore.Mvc;

namespace FruitShopMVC.Controllers
{
	public class AboutUsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
