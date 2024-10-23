using Newtonsoft.Json;
using ProjectObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SteamRedesign.LoginUI
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            PerformLogin();

        }
        private bool CheckLogin(string emailUser, string password)
        {

            try
            {
                // Đọc file JSON từ hệ thống
                string jsonFilePath = "appsettings.json";
                string jsonData = File.ReadAllText(jsonFilePath);

                // Chuyển đổi JSON thành danh sách đối tượng User
                List<AccountUser> users = JsonConvert.DeserializeObject<List<AccountUser>>(jsonData);

                return (users.Any(user => user.Email == emailUser && user.Password == password));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file JSON: " + ex.Message);
                return false;
            }
        }
        // enter button event  
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PerformLogin();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PerformLogin();
            }
        }
        private void PerformLogin()
        {
            string enteredUserEmail = Convert.ToString(emailU.email.Text.Trim());

            string enteredPassword = Convert.ToString(passU.passbox.Password.Trim());

            // Kiểm tra đăng nhập
            if (CheckLogin(enteredUserEmail, enteredPassword))
            {
                MessageBox.Show("Đăng nhập thành công!");
                // Chuyển đến màn hình tiếp theo hoặc thực hiện các hành động cần thiết
                MainWindow adminWindow = new MainWindow();
                adminWindow.Show();

                //đóng window chứa LoginPage
                Window parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    parentWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }
    }
}
