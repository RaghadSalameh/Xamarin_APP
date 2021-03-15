using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_App.Models
{
    public class TappedPosition
    {
        public int accuracy { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool fallback_boundary { get; set; }
    }

}
