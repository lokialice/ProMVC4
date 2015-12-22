using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnsentialTools.Models
{
    public class Product
    {
        #region[Properties]

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        #endregion
    }
}