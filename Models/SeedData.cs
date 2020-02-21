using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyScriptureJournal.Data;
using System;
using System.Linq;

namespace MyScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions< MyScriptureJournalContext>>()))
            {
                // Look for any Scriptures.
                if (context. Scriptures.Any())
                {
                    return;   // DB has been seeded
                }

                context. Scriptures.AddRange(
                    new Scriptures
                    {
                        Scripture = "2 Nephi 9:29",
                        DateEntered = DateTime.Parse("2020-2-17"),
                        Note = "But to be learned is good if they hearken unto the counsels of God."
                    },

                    new Scriptures
                    {
                        Scripture = "Nephi1",
                        DateEntered = DateTime.Parse("2020-2-18"),
                        Note = "Text here"
                    },

                    new Scriptures
                    {
                        Scripture = "Nephi1",
                        DateEntered = DateTime.Parse("2020-2-18"),
                        Note = "Text here"
                    },

                    new Scriptures
                    {
                        Scripture = "Nephi1",
                        DateEntered = DateTime.Parse("2020-2-18"),
                        Note = "Text here"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
