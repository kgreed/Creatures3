 
using System;
using System.Text;
using System.Windows.Forms;
using Creatures3.Module.BusinessObjects;
namespace Creatures.Module.Win
{
    public static class WinMigrationHelper
    {
        public static bool CheckMigrationVersionAndUpgradeIfNeeded( )
        {
            try
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using var db = new Creatures3DbContext(connectionString);
                if (!db.Database.Exists()) return true; // Let EF Create it

                var compatible = false;

                try
                {
                    compatible = db.Database.CompatibleWithModel(true);
                }
                catch (Exception )
                {
                    compatible = false;

                }
                if (compatible)
                {
                    return true;
                }
              
                return RunMigrations(db);
                   
                
            }
            catch (Exception ex)
            {
                var s = string.Format("Problem in MigrateIfNeeded /n/p" + ex);
                MessageBox.Show(s);
                return false;
            }
        }
        private static string AskForUpgradePassword(string upgradeDescription)
        {
            var msg = new StringBuilder();
            msg.AppendLine("Please say NO, exit the program and run the upgrade.");
            msg.AppendLine("Ask support if you don't have the upgrade shortcut on your desktop.");

            msg.AppendLine("Notes for support: The database structure has changed.");
            msg.AppendLine(upgradeDescription);
            msg.AppendLine("Continue if you are the developer (hit cat)");
            msg.AppendLine("");
            if (MessageBox.Show(msg.ToString(), "Upgrade database", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return null;
            }
            var pwd = "";
            ShowInputDialog("Version Change!", ref pwd);
            return pwd;

        }
        public static DialogResult ShowInputDialog(string caption, ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog,
                ClientSize = size,
                Text = caption
            };

            //inputBox.Text = "Name";
            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button
            {
                DialogResult = System.Windows.Forms.DialogResult.OK,
                Name = "okButton",
                Size = new System.Drawing.Size(75, 23),
                Text = "&OK",
                Location = new System.Drawing.Point(size.Width - 80 - 80, 39)
            };
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel,
                Name = "cancelButton",
                Size = new System.Drawing.Size(75, 23),
                Text = "&Cancel",
                Location = new System.Drawing.Point(size.Width - 80, 39)
            };
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }
        private static bool RunMigrations(Creatures3DbContext db)
        {
            var upgradeDescription = MigrationHelper.MigrationDescription(db);
            var pwd = AskForUpgradePassword(upgradeDescription);
            if (pwd != "cat")
            {
                return  false ;
            }
            try
            {
                //http://stackoverflow.com/questions/34699283/ef6-1-3-expects-createdon-field-in-migrationhistory-table-when-database-setiniti
                //MessageBox.Show(
                //    "You might need to the following SQL First /n/p " +
                //    "ALTER TABLE dbo.__MigrationHistory ADD CreatedOn DateTime Default GETDATE() /n/p" + "GO /n/p" +
                //    "UPDATE dbo.__MigrationHistory SET CreatedOn = GETDATE()");
                MigrationHelper.RunMigrations(db);
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return true;

            }
        }
    }
}