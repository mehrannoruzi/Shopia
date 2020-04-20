﻿using Shopia.Domain;
using Elk.EntityFrameworkCore;

namespace Shopia.DataAccess.Ef
{
    public class StoreRepo : EfGenericRepo<Store>
    {
        public StoreRepo(AppDbContext appContext) : base(appContext)
        { }


    }
}
