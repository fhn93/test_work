using System.Linq;
using System.Windows;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        WokerContext DB;
        public AuthorizationWindow()
        {
            InitializeComponent();
            DB = new WokerContext();
            DB.Wokers.ToList();
            this.Show();
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {
            Accaunt acc = new Accaunt { Login = Log.Text, Password=Pass.Password};
            acc = DB.Accaunts.FirstOrDefault(c => c.Login == acc.Login && c.Password == acc.Password);
            if (acc == null)
            {
                Pass.Password = "";
                Log.Text = "";
                System.Windows.MessageBox.Show("Не верный логин или пароль");
            }
            else     
            {                                     
                if (acc.Woker == null)
                {
                    new AdminWindow();
                    this.Close();
                }
                else
                {                                              
                    new UserWindow(acc.Woker);
                    this.Close();
                }
            }
        } //авторизация
    }
}
