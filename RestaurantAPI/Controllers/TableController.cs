using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    public class TableController : BaseApiClass
    {
        private readonly ITableRepository tableRepository;

        public TableController(ITableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        //[HttpPost]
        //public ActionResult createResrevatoinTable(TableDto table)
        //{
        //    if(!tableRepository.createReservationTable(table.Id))
        //        return BadRequest();

        //}
    }
}
