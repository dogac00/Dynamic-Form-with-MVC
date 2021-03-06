﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DynamicFormsApplication.Data;
using DynamicFormsApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DynamicFormsApplication.Controllers
{
    [Authorize]
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public FormsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Forms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Forms.ToListAsync());
        }

        // GET: Forms/ViewForm/5
        [HttpGet]
        public async Task<IActionResult> ViewForm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms.Include("Fields")
                .FirstOrDefaultAsync(m => m.Id == id);

            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Form form, 
            List<string> fieldNames, List<bool> fieldIsRequireds, List<string> fieldDataTypes)
        {
            List<Field> fields = new List<Field>();

            for (int i = 0; i < fieldNames.Count; ++i)
            {
                Field tempField = new Field()
                {
                    Required = fieldIsRequireds[i],
                    Name = fieldNames[i],
                    DataType = fieldDataTypes[i]
                };

                fields.Add(tempField);
            }

            form.CreatedAt = DateTime.Now;
            form.CreatedBy = User.Identity.Name;
            form.Fields = fields;

            if (ModelState.IsValid)
            {
                _context.Forms.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return Redirect("Index");
        }

        // GET: Forms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(int id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }
    }
}
