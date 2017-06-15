using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SplitChat.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Path to directory in which application data is stored
        /// </summary>
        public string DataDir { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public App()
        {
            // Initialize object
            DataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SplitChat"
            );

            // Create application data directory (if one does not exist)
            if (!Directory.Exists(DataDir))
            {
                CreateDataDir();
            }
        }

        /// <summary>
        /// Creates the directory structure that the application uses to store data
        /// </summary>
        private void CreateDataDir()
        {
            // Copy app data dir structure from ./app/skel
            Core.Utility.Filesystem.Copy(Path.Combine(
                Environment.CurrentDirectory,
                "app" + Path.DirectorySeparatorChar + "skel"
            ), DataDir);
        }
    }
}
