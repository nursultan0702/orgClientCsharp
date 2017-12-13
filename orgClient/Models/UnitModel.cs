using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using orgClient.StructureService;

namespace orgClient.Models
{
    public class UnitModel
    {
        
        public int companyId { get; set; }
        public string description { get; set; }
        public int parentId { get; set; }
        public int status { get; set; }
        public int unitId { get; set; }
        public string unitName { get; set; }
        public List<unit> UnitList { get; set; }
       
    }
}