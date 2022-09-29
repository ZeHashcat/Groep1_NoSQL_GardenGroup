using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GardenGroupModel
{
    public class Settings
    {
        private const double resolution_X_Min = 1280;
        private const double resolution_Y_Min = 720;
        private const string section = "Graphical";
        private WindowSize windowSize;
        private WindowSize[] predefinedWindowSizes;
        private bool fullscreen = false;
        private IniFile settingsIni = new IniFile("Settings");
        

        //EXPLANATION: Resolution that is given is the primary screen resolution, which can't be accessed from here afaik.
        //NOTE: This class maintains it's own .ini file, so only one instance should be made and passed around the different windows. <<---`-`/\/\`-`(IMPORTANT)
        public Settings(double resolution_X, double resolution_Y)
        {
            windowSize = new WindowSize("4:3", resolution_X, resolution_Y);
            FloodWindowSizes();
            GetSetSettings();
            GetSetFullscreen();
        }

        public WindowSize WindowSize { get { return windowSize; } }

        //EXPLANATION: Go through each option and get values, or set default values when values are missing or incorrect.
        private void GetSetSettings()
        {
            GetSetResolution();
        }

        //EXPLANATION: Decides which resolution to use, .ini or primary screen resolution. Also sets default .ini resolution values to auto when nothing is found.
        private void GetSetResolution()
        {
            string resolutionX = "Resolution_X";
            string resolutionY = "Resolution_Y";
            string automaticMode = "AUTO";

            //EXPLANATION: Check if keys exist and if they are both set to auto.
            if (settingsIni.KeyExists(resolutionX, section) && settingsIni.KeyExists(resolutionY, section))
            {
                //EXPLANATION: Get resolution from .ini
                string iniResolutionX = settingsIni.Read(resolutionX, section);
                string iniResolutionY = settingsIni.Read(resolutionY, section);

                //EXPLANATION: Check if .ini values are numeric and within primary screen boundaries and above minimum.
                if (iniResolutionX.All(char.IsDigit) && iniResolutionY.All(char.IsDigit) && double.Parse(iniResolutionX) <= windowSize.ResolutionX && double.Parse(iniResolutionY) <= windowSize.ResolutionY && double.Parse(iniResolutionX) >= resolution_X_Min && double.Parse(iniResolutionY) >= resolution_Y_Min)
                {
                    //EXPLANATION: Set resolution from .ini
                    windowSize.ResolutionX = double.Parse(iniResolutionX);
                    windowSize.ResolutionY = double.Parse(iniResolutionY);
                    return;
                }
                else if (settingsIni.Read(resolutionX, section) == automaticMode && settingsIni.Read(resolutionY, section) == automaticMode)
                {
                    return;
                }
                //EXPLANATION: If above if statements fail, the only case should be that one value == "auto".
            }
            //EXPLANATION: Make both values auto.
            settingsIni.Write(resolutionX, automaticMode, section);
            settingsIni.Write(resolutionY, automaticMode, section);
        }

        //EXPLANATION: Decides whether or not to use fullscreen from the .ini or default value. if no key found or invalid it will set one.
        private void GetSetFullscreen()
        {
            string fullscreen = Convert.ToString(this.fullscreen);
            string key = "Fullscreen";
            
            if (settingsIni.KeyExists(fullscreen, section))
            {
                //EXPLANATION: Tries to parse before setting this.fullscreen to the .ini value, if it can't this.fullscreen will default to false.
                bool validValue = false;
                bool.TryParse(settingsIni.Read(fullscreen, section), out validValue);
                if(validValue == true)
                {
                    this.fullscreen = bool.Parse(fullscreen);
                    return;
                }
            }
            settingsIni.Write(key, fullscreen, section);
        }

        //EXPLANATION: Decides whether or not to use fullscreen from the .ini or default value. if no key found or invalid it will set one.
        private void GetSetAspectRatio()
        {
            string key = "Aspect_Ratio";
            string value = settingsIni.Read(key, section);

            if (settingsIni.KeyExists(key, section))
            {
                foreach(WindowSize windowSize in predefinedWindowSizes)
                {
                    if (windowSize.AspectRatio == value)
                    {
                        this.windowSize = windowSize;
                        return;
                    }
                }
            }
            settingsIni.Write(key, value, section);
        }

        //EXPLANATION: returns a list with all the valid window sizes.
        private WindowSize[] FloodWindowSizes()
        {
            return new WindowSize[10];
        }
    }
}
