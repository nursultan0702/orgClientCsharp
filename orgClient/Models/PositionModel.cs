using orgClient.StructureService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orgClient.Models
{
    public class PositionModel
    {
        public List<position> PositionList { get; set; }
        public position position { get; set; }
        public position parentPosition { get; set; }
    }
}