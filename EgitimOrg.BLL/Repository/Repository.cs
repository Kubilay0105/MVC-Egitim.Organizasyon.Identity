﻿using EgitimOrg.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.BLL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private EduOrgContext _eduorgContext; //veritabanı
        private DbSet<T> _dbSet;   //tablo

        public Repository(EduOrgContext context)
        {
            if (context != null)
            {
                _eduorgContext = context;
                _dbSet = _eduorgContext.Set<T>();
            }
        }
        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetById(int Id)
        {
            return _dbSet.Find(Id);
        }
        public bool Add(T entity)
        {
            bool Sonuc = false;
            try
            {
                _dbSet.Add(entity);
                Sonuc = Convert.ToBoolean(_eduorgContext.SaveChanges());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                //throw new Exception("Kayıt yapılamadı!");
            }
            return Sonuc;
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            bool Sonuc = false;
            try
            {
                T deleted = _dbSet.Find(Id);
                
                _dbSet.Remove(deleted);
                Sonuc = Convert.ToBoolean(_eduorgContext.SaveChanges());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                //throw new Exception("Kayıt yapılamadı!");
            }
            return Sonuc;
        }

        public IList<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> sorgu = _dbSet;
            if (filter != null)
                sorgu = sorgu.Where(filter);
            if (orderby != null)
                sorgu = orderby(sorgu);
            foreach (Expression<Func<T, object>> tablo in includes)
            {
                sorgu = sorgu.Include(tablo);
            }
            return sorgu.ToList();
        }
        public bool Update(T entity)
        {
            bool Sonuc = false;
            try
            {
                _eduorgContext.Entry(entity).State = EntityState.Modified;
                Sonuc = Convert.ToBoolean(_eduorgContext.SaveChanges());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                //throw new Exception("Kayıt yapılamadı!");
            }
            return Sonuc;
        }

        public T Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> sorgu = _dbSet;
            if (filter != null)
                sorgu = sorgu.Where(filter);
            if (orderby != null)
                sorgu = orderby(sorgu);
            foreach (Expression<Func<T, object>> tablo in includes)
            {
                sorgu = sorgu.Include(tablo);
            }
            return sorgu.FirstOrDefault();
        }
    }
}
