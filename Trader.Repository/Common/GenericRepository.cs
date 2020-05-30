﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Repository.Common
{
    public class GenericRepository<TElement, TKey> : IGenericRepository<TElement, TKey>
        where TElement : class
    {
        #region Private Definitions
        DbContext m_context;
        DbSet<TElement> m_table;
        #endregion

        public GenericRepository(DbContext context)
        {
            m_context = context;
            m_table = m_context.Set<TElement>();
        }

        public IEnumerable<TElement> GetAll()
        {
            return m_table;
        }
        public TElement Get(TKey key)
        {
            return m_table.Find(key);
        }

        public void AddOrUpdate(TElement item)
        {
            m_table.AddOrUpdate(item);
        }

        public void Remove(TKey key)
        {
            var target = Get(key);
            m_table.Remove(target);
        }

        public IEnumerable<TElement> Where(Expression<Func<TElement, bool>> predicate)
        {
            return m_table.Where(predicate);
        }

        public void Save()
        {
            m_context.SaveChanges();
        }
    }
}
