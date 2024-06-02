using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebFruit.Data
{
    public class PetAuthDbContext : IdentityDbContext
    {
        public PetAuthDbContext(DbContextOptions<PetAuthDbContext> options) : base(options) { }
       
    }
}
