using QRCode.Models;
using QRCode.Repository;
using QRCode.Repository.Interface;
using System;
using System.Collections.Generic;

namespace QRCode.Services
{
    public interface IProductService
    {
        bool InsertRange(List<Product> entities, bool isOveride = true);

        bool Insert(Product entity);

        List<Product> GetAll();

        Product GetByID(Guid id);

        bool DeleteAll();

        bool DeleteByID(Guid id);

        bool Update(Product entity);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _categoryFilmRepository;

        public ProductService()
        {
            this._categoryFilmRepository = new ProductRepository();
        }

        public bool DeleteAll()
        {
            return _categoryFilmRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _categoryFilmRepository.DeleteByID(id);
        }

        public List<Product> GetAll()
        {
            return _categoryFilmRepository.GetAll();
        }

        public Product GetByID(Guid id)
        {
            return _categoryFilmRepository.GetByID(id);
        }

        public bool Insert(Product entity)
        {
            return _categoryFilmRepository.Insert(entity);
        }

        public bool InsertRange(List<Product> entities, bool isOveride = true)
        {
            return _categoryFilmRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Product entity)
        {
            return _categoryFilmRepository.Update(entity);
        }
    }
}