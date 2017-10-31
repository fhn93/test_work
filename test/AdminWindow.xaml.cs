using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace test
{
    public partial class AdminWindow : Window
    {
        WokerContext DB;
        private Woker selectedWoker;
        Woker sub;
        Woker shef;
        string date;
        double summ;//переменные для этого окна
                                                       
        public AdminWindow()
        {    
            InitializeComponent();  
            DB = new WokerContext();                
            typeCombo.ItemsSource = new List<string> { "Employee", "Manamer", "Salesman" };
            RefreshAll();
            this.Show();
            DB.Accaunts.ToList();
        }

        private void Add_woker(object sender, RoutedEventArgs e)
        {
            Accaunt newAccaunt = new Accaunt { Login=loginBox.Text,Password=passwordBox.Text};  

            DB.Accaunts.Add(newAccaunt);                        
            DB.SaveChanges();                                                           
                                                             
            Woker newWoker = new Woker
            {
                Name = nameBox.Text,
                Surname = surnameBox.Text,
                Rate = System.Convert.ToInt32(rateBox.Text),
                Type = typeCombo.SelectedItem.ToString(),
                Begin = Convert.ToDateTime(beginBox.Text),
                AccauntId = newAccaunt.Id,
                Accaunt=newAccaunt  
            };         
            
            DB.Wokers.Add(newWoker);                          
            DB.SaveChanges();                                 
            RefreshAll();
        }    //добавление работника в базу данных

        private void RefreshContentAddTab()
        {                            
            rateBox.Clear();
            nameBox.Clear();
            surnameBox.Clear();
            passwordBox.Clear();
            rateBox.Clear();
            loginBox.Clear();
            beginBox.Clear();
            typeCombo.SelectedValue = -1;    
            WokerList.ItemsSource = DB.Wokers.ToList();
            AllWokers.ItemsSource = DB.Wokers.ToList();        
        }     //обновление формы бодавления сотрудника
                               
        private void RefreshAll()
        {
            RefreshContentSuborinteTab();
            RefreshContentAddTab();
            RefreshAllDataTab();
        }          //обновлене всех форм

        private void RefreshAllDataTab()
        { 
            SeeAllWokers.ItemsSource = DB.Wokers.ToList();
            subordinateList.ItemsSource = new List<Woker>();
            SFIO.Content = "";
            SSalaryView.Content = "";
            SBegins.Content = "";
            FIO.Content = "";
            Begins.Content = "";
            SalaryView.Content = "";
            ShefFIO.Content = "";
            log.Content = "";
            pas.Content = "";
        }     //обновление формы с данными на всех сотрудников

        private void RefreshContentSuborinteTab()
        {                                  
            AllWokers.ItemsSource = DB.Wokers.ToList();

            if (selectedWoker != null)
            {
                if (selectedWoker.Type != "Employee") notSubordinate.ItemsSource = DB.Wokers.Where(c => c.Shef == null && c != selectedWoker && !selectedWoker.IsSubShef(c)).ToList();
                else notSubordinate.ItemsSource = new List<Woker>();
                subordinateList.ItemsSource = DB.Wokers.Where(c => c.Shef == selectedWoker).ToList();
                if (selectedWoker.Shef == null)
                    ShefList.ItemsSource = DB.Wokers.Where(c=>c.Type!= "Employee" && !c.IsSubShef(selectedWoker) && c!=selectedWoker).ToList();
                else ShefList.ItemsSource = new List<Woker> { selectedWoker.Shef };
            }
            else
            {
                notSubordinate.ItemsSource = DB.Wokers.Where(c => c.Shef == null).ToList();
            }              
                     
        }         //обновление формы с натройки "подчиненности"
        
        private void AssSubordination(object sender, RoutedEventArgs e)
        {
            if (notSubordinate.SelectedIndex > -1)
            {
                Woker subordinateWoker = notSubordinate.Items[notSubordinate.SelectedIndex] as Woker;
                if (selectedWoker != null && subordinateWoker != null)
                {
                    subordinateWoker.Shef = selectedWoker;
                    DB.SaveChanges();
                    RefreshContentSuborinteTab();
                }
            }
        }  

        private void SetShef(object sender, RoutedEventArgs e)
        {   if (ShefList.SelectedIndex > -1)
            {
                Woker myShef = ShefList.Items[ShefList.SelectedIndex] as Woker;
                if (myShef != null && selectedWoker != null)
                    selectedWoker.Shef = myShef;
                DB.SaveChanges();
                RefreshContentSuborinteTab();
            }
        }           

        private void AllWokers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWoker = AllWokers.Items[AllWokers.SelectedIndex] as Woker;                               
            if (selectedWoker != null)                                               
            RefreshContentSuborinteTab();
        }    //выбор сотрудника

        private void SeeAllWokers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAllDataTab();
            Woker shef = new Woker();
            if(SeeAllWokers.SelectedIndex!=-1)
            {
                shef = SeeAllWokers.Items[SeeAllWokers.SelectedIndex] as Woker;
                Subordinations.ItemsSource = DB.Wokers.Where(c=>c.IsSubShef(shef)).ToList();
                FIO.Content = shef.ToString();
                Begins.Content ="В компании с: "+ shef.Begin.ToString("d");
                SalaryView.Content ="Зарплата: "+ shef.ShowSalary(date);
                if (shef.Shef != null) ShefFIO.Content = "Начальник: " + shef.Shef.Name + " " + shef.Surname;
                else ShefFIO.Content = "работает без начальника"; 
                log.Content = "Логин:  "+ shef.Accaunt.Login;
                pas.Content = "Пароль:  "+shef.Accaunt.Password;   
            }
        }  //выбор сотрудника

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Subordinations.SelectedIndex != -1 && Subordinations.SelectedIndex !=-1)
            {      
                sub = Subordinations.Items[Subordinations.SelectedIndex] as Woker;
                SFIO.Content = sub.ToString();
                SBegins.Content = "В компании с: "+sub.Begin;
                SSalaryView.Content = "Зарплата: "+sub.ShowSalary(date);  
            }
        }   //выбор подчиненного

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new AuthorizationWindow();
            this.Close();
        }

        private void SetDate(object sender, RoutedEventArgs e)
        {
            date = CurDate.Text;
            SalaryView.Content = "Зарплата: " + shef.ShowSalary(date);
            SSalaryView.Content = "Зарплата: " + sub.ShowSalary(date);
            summ = 0;
            foreach (Woker w in SeeAllWokers.ItemsSource)
            {
                summ += System.Convert.ToDouble(w.ShowSalary(date));
            }
            AllSalary.Content = "Затраты на зарплату составляют: " + summ;
        }
    }
}  