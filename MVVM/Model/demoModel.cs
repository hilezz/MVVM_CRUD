using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model
    {
    public class BaseNotify : INotifyPropertyChanged
            {
                public event PropertyChangedEventHandler PropertyChanged;
                public void OnPropertyChanged<T>(Expression<Func<T>> property)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(FindPropertyName(property)));
                }

                public static string FindPropertyName<T>(Expression<Func<T>> property)
                {
                    var propertyInfo = (property.Body as MemberExpression).Member as System.Reflection.PropertyInfo;
                    if (propertyInfo == null)
                        throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
                    return propertyInfo.Name;
                }

            }
    public class demoModel : BaseNotify
    {
        private int id;
        public int ID
        {
            get { return id; }
            set 
            { 
                id = value;
                OnPropertyChanged(() => ID);
            }
        }
        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set {
                firstname = value;
                OnPropertyChanged(() => Firstname);
            }
        }

        private string secondname;
        public string Secondname
        {
            get { return secondname; }
            set {
                secondname = value;
                OnPropertyChanged(() => Secondname);
            }

        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set {
                lastname = value;
                OnPropertyChanged(() => Lastname);
            }
        }

        private string age;
        public string Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged(() => Age);
            }
        }

        //SEARCHDATA-----------------------------------------------------------------------------------------------
        private string s_firstname;

        public string S_Firstname
        {
            get { return s_firstname; }
            set
            {
                s_firstname = value;
                OnPropertyChanged(() => S_Firstname);
            }
        }

        private string s_secondname;
        public string S_Secondname
        {
            get { return s_secondname; }
            set
            {
                s_secondname = value;
                OnPropertyChanged(() => S_Secondname);
            }

        }

        private string s_lastname;
        public string S_Lastname
        {
            get { return s_lastname; }
            set
            {
                s_lastname = value;
                OnPropertyChanged(() => S_Lastname);
            }
        }

        //private string sgen;
        //public string Sgen
        //{
        //    get { return sgen; }
        //    set
        //    {
        //        sgen = value;
        //        OnPropertyChanged(()=> Sgen);

        //    }
        //}

        private string s_age;
        public string S_Age
        {
            get { return s_age; }
            set
            {
                s_age = value;
                OnPropertyChanged(() => S_Age);
            }
        }

        
        //---------------------------------------------------------------------------------------------------------

        private string sdata;
        public string Sdata
        {
            get { return sdata; }
            set
            {
                sdata = value;
                OnPropertyChanged(() => Sdata);
            }
        }

       //COMBOBOX1

        private Gender gen;

        public Gender Gen
        {
            get { return gen; }
            set
            {
                gen = value;
                OnPropertyChanged(()=> Gen);
            }
        }

       //COMBOBOX2

        private SGender s_gen;
        public SGender GenSR
        {
            get { return s_gen; }
            set
            {
                s_gen = value;
                OnPropertyChanged(() => GenSR);
            }
        }

        //private DataTable sresult;
        //public DataTable Sresult
        //{
        //    get { return sresult; }
        //    set
        //    {
        //        sresult = value;
        //        OnPropertyChanged(() => Sresult);
        //    }
        //}
        
    }
    public class Gender : BaseNotify
    {
       
        private string genderVal;
        public string GenderVal
        {
            get { return genderVal; }
            set
            {
                genderVal = value;
                OnPropertyChanged(() => GenderVal);
            }
        }
    }
    public class SGender : BaseNotify
    {

        private string _genderVal;
        public string GenderValSR
        {
            get { return _genderVal; }
            set
            {
                _genderVal = value;
                OnPropertyChanged(() => GenderValSR);
            }
        }
    }

}
