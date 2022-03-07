using iFly.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace iFly.DAL.Database
{
    public class DatabaseSeeder
    {
        public static async Task Seed(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

                if (context.Database.EnsureCreated())
                {
                    string filepath = @"C:\Users\altin\Documents\git\DIANS\Homework 4\iFly\iFly.DAL\Database\flights.csv";
                    var readcsv = File.ReadAllText(filepath);
                    string[] csvfilerecord = readcsv.Split('\n');

                    foreach (var row in csvfilerecord)
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var cells = row.Split(',');
                            var flight = new Flight
                            {
                                From = cells[0], // number is in first cell
                                To = cells[1],   // cvv is in second cell
                                Date = DateTime.Parse(cells[2])
                            };
                            await context.AddAsync(flight);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}