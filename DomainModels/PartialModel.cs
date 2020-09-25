using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class PartialModel
    {
        public PartialModel(string ControlID, Dictionary<string, object> dicData)
        {
            this.ControlID = ControlID;
            this.dicData = dicData;
        }
        public string ControlID { get; set; }
        public Dictionary<string, object> dicData { get; set; }
    }
}
