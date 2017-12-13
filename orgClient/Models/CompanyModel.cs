using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using orgClient.StructureService;

namespace orgClient.Models
{
    public class CompanyModel
    {
        public int companyId { get; set; }
        public string description { get; set; }
        public int parentId { get; set; }
        public bool status { get; set; }
        public List<String> CompanyNameList { get; set; }
    }
}