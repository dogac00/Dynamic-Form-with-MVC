using DynamicFormsApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicFormsApplication.Views.Shared.Components.BootstrapPopup
{
    public class BootstrapPopupViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public BootstrapPopupViewComponent(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var forms = await _context.Forms.ToListAsync();
            return View(forms);
        }
    }
}
