using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace GardenGroupLogic
{
    public class IniFileLogic
    {
        private string name;
        private string file;

        //EXPLANATION: Not quite sure how these work, it seems they lead to methods within .dll's.
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string section, string key, string standard, StringBuilder stringBuilder, int size, string filePath);

        //EXPLANATION: Name is mostly used when the key doesn't have a section.
        //EXPLANATION: FileInfo allows for creation, opening, moving, deletion and copying of files, there it is given to Read and Write function.
        public IniFileLogic(string name)
        {
            this.name = name;
            file = new FileInfo($"{name}.ini").FullName;
        }

        //EXPLANATION: Uses a stringbuilder with a 255 char limit to return a string
        public string Read(string key, string section = null)
        {
            StringBuilder stringBuilder = new StringBuilder(255);
            GetPrivateProfileString(section ?? name, key, "", stringBuilder, 255, file);
            return stringBuilder.ToString();
        }

        //EXPLANATION: Uses the external .dll method to write a key, section and value to a file.
        public void Write(string key, string value, string section = null)
        {
            WritePrivateProfileString(section ?? name, key, value, file);
        }

        //EXPLANATION: Deletes a key from a section.
        public void DeleteKey(string key, string section = null)
        {
            Write(key, null, section ?? name);
        }

        //EXPLANATION: Deletes a section.
        public void DeleteSection(string section = null)
        {
            Write(null, null, section ?? name);
        }

        //EXPLANATION: if a key-value is longer the 0 chars this returns true.
        public bool KeyExists(string key, string section = null)
        {
            return Read(key, section ?? name).Length > 0;
        }
    }
}
