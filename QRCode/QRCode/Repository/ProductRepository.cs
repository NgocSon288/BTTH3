﻿using QRCode.Models;
using QRCode.Repository.Interface;

namespace QRCode.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
    }
}