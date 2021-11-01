using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Safarset.Service.Services;

namespace Safarset2.Class
{
   public partial class CrudComponent<TEntity, TViewModel , TKey, TIService> : ComponentBase
       where TIService : IBaseService<TEntity>
       where TEntity : class
       where TViewModel : class
    {
        #region fields
        [Inject] protected TIService Service { get; set; }
        [Inject] protected  IMapper Mapper{get;set;}
        [Inject] private ILogger<TEntity> Logger { get; set; }
        [Inject] private IHttpContextAccessor HttpContextAccessor{ get; set; }
        #endregion

        #region ctor
        //public CrudComponent(TIService service,IMapper mapper)
        //{
        //    Mapper = mapper;
        //    Service = service;
        //}
        #endregion


        public virtual async Task<bool> Insert(TViewModel entityViewModel)
        {
            var entity = Mapper.Map<TEntity>(entityViewModel);
            if (await Service.InsertAsync(entity, true))
            {
                //for return value of key
                var keys = Service.KeyNameAndValue(entity);
                var stringKeys = string.Join(",", keys.Values);
                // var firstKey =  keys.FirstOrDefault().Value.ToString();
                var userId = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                Logger.LogInformation( "Table: "+typeof(TEntity).Name + ", InsertItemsWithIds: "+stringKeys+",userId: "+ userId);
                return true;
            }
            return false;
        }

        public virtual async Task<bool> Update(TViewModel entityViewModel, TKey id)
        {
            var editedModel = await Service.FindAsync(id);
            var entity = Mapper.Map(entityViewModel,editedModel);
            if (await Service.UpdateAsync(entity, true))
                return true;
            return false;
        }

        public virtual async Task<bool> Delete(TKey key)
        {
            var result = await Service.DeleteAsync(true, key);
            return result;
        }
        public virtual IEnumerable<TViewModel> GetAll()
        {
            var result =  Service.GetAllAsQueryable();
            var mappedResult= Mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(result);
            return mappedResult;
        }

        public virtual async Task<TViewModel> FindAsync(TKey key)
        {
            var obj = await Service.FindAsync(key);
            var result = Mapper.Map<TViewModel>(obj);
            return result;
        }
    }
}
