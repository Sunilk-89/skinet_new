using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int _maxPageSize = 50;

        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        public int PageSize {
             get => _pageSize; 
             set => _pageSize =(value > _maxPageSize) ? _maxPageSize : value; 
             
             }

             public int? BrandId { get; set; }
             public int? TypeId { get; set; }

             public string Sort { get; set; }

             private string _serach;

             public string Search {
                  get => _serach; 
                  set => _serach = value.ToLower(); 
                  }
    }
}