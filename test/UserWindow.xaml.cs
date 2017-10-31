using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace test
{
    public partial class UserWindow : Window
    {
        Woker Me;
        Woker sub;
        string date; //переменные для этого окна   

        public UserWindow(Woker w)
        {
            InitializeComponent();
            Me = w;          
            this.Show();
            FIO.Content = Me.Name + " " + Me.Surname;
            TypeInfo.Content = Me.Type;
            if (Me.Shef != null)
                ShefFIO.Content = "Начальник: " + Me.Shef.Name + " " + Me.Shef.Surname;
            else ShefFIO.Content = "У вас нет начальника";
            Begin.Content = "В компании с " + Me.Begin.ToString("d");
            List<Woker> list=new List<Woker>();
             Me.RekSub(list);
            Subordinations.ItemsSource = list;
            SalaryView.Content ="Зарплата: "+ Me.ShowSalary(date);   
        } //открытие и заполнение формы

        private void Subordinations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Subordinations.SelectedIndex != -1)
            {
                sub = Subordinations.Items[Subordinations.SelectedIndex] as Woker;
                SFIO.Content = sub.Name + " " + sub.Surname;
                SSalaryView.Content = "Зарплата: " + sub.ShowSalary(date);
                SBegins.Content = "В кмпании с " + sub.Begin.ToString("d");
            }
        }     //выбор подчиненного и вывод информации о нем

        private void SetDate(object sender, RoutedEventArgs e)
        {
            date = CurDate.Text;
            SalaryView.Content = "Зарплата: " + Me.ShowSalary(date);
            if(sub!=null) SSalaryView.Content = "Зарплата: " + sub.ShowSalary(date);
        }   //выбор даты и обновление информации о зарплате

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new AuthorizationWindow();
            this.Close();
        }
    }
}
