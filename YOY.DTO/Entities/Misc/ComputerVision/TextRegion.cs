using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.ComputerVision
{
    public class TextRegion
    {
        public string BoundingBox { set; get; }
        public List<TextLine> Lines { set; get; }
    }
}
