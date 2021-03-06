﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using Creatures3.Module.Win;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Utils;
using DevExpress.XtraEditors;
namespace Creatures3.Win
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            WindowsFormsSettings.LoadApplicationSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ToolTipController.DefaultController.ToolTipType = ToolTipType.SuperTip;
            if (Tracing.GetFileLocationFromSettings() == FileLocation.CurrentUserApplicationDataFolder)
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            Tracing.Initialize();
            var winApplication = new Creatures3WindowsFormsApplication();
            winApplication.GetSecurityStrategy().RegisterEFAdapterProviders();
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
                winApplication.ConnectionString =
                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString =
 ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if (Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
#endif
            try
            {
                if (!WinMigrationHelper.CheckMigrationVersionAndUpgradeIfNeeded())
                {
                }
                else
                {
                    winApplication.Setup();
                    winApplication.Start();
                }

                //winApplication.Setup();
                //winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.StopSplash();
                winApplication.HandleException(e);
            }
        }
    }
}