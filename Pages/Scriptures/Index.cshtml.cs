﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        // search
        public IList<Scriptures> Scripture { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Note { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchNote { get; set; }

        //Sorting

        public string ScriptureSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }



        public async Task OnGetAsync(string sortOrder)
        {
            IQueryable<string> noteQuery = from m in _context.Scriptures
                                           orderby m.Note
                                           select m.Note;
            var Scriptures1 = from m in _context.Scriptures select m;
            //search
            if (!string.IsNullOrEmpty(SearchString))
            {
                Scriptures1 = Scriptures1.Where(s => s.Scripture.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(SearchNote))
            {
                Scriptures1 = Scriptures1.Where(x => x.Note == SearchNote);
            }
            Note = new SelectList(await noteQuery.Distinct().ToListAsync());
            Scripture = await Scriptures1.ToListAsync();


            //sort

            {
                ScriptureSort = String.IsNullOrEmpty(sortOrder) ? "Scripture_desc" : "";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";

                IQueryable<Scriptures> sortVar = from s in _context.Scriptures
                                                 select s;

                switch (sortOrder)
                {
                    case "Scripture_desc":
                        sortVar = sortVar.OrderByDescending(s => s.Scripture);
                        break;
                    case "Date":
                        sortVar = sortVar.OrderBy(s => s.DateEntered);
                        break;
                    case "date_desc":
                        sortVar = sortVar.OrderByDescending(s => s.DateEntered);
                        break;
                    default:
                        sortVar = sortVar.OrderBy(s => s.Scripture);
                        break;
                }

                Scripture = await sortVar.AsNoTracking().ToListAsync();
            }
        } 
        
    }
}
