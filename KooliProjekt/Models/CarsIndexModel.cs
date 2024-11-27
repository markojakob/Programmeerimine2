﻿using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class CarsIndexModel
    {
        public CarsSearch Search { get; set; }
        public PagedResult<Car> Data { get; set; }
    }
}
