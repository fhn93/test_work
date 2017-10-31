using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
       
namespace test
{                    
    public partial class App : Application
    {
        public App()   //при запуске ели нет БД оздаем ее и добавляем аккант админа
        {
            Accaunt a = new Accaunt { Login = "Admin", Password = "Adminpassword" };
            this.InitializeComponent();  

            using (var db = new WokerContext())      
            {
                db.Database.Migrate();
                if(db.Accaunts.Count()==0)
                {
                    db.Accaunts.Add(a);
                    db.SaveChanges();
                }
            }
        }
    }
}
