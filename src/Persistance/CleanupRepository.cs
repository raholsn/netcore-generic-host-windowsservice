using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Persistance
{
    public class CleanupRepository : ICleanupRepository
    {
        private readonly DataContext _context;
        public CleanupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CleanUp(CleanUp cleanup)
        {
            try
            {
                await _context.AddAsync(cleanup);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(ex.Message, EventLogEntryType.Information, 101, 1);
                }
            }
            
        }
    }
}
