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
                        Scripture = "1 Nephi 3:7",
                        DateEntered = DateTime.Parse("2020-2-21"),
                        Note = "I awill go and do the things which the Lord hath commanded,"
                    },

                    new Scriptures
                    {
                        Scripture = "Matthew 6:21",
                        DateEntered = DateTime.Parse("2020-2-19"),
                        Note = "For where your treasure is, there will your heart be also."
                    },

                    new Scriptures
                    {
                        Scripture = "Matthew 5:6",
                        DateEntered = DateTime.Parse("2020-2-18"),
                        Note = "Let your light so shine before men, that they may see your good works, and glorify your Father which is in heaven."
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
