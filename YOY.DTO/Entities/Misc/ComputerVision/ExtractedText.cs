using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.ComputerVision
{
    public class ExtractedText
    {
        public string Language { set; get; }
        public string TextAngle { set; get; }
        public string Orientation { set; get; }
        public List<TextRegion> Regions { set; get; }
    }
}
