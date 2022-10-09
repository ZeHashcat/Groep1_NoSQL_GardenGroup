using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class WindowSize
    {
        private string aspectRatio;
        private double resolutionX;
        private double resolutionY;

        public WindowSize(string aspectRatio, double resolutionX, double resolutionY)
        {
            this.aspectRatio = aspectRatio;
            this.resolutionX = resolutionX;
            this.resolutionY = resolutionY;
        }

        public string AspectRatio { get { return aspectRatio; } set { aspectRatio = value; } }
        public double ResolutionX { get { return resolutionX; } set { resolutionX = value; } }
        public double ResolutionY { get { return resolutionY; } set { resolutionY = value; } }
    }
}
