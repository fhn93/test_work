using System;
using System.Collections.Generic;

namespace test
{
    public class Woker
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Type { get; set; }

        public int Rate { get; set; }

        public DateTime Begin { get; set; }

        public int AccauntId { get; set; }
        public Accaunt Accaunt { get; set; }

        public IList<Woker> Subordinate { get; set; }

        public Woker Shef { get; set; }

        public bool IsSubShef(Woker he)
        {              
            if (Shef == he) return true;
            if (Shef == null) return false;
            return (Shef.IsSubShef(he));       
        }   //проверка на наличие подчиненности любого уровня

        public override string ToString()
        {     
            return (Name + " " + Surname+" "+Type);
        }

        public void RekSub(List<Woker> list)
        {
            bool flag;                  
            if(Subordinate!=null)
            foreach (Woker w in Subordinate)
            {
                if (w != null)
                {
                    w.RekSub(list);
                        flag = true;
                            foreach (Woker a in list) if (w == a) flag = false;
                        if (flag) 
                        list.Add(w);
                }
            }              
        }    //поиск подчиненных всех уровней

        public string ShowSalary(string _date)
        {
            if (_date == "" || _date == null) return ("не задана дата");
            DateTime date;
            try
            {
                date = System.Convert.ToDateTime(_date);
            }
            catch
            {
                return ("Дата задана не верно");
            }

            double result = Calcsalary(date);
            return (result.ToString());
        }     //запуск функции расчета зарплаты

        private double Calcsalary(DateTime _data)
        {
            if (_data < Begin) return 0;
            double yy =Math.Truncate((_data-Begin).TotalDays/365);
            double result=0;
            switch (Type)
            {
                case "Employee":
                    yy = Math.Min(yy*0.03,0.3);
                    return(Rate * (1 + yy));
                case "Manamer":
                    yy = Math.Min(yy * 0.05, 0.4);
                    result = Rate * (1 + yy);
                    if(Subordinate!=null)
                        foreach (Woker w in Subordinate) result += 0.005 * w.Calcsalary(_data);
                    return result;
                case "Salesman":
                    yy = Math.Min(yy * 0.01, 0.35);
                    result = Rate * (1 + yy);
                    List<Woker> list = new List<Woker>();
                    RekSub(list);
                    if(list!=null)   
                        foreach (Woker w in list) result += 0.01 * w.Calcsalary(_data);
                    return result;
                default:
                    return result;
            }         
        }    //расчет зарплаты
    }
}
