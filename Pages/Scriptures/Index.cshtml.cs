using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IList<Scriptures> Scripture { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IList<Scriptures> Note { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString2 { get; set; }
        public async Task OnGetAsync()
          {
            var Scriptures1 = from m in _context.Scriptures select m;
            var Notes = from m in _context.Scriptures select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                Scriptures1 = Scriptures1.Where(s => s.Scripture.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(SearchString2))
            {
                Notes = Notes.Where(s => s.Note.Contains(SearchString2));
            }
            Note = await Notes.ToListAsync();
            Scripture = await Scriptures1.ToListAsync();
        }
          
        
}
}
