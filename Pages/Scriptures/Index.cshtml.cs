using System;
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
        [BindProperty(SupportsGet = true)]
        public string SearchNote { get; set; }

        //Sorting

        public string ScriptureSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }



        public async Task OnGetAsync(string sortOrder)
        {
            
            var Scriptures1 = from m in _context.Scriptures select m;
            
            //search
            if (!string.IsNullOrEmpty(SearchString))
            {
                Scriptures1 = Scriptures1.Where(s => s.Scripture.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(SearchNote))
            {
                Scriptures1 = Scriptures1.Where(x => x.Note.Contains(SearchNote));
            }
            

            //sort

            
                ScriptureSort = String.IsNullOrEmpty(sortOrder) ? "Scripture_desc" : "";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";

               
                switch (sortOrder)
                {
                    case "Scripture_desc":
                        Scriptures1 = Scriptures1.OrderByDescending(s => s.Scripture);
                        break;
                    case "Date":
                        Scriptures1 = Scriptures1.OrderBy(s => s.DateEntered);
                        break;
                    case "date_desc":
                        Scriptures1 = Scriptures1.OrderByDescending(s => s.DateEntered);
                        break;
                    default:
                        Scriptures1 = Scriptures1.OrderBy(s => s.Scripture);
                        break;
                }

                Scripture = await Scriptures1.AsNoTracking().ToListAsync();
            
        } 
        
    }
}
