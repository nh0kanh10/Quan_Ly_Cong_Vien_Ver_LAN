using FlaUI.Core;
using FlaUI.UIA3;
using System;
using System.IO;

namespace DaiNam.UITests.Core
{
    public static class AppManager
    {
        public static Application App { get; private set; }
        public static UIA3Automation Automation { get; private set; }
        public static FlaUI.Core.AutomationElements.Window MainWindow { get; private set; }

        public static void LaunchApp()
        {
            if (App != null) return; // App already running

            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DaiNamApp.exe");
            
            // For NUnit runner, the BaseDirectory is often where the test DLL is, 
            // but the actual DaiNamApp.exe might be in GUI/bin/Debug. 
            // We adjust the path if necessary.
            if (!File.Exists(exePath))
            {
                exePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\GUI\bin\Debug\DaiNamApp.exe"));
            }

            if (!File.Exists(exePath))
                throw new FileNotFoundException($"Cannot find DaiNamApp.exe at {exePath}");

            App = Application.Launch(exePath);
            Automation = new UIA3Automation();
            
            // Wait for main window
            MainWindow = App.GetMainWindow(Automation, TimeSpan.FromSeconds(10));
            if (MainWindow == null)
            {
                throw new Exception("Main window failed to load.");
            }
        }

        public static void CloseApp()
        {
            App?.Close();
            App?.Dispose();
            Automation?.Dispose();

            App = null;
            Automation = null;
            MainWindow = null;
        }
    }
}
