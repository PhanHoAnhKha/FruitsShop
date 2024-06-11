using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FruitShopMVC.Models;
namespace FruitShopMVC.Data
{
	public class PetAuthDbContext : IdentityDbContext
	{
		public PetAuthDbContext(DbContextOptions<PetAuthDbContext> options) : base(options) { }
	}
}
