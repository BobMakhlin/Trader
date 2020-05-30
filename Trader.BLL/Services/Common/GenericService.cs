using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trader.Repository.Common;

namespace Trader.BLL.Services.Common
{
    public class GenericService<TDbElement, TDtoElement, TKey>
        : IGenericService<TDbElement, TDtoElement, TKey>
        where TDbElement : class
        where TDtoElement : class
    {
        #region Private Definitions
        IGenericRepository<TDbElement, TKey> m_repository;
        IMapper m_mapper;
        #endregion

        public GenericService(IGenericRepository<TDbElement, TKey> repository)
        {
            m_repository = repository;
            m_mapper = GetMapper();
        }

        protected virtual IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<TDbElement, TDtoElement>();
                    ce.CreateMap<TDtoElement, TDbElement>();
                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }

        public IEnumerable<TDtoElement> GetAll()
        {
            var collection = m_repository.GetAll();
            var target = m_mapper.Map<IEnumerable<TDtoElement>>(collection);
            return target;
        }
        public TDtoElement Get(TKey key)
        {
            var item = m_repository.Get(key);
            var target = m_mapper.Map<TDtoElement>(item);
            return target;
        }

        public TDtoElement AddOrUpdate(TDtoElement item)
        {
            var target = m_mapper.Map<TDbElement>(item);
            m_repository.AddOrUpdate(target);
            m_repository.Save();
            return m_mapper.Map<TDtoElement>(target);
        }

        public void Remove(TKey key)
        {
            m_repository.Remove(key);
            m_repository.Save();
        }

        public IEnumerable<TDtoElement> Where(Expression<Func<TDtoElement, bool>> predicate)
        {
            var targetPredicate = m_mapper.Map<Expression<Func<TDbElement, bool>>>(predicate);
            var result = m_repository.Where(targetPredicate);
            return m_mapper.Map<IEnumerable<TDtoElement>>(result);
        }
    }
}
