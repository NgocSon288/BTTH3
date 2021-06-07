using QRCode.Models;
using QRCode.Repository;
using QRCode.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

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

        bool DeleteWithoutGuid(Product entity);

        bool Update(Product entity);

        bool UpdateWithoutGuid(Product entity);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            this._productRepository = new ProductRepository();
        }

        public bool DeleteAll()
        {
            return _productRepository.DeleteAll();
        }

        public bool DeleteByID(Guid id)
        {
            return _productRepository.DeleteByID(id);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetByID(Guid id)
        {
            return _productRepository.GetByID(id);
        }

        public bool Insert(Product entity)
        {
            return _productRepository.Insert(entity);
        }

        public bool InsertRange(List<Product> entities, bool isOveride = true)
        {
            return _productRepository.InsertRange(entities, isOveride);
        }

        public bool Update(Product entity)
        {
            return _productRepository.Update(entity);
        }

        public bool UpdateWithoutGuid(Product entity)
        {
            var list = _productRepository.GetAll();
            var itemDelete = list.FirstOrDefault(item => item.ID == entity.ID);
            list.Remove(itemDelete);
            list.Add(entity);

            return InsertRange(list);
        }

        public bool DeleteWithoutGuid(Product entity)
        {
            var list = _productRepository.GetAll();
            var itemDelete = list.FirstOrDefault(item => item.ID == entity.ID);
            list.Remove(itemDelete);

            DeleteAll();
            return InsertRange(list);
        }
    }
}