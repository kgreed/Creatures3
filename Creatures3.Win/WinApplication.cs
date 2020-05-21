using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using Creatures3.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.Persistent.Base;
namespace Creatures3.Win
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Win.WinApplication._members
    public partial class Creatures3WindowsFormsApplication : WinApplication
    {
        public Creatures3WindowsFormsApplication()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            if (args.Connection != null)
                args.ObjectSpaceProviders.Add(new EFObjectSpaceProvider(typeof(Creatures3DbContext), TypesInfo, null,
                    (DbConnection) args.Connection));
            else
                args.ObjectSpaceProviders.Add(new EFObjectSpaceProvider(typeof(Creatures3DbContext), TypesInfo, null,
                    args.ConnectionString));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        private void Creatures3WindowsFormsApplication_CustomizeLanguagesList(object sender,
            CustomizeLanguagesListEventArgs e)
        {
            var userLanguageName = Thread.CurrentThread.CurrentUICulture.Name;
            if (userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1)
                e.Languages.Add(userLanguageName);
        }

        private void Creatures3WindowsFormsApplication_DatabaseVersionMismatch(object sender,
            DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if (Debugger.IsAttached)
            {
                e.Updater.Update();
                e.Handled = true;
            }
            else
            {
                var message = "The application cannot connect to the specified database, " +
                              "because the database doesn't exist, its version is older " +
                              "than that of the application or its schema does not match " +
                              "the ORM data model structure. To avoid this error, use one " +
                              "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";
                if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                throw new InvalidOperationException(message);
            }
#endif
        }

        #region Default XAF configuration options (https: //www.devexpress.com/kb=T501418)

        static Creatures3WindowsFormsApplication()
        {
            PasswordCryptographer.EnableRfc2898 = true;
            PasswordCryptographer.SupportLegacySha512 = false;
            BaseObjectSpace.ThrowExceptionForNotRegisteredEntityType = true;
            ImageLoader.Instance.UseSvgImages = true;
            DetailView.UseAsyncLoading = true;
            SecurityStrategy.EnableSecurityForActions = true;
        }

        private void InitializeDefaults()
        {
            LinkNewObjectToParentImmediately = false;
            OptimizedControllersCreation = true;
            UseLightStyle = true;
            SplashScreen = new DXSplashScreen(typeof(XafSplashScreen), new DefaultOverlayFormOptions());
            ExecuteStartupLogicBeforeClosingLogonWindow = true;
        }

        #endregion
    }
}