﻿using E_Commerce.Repository;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        List<Menu> GetByRestaurantId(int restuarantId);
    }
}
