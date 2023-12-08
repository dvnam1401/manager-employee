using ControlzEx.Theming;
using ModernWpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VBSQLHelper;


namespace crudWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SQLHelper.SERVER_NAME = "VAN-NAM\\VANNAM";
            SQLHelper.DATABASE = "db_QLNV_WPF";
            SQLHelper.USERNAME_DB = "sa";
            SQLHelper.PASSWORD_DB = "123456789";
            SQLHelper.ConnectString();
            base.OnStartup(e);
        }

    }
}
