﻿using Food.Application.Admin.Dtos.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.FoodMenus
{
    public class FoodCategoriaSave
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? nombreImg { get; set; }
        public decimal Precio { get; set; }
        public CategoriaSaveDto? Categoria { get; set; }
    }
}
