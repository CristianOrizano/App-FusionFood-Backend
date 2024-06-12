using Food.Application.Admin.Dtos.FoodMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.DetalleOrdens
{
    public class DetalleOrdenListDto
    {
        public FoodMenuDto? FoodMenu { get; set; }
        public int Cantidad { get; set; }
    }
}
