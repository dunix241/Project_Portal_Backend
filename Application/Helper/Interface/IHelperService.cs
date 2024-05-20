using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper.Interface
{
    public interface IHelperService
    {
        Task<long> GenerateIRN(DataContext dataContext);
    }
}
