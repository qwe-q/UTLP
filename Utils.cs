using System;
using System.IO;

namespace UTLP
{
    public class Utils
    {
        /*
         * Gets the path of an individual log file given
         * its time and its subdirectory name (i.e. trips, fuel)
         */
        public static string GetFilePath(string Subdirectory, DateTime Time) =>
            Path.Combine(
                GetBaseDirectory(), 
                Subdirectory + Time.ToString("yyyy-M-d HH:mm:ss") + ".json");

        public static string GetBaseDirectory() => 
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    }
}