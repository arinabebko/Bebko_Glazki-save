using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

namespace Бебко_Глазки_save
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {



        private Agent currentAgent = new Agent();
        private int a;


        //  private Service _currentServise = new Service();
        public AddEditPage(Agent SelectedAgent)
        {

            InitializeComponent();
            if (SelectedAgent != null)
            {
                currentAgent = SelectedAgent;
                a = 1;
            }
            else { a = 0; }
            // var currentAgent = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();
            DataContext = currentAgent;

            LoadAgentTypes();

        }
     

        private void LoadAgentTypes()
        {
            var agentTypes = BebkoГлазкиSaveEntities.GetContext().AgentType.ToList();
            //ComboAgentType.Items.Clear();
            ComboAgentType.ItemsSource = agentTypes;
        }


        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if(myOpenFileDialog.ShowDialog()==true)
            {
                currentAgent.Logo = myOpenFileDialog.FileName;

                LogoImage.Source=new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }
        
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");





            if (ComboAgentType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            /*
            else
            {
                string selectedType = ComboAgentType.SelectedItem.ToString();
                if (selectedType == "ООО")
                {
                    currentAgent.AgentTypeID = 1; // Устанавливаем ID для "ООО"
                }
                else if (selectedType == "ИП")
                {
                    currentAgent.AgentTypeID = 2; // Устанавливаем ID для "ИП"
                }
            }

            */


            if (string.IsNullOrWhiteSpace(currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if (currentAgent.Priority <=0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            if (string.IsNullOrWhiteSpace(currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            if (string.IsNullOrWhiteSpace(currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");

            else
            {
                string ph = currentAgent.Phone.Replace("(","").Replace(")","").Replace("-","").Replace("+","").Replace(" ", "");
                if (((ph[1]=='9'|| ph[1]=='4'||  ph[1]=='8') && ph.Length != 11) ||  (ph.Length != 11 && ph[1]=='3'))
                {
                    errors.AppendLine("Укажите правильно телефон агента");

                }
            }
            if (string.IsNullOrWhiteSpace(currentAgent.Email))
                errors.AppendLine("Укажите email агента");
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }



            if (currentAgent.ID == 0)
            {
                //вот сюда

                BebkoГлазкиSaveEntities.GetContext().Agent.Add(currentAgent);
            }
            try
            {
                BebkoГлазкиSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("сохранено");
                Manager.MainFrame.GoBack();
                Manager.MainFrame.Navigate(new AgentsPage());
            }

            catch (DbUpdateConcurrencyException ex)
            {
                MessageBox.Show("Конфликт при обновлении данных: " + ex.Message);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Ошибка обновления базы данных: " + ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
         

            // catch (Exception ex)
            //  {
            //      MessageBox.Show(ex.Message.ToString());
            //  }








        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {


            var currentAgent = (sender as Button).DataContext as Agent;

            var currentProductSale = BebkoГлазкиSaveEntities.GetContext().ProductSale.ToList();
            currentProductSale = currentProductSale.Where(p => p.AgentID == currentAgent.ID).ToList();

            if (currentProductSale.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существует запись на этого агента");
            else
            {




                if (MessageBox.Show("Вы точно хотите выполнить удаление? ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                       // var currentproductSales = BebkoГлазкиSaveEntities.GetContext().ProductSale.Where(ps => ps.AgentID == currentAgent.ID).ToList();

                        //if (productSales.Any())
                        //{
                            // Если есть информация о реализации продукции, запрещаем удаление
                         //   MessageBox.Show("Удаление невозможно, так как у агента есть информация о реализации продукции.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                           // return; // Выходим из метода
                        //}




                        BebkoГлазкиSaveEntities.GetContext().Agent.Remove(currentAgent);
                        // BebkoГлазкиSaveEntities.GetContext().ProductSale.Remove(currentProductSale);

                        BebkoГлазкиSaveEntities.GetContext().SaveChanges();


                        //AgentsListView.ItemsSource = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();

                     //   AgentsListView.ItemsSource = CurrentPageList;
                      //  AgentsListView.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    Manager.MainFrame.Navigate(new AgentsPage());
                }
            }



            
        }

        private void ComboAgentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnRealiz_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new History (currentAgent));
        
        }
    }
}
