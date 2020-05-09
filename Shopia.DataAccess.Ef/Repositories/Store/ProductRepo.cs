﻿using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class ProductRepo : EfGenericRepo<Product>,IProductRepo
    {
        readonly AppDbContext _appContext;
        public ProductRepo(AppDbContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }

    }
}
