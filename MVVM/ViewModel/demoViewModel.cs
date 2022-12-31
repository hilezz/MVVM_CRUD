using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using MVVM.Model;
using MVVM.Commands;
using System.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace MVVM.ViewModel
{
    internal class demoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertChanged(string PropertName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertName));
            }
        }
        //Connectionstring-----------------------------------------------------------------------------

        string connectionString = @"Data Source=DESKTOP-HJJQAOU;Initial Catalog=DEMO;Integrated Security=True";


        public demoViewModel()
        {

            demoModel = new demoModel()
            {

            };

            Genders = new List<Gender>();
            Genders.Add(new Gender() { GenderVal = "Male" });
            Genders.Add(new Gender() { GenderVal = "Female" });
            Genders.Add(new Gender() { GenderVal = "Others" });

            S_Genders = new List<SGender>();
            S_Genders.Add(new SGender() { GenderValSR = "Male" });
            S_Genders.Add(new SGender() { GenderValSR = "Female" });
            S_Genders.Add(new SGender() { GenderValSR = "Others" });


        }
        //COMBOBOX1
        private List<Gender> genders;
        public List<Gender> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                NotifyPropertChanged("Genders");
            }
        }
        //COMBOBOX2
        private List<SGender> S_genders;
        public List<SGender> S_Genders
        {
            get { return S_genders; }
            set
            {
                S_genders = value;
                NotifyPropertChanged("S_Genders");
            }
        }

        private demoModel d_model;
        public demoModel demoModel
        {
            get { return d_model; }
            set
            {
                d_model = value;
                NotifyPropertChanged("demoModel");
            }
        }
        

        private ObservableCollection<demoModel> demo;

        public ObservableCollection<demoModel> Demo
        {
            get
            {
                return demo;
            }
            set
            {
                demo = value;

                NotifyPropertChanged("Demo");

            }
        }

        //------------------------------------------------------------------------------------
        private ICommand _submitcmd;
        public ICommand Submitcommand
        {
            get
            {
                return _submitcmd ?? (_submitcmd = new RelayCommand(x => {
                    SubmitExecute(x);
                }, param => { return CanSubmitExecute(null); } 
                
                ));
                //if (_submitcmd == null)
                //{
                //    _submitcmd = new Relaycommand(SubmitExecute, CanSubmitExecute);
                //}
                //return _submitcmd;
            }
        }

        private ICommand _doubleClickCmd;

        public ICommand DoubleClickCmd
        {
            get
            {
                return _doubleClickCmd ?? (_doubleClickCmd = new RelayCommand(x =>
                {
                    EditList(x);
                }));

                //if (_doubleClickCmd == null)
                //{
                //    _doubleClickCmd = new Relaycommand(EditList);
                //}
                //return _doubleClickCmd;
            }
        }

        private void EditList(object parameter)
        {
            if (parameter is demoModel para)
            {
                
                demoModel.ID = para.ID;
                demoModel.Firstname = para.S_Firstname;
                demoModel.Secondname = para.S_Secondname;
                demoModel.Lastname = para.S_Lastname;
                demoModel.Age = para.S_Age.ToString();
                demoModel.Gen = new Gender() { GenderVal = para.GenSR.GenderValSR };


            }

        }


        private void SubmitExecute(object parameter)
        {
            
            
            if(Demo == null)
                Demo = new ObservableCollection<demoModel>();
            SqlConnection con = new SqlConnection(connectionString);

            if (demoModel.ID==0)
            {

            Demo.Add(new demoModel());
                {

                    
                    SqlDataAdapter adapter = new SqlDataAdapter("UserInsert", con);

                    try
                    {
                        con.Open();
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar, (100)).Value = !string.IsNullOrEmpty(demoModel.Firstname) ? (object)demoModel.Firstname : DBNull.Value;
                        adapter.SelectCommand.Parameters.Add("@Secondname", SqlDbType.VarChar, (100)).Value = !string.IsNullOrEmpty(demoModel.Secondname) ? (object)demoModel.Secondname : DBNull.Value;
                        adapter.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar, (100)).Value = !string.IsNullOrWhiteSpace(demoModel.Lastname) ? (object)demoModel.Lastname : DBNull.Value;
                        adapter.SelectCommand.Parameters.Add("@Gender", SqlDbType.VarChar, (50)).Value = !string.IsNullOrWhiteSpace(demoModel?.Gen?.GenderVal) ? (object)demoModel.Gen.GenderVal : DBNull.Value;
                        adapter.SelectCommand.Parameters.Add("@Age", SqlDbType.Int).Value = !string.IsNullOrEmpty(demoModel.Age) ? (object)demoModel.Age : DBNull.Value;
                        adapter.SelectCommand.ExecuteNonQuery();
                        MessageBox.Show("values Inserted");

                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Values not Inserted");
                    }
                    finally
                    {
                        con.Close();

                    }

                }
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter("UpdateData", con);

                try
                {
                    con.Open();
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(demoModel.ID);
                    adapter.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar, (100)).Value = !string.IsNullOrEmpty(demoModel.Firstname) ? (object)demoModel.Firstname : DBNull.Value;
                    adapter.SelectCommand.Parameters.Add("@Secondname", SqlDbType.VarChar, (100)).Value = !string.IsNullOrEmpty(demoModel.Secondname) ? (object)demoModel.Secondname : DBNull.Value;
                    adapter.SelectCommand.Parameters.Add("@Lastname", SqlDbType.VarChar, (100)).Value = !string.IsNullOrWhiteSpace(demoModel.Lastname) ? (object)demoModel.Lastname : DBNull.Value;
                    adapter.SelectCommand.Parameters.Add("@Gender", SqlDbType.VarChar, (50)).Value = !string.IsNullOrWhiteSpace(demoModel?.Gen?.GenderVal) ? (object)demoModel.Gen.GenderVal : DBNull.Value;
                    adapter.SelectCommand.Parameters.Add("@Age", SqlDbType.Int).Value = !string.IsNullOrEmpty(demoModel.Age) ? (object)demoModel.Age : DBNull.Value;
                    adapter.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("values Updated");

                }
                catch (SqlException)
                {
                    MessageBox.Show("Values not Inserted");
                }
                finally
                {
                    con.Close();

                }

            }

        }
        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }

        public void Clear()
        {
            demoModel.Firstname=String.Empty;
            demoModel.Secondname = String.Empty;
            demoModel.Lastname = String.Empty;
            demoModel.Age = String.Empty;
            demoModel.Sdata = String.Empty;
            demoModel.ID = 0;
            

        }
        private ICommand cleargrid;

        public ICommand Cleargrid
        {
            get
            {
                return cleargrid ?? (cleargrid = new RelayCommand(x =>
                {
                    ClearGrid();
                }));
                //if (cleargrid == null)
                //{
                //    cleargrid = new Relaycommand(ClearGrid);
                //}
                //return cleargrid;
            }
        }
        public void ClearGrid()
        {
            demoModel.Sdata = String.Empty;
            demoModel.S_Firstname = String.Empty;
            demoModel.S_Secondname = String.Empty;
            demoModel.S_Lastname = String.Empty;
            demoModel.S_Age = String.Empty;
            demoModel.ID = 0;
            demo.Clear();
        }

        private ICommand clearcmd;
        public ICommand Clearcommand
        {
            get
            {

                return clearcmd ?? (clearcmd = new RelayCommand(x =>
                {
                    Clear();
                }));
                //return clearcmd ??(clearcmd = new RelayCommand(x=>
                //{
                //    Clear();
                //}  ));



                //if (clearcmd == null)
                //{
                //    clearcmd = new Relaycommand(Clear);
                //}
                //return clearcmd;
            }
        }

        //------------------------------------------------------------------
        private ICommand serchdata;
        public ICommand Searchcommand
        {
            get
            {
                return serchdata ?? (serchdata = new RelayCommand(x =>
                {
                    SearchData(x);
                }));
                //if (serchdata == null)
                //{

                //    serchdata = new Relaycommand(SearchData);
                //}
                //return serchdata;
            }
        }
        public void SearchData(object parameter)
        {
            if (Demo == null)
                Demo = new ObservableCollection<demoModel>();


            SqlConnection con = new SqlConnection(connectionString);

            if (demoModel.Sdata != "")
            {
                SqlCommand sqlCommand = new SqlCommand("UserView", con);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                con.Open();
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.Add("@Firstname", SqlDbType.VarChar, (100)).Value = !string.IsNullOrWhiteSpace(demoModel.Sdata) ? (object)demoModel.Sdata : DBNull.Value;
                adapter.SelectCommand.Parameters.Add("@Gender", SqlDbType.VarChar, (50)).Value = !string.IsNullOrWhiteSpace(demoModel?.GenSR?.GenderValSR) ? (object)demoModel.GenSR.GenderValSR : DBNull.Value;
                adapter.SelectCommand.ExecuteNonQuery();
                adapter.Fill(dataTable);
                adapter.Fill(dataSet);

                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        demoModel.ID = Convert.ToInt32(dataTable.Rows[i]["ID"].ToString());
                        demoModel.S_Firstname = dataTable.Rows[i]["Firstname"].ToString();
                        demoModel.S_Secondname = dataTable.Rows[i]["Secondname"].ToString();
                        demoModel.S_Lastname = dataTable.Rows[i]["Lastname"].ToString();
                        demoModel.S_Age = dataTable.Rows[i]["Age"].ToString();
                        demoModel.GenSR = new SGender()
                        {
                            GenderValSR = dataTable.Rows[i]["Gender"].ToString()
                        };

                        Demo.Add(new demoModel()
                        {
                            ID = demoModel.ID,
                            S_Firstname = demoModel.S_Firstname,
                            S_Secondname = demoModel.S_Secondname,
                            S_Lastname = demoModel.S_Lastname,
                            GenSR = new SGender() { GenderValSR = demoModel.GenSR.GenderValSR },
                            S_Age = demoModel.S_Age,
                        });
                    }
                }

            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("DT_Data", con);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                con.Open();
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.ExecuteNonQuery();
                adapter.Fill(dataTable);
                adapter.Fill(dataSet);


                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        demoModel.ID = Convert.ToInt32(dataTable.Rows[i]["Id"].ToString());
                        demoModel.S_Firstname = dataTable.Rows[i]["FirstName"].ToString();
                        demoModel.S_Secondname = dataTable.Rows[i]["SecondName"].ToString();
                        demoModel.S_Lastname = dataTable.Rows[i]["LastName"].ToString();
                        demoModel.S_Age = dataTable.Rows[i]["Age"].ToString();
                        demoModel.GenSR = new SGender()
                        {
                            GenderValSR = dataTable.Rows[i]["Gender"].ToString()
                        };

                        Demo.Add(new demoModel()
                        {
                            ID = demoModel.ID,
                            S_Firstname = demoModel.S_Firstname,
                            S_Secondname = demoModel.S_Secondname,
                            S_Lastname = demoModel.S_Lastname,
                            GenSR = new SGender() { GenderValSR = demoModel.GenSR.GenderValSR },
                            S_Age = demoModel.S_Age,
                        });
                    }
                }
            }
        }


    }


}
