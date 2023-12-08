using Dapper.Contrib.Extensions;
using Microsoft.Win32;
using ModernWpf;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VBSQLHelper;

namespace crudWPF
{   
    public partial class MainWindow : Window
    {
        public string uri_avatar = AppDomain.CurrentDomain.BaseDirectory + @"\img\no_avatar.jpg";
        public string uri_avatar_default = AppDomain.CurrentDomain.BaseDirectory + @"\img\no_avatar.jpg";
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            tbtn_theme.Toggled += Tbtn_theme_Toggled;
        }

        private void Tbtn_theme_Toggled(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                ThemeManager.Current.AccentColor = Colors.Goldenrod;
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                ThemeManager.Current.AccentColor = Colors.Red;

            }
        }

        [Table("employee")]
        class Employee {
            [ExplicitKey]
            public string id { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public bool gender { get; set; }
            public DateTime birthday { get; set; }
            public string address { get; set; }
            [Write(false)]
            public string birthday_text { get; set; }
            [Write(false)]
            public string gender_text { get; set; }
            public string avatar { get; set; }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        public void RefreshData() {
            var data = SQLHelper.ExecQueryData<Employee>("SELECT avatar, id, firstname, lastname, gender, birthday, address, FORMAT(birthday, 'dd/MM/yyyy') AS birthday_text, IIF(gender = 1, 'Male', N'Female') AS gender_text FROM Employee;");
            gridData.ItemsSource = data;
        }

        private void gridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var employeeSelected = (Employee)gridData.SelectedItem;
                if (employeeSelected != null) {
                    txtID.Text = employeeSelected.id;
                    txtFirstName.Text = employeeSelected.firstname;
                    txtLastName.Text = employeeSelected.lastname;
                    txtAddress.Text = employeeSelected.address;
                    txtBirthday.SelectedDate = employeeSelected.birthday;
                    rdoMale.IsChecked = employeeSelected.gender == true;
                    rdoFemale.IsChecked = employeeSelected.gender == false;
                    if (string.IsNullOrEmpty(employeeSelected.avatar)) {
                        picAvatar.ImageSource = new BitmapImage(new Uri(uri_avatar_default));
                        return;
                    }
                    picAvatar.ImageSource = new BitmapImage(new Uri(employeeSelected.avatar));

                }
               
            }
            catch { }
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {           
            if (string.IsNullOrEmpty(txtID.Text)) {
                var dlg2 = await new ContentDialog()
                {
                    Title = "Thông báo",
                    Content = $"Bạn chưa chọn Employee nào để xóa?",
                    CloseButtonText = "Đóng",                   
                    IsSecondaryButtonEnabled = false,
                    
                    
                }.ShowAsync();
                return;
            }
          
            var dlg = await new ContentDialog()
            {
                Title = "Thông báo",
                Content = $"Bạn có chắc chắn Xóa nhân viên: {txtFirstName.Text} {txtLastName.Text}?",
                PrimaryButtonText = "Xóa",             
                SecondaryButtonText = "Không",
                
            }.ShowAsync();

            if (dlg == ContentDialogResult.Primary) {
                SQLHelper.ExecQueryNonData("delete from employee where id=@id", new { id = txtID.Text });
                RefreshData();
            }

        
          
        }

      
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur =
               new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 35;
            win.Effect = objBlur;
        }
       
        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            uri_avatar = uri_avatar_default;
            picAvatar.ImageSource = new BitmapImage(new Uri(uri_avatar_default));
            txtID.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            var employee = new Employee();
            employee.id = txtID.Text;
            employee.firstname = txtFirstName.Text;
            employee.lastname = txtLastName.Text;
            employee.address = txtAddress.Text;
            employee.gender = rdoMale.IsChecked ?? false;
            employee.birthday = txtBirthday.SelectedDate ?? DateTime.Now;
            employee.avatar = uri_avatar;

            SQLHelper.Insert(employee);
            RefreshData();
            btnAdd_Click(null, null);
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee();
            employee.id = txtID.Text;
            employee.firstname = txtFirstName.Text;
            employee.lastname = txtLastName.Text;
            employee.address = txtAddress.Text;
            employee.gender = rdoMale.IsChecked ?? false;
            employee.birthday = txtBirthday.SelectedDate ?? DateTime.Now;
            employee.avatar = uri_avatar;
            SQLHelper.Update(employee);
            RefreshData();
            var dlg2 = await new ContentDialog()
            {
                Title = "Thông báo",
                Content = $"Cập nhật thông tin nhân viên thành công!",
                CloseButtonText = "Đóng",
                IsSecondaryButtonEnabled = false,


            }.ShowAsync();
        }

       

        private void picAvatar_MouseDown(object sender, RoutedEventArgs e)
        {
            var opendialog = new OpenFileDialog();
            opendialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            if (opendialog.ShowDialog() == true)
            {  
               picAvatar.ImageSource = new BitmapImage(new Uri(opendialog.FileName));
                uri_avatar = opendialog.FileName;
            }
        }
    }
}
