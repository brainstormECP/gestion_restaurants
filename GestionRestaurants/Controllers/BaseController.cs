using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Controllers
{
    public class BaseController:Controller
    {
        protected DbContext _db;

        public BaseController(DbContext context)
        {
            _db = context;
        }
    }
}
