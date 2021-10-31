using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Safarset.Datalayer.Context;
using Safarset.Datalayer.Context.Entities;

namespace Safarset.Service.Services
{
    public interface ICityService : IBaseService<City>
    {

    }
    public class CityService : BaseService<City>,ICityService
    {
        public CityService(IUnitOfWork unitOfWork, ILogger<City> logger) : base(unitOfWork, logger)
        {
        }
    }
}
