using Application.Helper.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class HelperService : IHelperService
    {
        public async Task<long> GenerateIRN(DataContext _context)
        {
            var latestStudent = await _context.Students.OrderByDescending(x => x.IRN).FirstOrDefaultAsync();
            long latestIrnOrder = 0;
            long latestYearMonth = 0;
            if (latestStudent != null)
            {
                latestYearMonth = latestStudent.IRN / 100000;
                latestIrnOrder = latestStudent.IRN % (latestYearMonth * 100000);
            }
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var currentYearMonth = (currentYear * 100) + currentMonth;

            if (currentYearMonth == latestYearMonth)
            {
                latestIrnOrder++;
            }
            else
            {
                latestYearMonth = currentYearMonth;
            }

            long irn = (latestYearMonth * 100000) + latestIrnOrder;
            return irn;
        }
    }
}
