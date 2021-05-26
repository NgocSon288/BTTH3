using QRCode.Models;
using QRCode.Repository;
using QRCode.Repository.Interface;
using System;
using System.Collections.Generic;

namespace QRCode.Services
{
    public interface ICategoryService
    {
        bool InsertRange(List<Category> entities, bool isOveride = true);

        bool Insert(Category entity);

        List<Category> GetAll();

        Category GetByID(Guid id);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(Category entity);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryFilmRepository;

        public CategoryService()
        {
            this._categoryFilmRepository = new CategoryRepository();
        }

        public bool DeleteAll()
        {
            return _categoryFilmRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryFilmRepository.DeleteByID(id);
        }

        public List<Category> GetAll()
        {
            return _categoryFilmRepository.GetAll();
        }

        public Category GetByID(Guid id)
        {
            return _categoryFilmRepository.GetByID(id);
        }

        public bool Insert(Category entity)
        {
            return _categoryFilmRepository.Insert(entity);
        }

        public bool InsertRange(List<Category> entities, bool isOveride = true)
        {
            return _categoryFilmRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Category entity)
        {
            return _categoryFilmRepository.Update(entity);
        }
    }
}