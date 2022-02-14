using MockPracUOW.Server.Data;
using MockPracUOW.Server.IRepository;
using MockPracUOW.Server.Models;
using MockPracUOW.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MockPracUOW.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Shared.Domain.Task> _tasks;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Shared.Domain.Task> Tasks
            => _tasks ??= new GenericRepository<Shared.Domain.Task>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async System.Threading.Tasks.Task Save(HttpContext httpContext)
        {
            //To be implemented
            //string user = "System";

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((Shared.Domain.Task)entry.Entity).CreatedDate = DateTime.Now;
                    ((Shared.Domain.Task)entry.Entity).CreatedBy = user.UserName;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}