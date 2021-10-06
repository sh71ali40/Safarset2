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
    public interface ISlideService : IBaseService<Slide>
    {
        
    }
    public class SlideService : BaseService<Slide>, ISlideService
    {
        public SlideService(IUnitOfWork unitOfWork, ILogger<Slide> logger) : base(unitOfWork, logger)
        {
        }


    }
}
