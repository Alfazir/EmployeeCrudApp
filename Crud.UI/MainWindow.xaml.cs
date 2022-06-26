using Crud.Domain.Model;
using Crud.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace Crud.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeeCrudServices _services;
        Employee _employee;
        public MainWindow()
        {
            InitializeComponent();
            _services = new EmployeeCrudServices();
            _employee = new Employee();
            this.DataContext = _employee;

            ButtonAdd.Click += ButtonAdd_Click;
            ButtonList.Click += ButtonList_Click;
            DataGridEmployees.SelectionChanged += DataGridEmployee_SelectionChanged;
            ButtonDelete.Click += ButtonDelete_Click;
            ButtonSearch.Click+= ButtonSearch_Click;
            ButtonClear.Click += ButtonClear_ClicK;
        }

        private async  void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            
            //await Awaiters.DetachCurrentSyncContext();
            // await Dispatcher.InvokeAsync( (Action) (() => { 
            try
            {
                //_= await _services.AddEmployee(txtEmployeeName.Text, txtEmployeeSurname.Text);
                //var responceTask = Task.Run(async () =>await _services.AddEmployee(txtEmployeeName.Text, txtEmployeeSurname.Text));
                //  var responce = responceTask.Result;
                // await CreateEmployee(txtEmployeeName.Text, txtEmployeeSurname.Text);
                // var result = Task.Run(async () => await _services.AddEmployee(txtEmployeeName.Text, txtEmployeeSurname.Text)).Result;
                // await Dispatcher.Invoke(async() =>  CreateEmployee(txtEmployeeName.Text, txtEmployeeSurname.Text));
                // completedCallback?.Invoke();
                //InitializeIfNeededAsync().Wait();
          


               // CreateEmployee(txtEmployeeName.Text, txtEmployeeSurname.Text);
               
                await _services.AddEmployee(txtEmployeeName.Text, txtEmployeeSurname.Text, txtEmployeePatronymic.Text, EmployeeDate:txtEmployeeDate.Text, 
                    EmployeeDepartment:txtEmployeeDepartment.Text, EmployeeEmail:txtEmployeeEmail.Text,EmployeePhone:txtEmployeePhone.Text, 
                    EmployeePosition:txtEmployeePosition.Text);

                throw new Exception("Новый сотрудник добавлен!");
                txtEmployeeID.Clear();
                txtEmployeeName.Clear();
                txtEmployeeSurname.Clear();
                txtEmployeeID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                await ListEmployees();
                // txtEmployeeID.Clear();
                //  txtEmployeeName.Clear();
                // txtEmployeeSurname.Clear();
                // txtEmployeeID.Focus();
                //   Thread.ResetAbort();
            }
                
            //}));
        }
        #region save as
        /* private Task CreateEmployee(string txtEmployeeName, string txtEmployeeSurname)
         {
              Awa

              _services.AddEmployee(txtEmployeeName, txtEmployeeSurname);



         }*/

        /* private async Task InitializeIfNeededAsync() => await Task.Delay(1);
         public struct DetachSynchronizationContextAwaiter : ICriticalNotifyCompletion
         {
             /// <summary>
             /// Returns true if a current synchronization context is null.
             /// It means that the continuation is called only when a current context
             /// is presented.
             /// </summary>
             public bool IsCompleted => SynchronizationContext.Current == null;

             public void OnCompleted(Action continuation)
             {
                 ThreadPool.QueueUserWorkItem(state => continuation());
             }

             public void UnsafeOnCompleted(Action continuation)
             {
                 ThreadPool.UnsafeQueueUserWorkItem(state => continuation(), null);
             }

             public void GetResult() { }

             public DetachSynchronizationContextAwaiter GetAwaiter() => this;
         }

         public static class Awaiters
         {
             public static DetachSynchronizationContextAwaiter DetachCurrentSyncContext()
             {
                 return new DetachSynchronizationContextAwaiter();
             }
         }*/
        #endregion
        private async Task ListEmployees ()
        {
            var employeesList = await _services.ListEmployees();
            DataGridEmployees.ItemsSource= employeesList.ToList();
           
        }

        private async void ButtonList_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                await ListEmployees();
            }
            catch (Exception ex)
            {
             MessageBox.Show (ex.Message);
            }
        }

        private void DataGridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var activelist = (Employee)DataGridEmployees.CurrentItem;

                if (activelist != null)
                {
                    txtEmployeeID.Text= activelist.Id.ToString();
                    txtEmployeeName.Text = activelist.EmployeeName;
                    txtEmployeePatronymic.Text = activelist.EmployeePatronymic;
                    txtEmployeeSurname.Text = activelist.EmployeeSurname;
                    txtEmployeePosition.Text = activelist.EmployeePosition;
                    txtEmployeeDate.Text = activelist.EmployeeDate.ToString();
                    txtEmployeeDepartment.Text = activelist.EmployeeDepartment;
                    txtEmployeeEmail.Text = activelist.EmployeeEmail;
                    txtEmployeePhone.Text = activelist.EmployeePhone;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
            if (txtEmployeeID.Text!= String.Empty && txtEmployeeName.Text != String.Empty )
             await _services.DeleteEmployee(int.Parse(txtEmployeeID.Text));

                throw new Exception($"{txtEmployeePosition.Text} {txtEmployeeSurname.Text} {txtEmployeeName.Text} {txtEmployeePatronymic.Text} удалён!") ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message) ;

            }
            finally
            {
                await ListEmployees();
            }

        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            var SearchName = await _services.SearchEmployeeByName(txtEmployeeName.Text);
            DataGridEmployees.ItemsSource= SearchName.ToList();
        }

        private  void ButtonClear_ClicK (object sender, RoutedEventArgs e)
        {
            DataGridEmployees.ItemsSource= null;
            txtEmployeeID.Clear();
            txtEmployeeName.Clear();
            txtEmployeeSurname.Clear();
            txtEmployeeID.Focus();
            txtEmployeeDepartment.Clear();
            txtEmployeeEmail.Clear();
            txtEmployeePatronymic.Clear();
            txtEmployeePhone.Clear();
            txtEmployeePosition.Clear();
            txtEmployeeDate.Text= null;
            /*if (txtEmployeeDate?.SelectedDate == new DateTime())
            {
                txtEmployeeDate.Text = null;
                txtEmployeeDate.DisplayDate = DateTime.Now;
            }*/

        }
        public async void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text != string.Empty && txtEmployeeName.Text != string.Empty)
                {
                    await _services.UpdateEmployee(int.Parse(txtEmployeeID.Text), txtEmployeeName.Text);
                    throw new Exception("Данные сотрудника обновлены");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ListEmployees();
            }
        }

        

    }
}
