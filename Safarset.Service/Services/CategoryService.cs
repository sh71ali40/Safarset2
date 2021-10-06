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
    public interface ICategoryService : IBaseService<Category>
    {

    }
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork,ILogger<Category> logger) : base(unitOfWork,logger)
        {
        }
    }
}
