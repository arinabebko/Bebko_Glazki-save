using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Бебко_Глазки_save
{
    /// <summary>
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {

        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;

        int CountRecords;
        int CountPage;
        int CurrentPage = 0;




        public AgentsPage()
        {
            InitializeComponent();
            var currentAgents = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();
            AgentsListView.ItemsSource = currentAgents;

            //   var currentProductSale = BebkoГлазкиSaveEntities.GetContext().ProductSale.ToList();
            //AgentsListView.ItemsSource = currentProductSale;
            ComboType.SelectedIndex = 0;
            ComboSort.SelectedIndex = 0;
            UpdateServices();


        }

  

        private void UpdateServices()
        {
            var currentAgents = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();
            if (ComboType.SelectedIndex == 0)
            {

                currentAgents = currentAgents.ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {

                currentAgents = currentAgents.Where(p => Convert.ToString(p.AgentTypeID) == "1").ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {

                currentAgents = currentAgents.Where(p => Convert.ToString(p.AgentTypeID) == "2").ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {

                currentAgents = currentAgents.Where(p => Convert.ToString(p.AgentTypeID) == "3").ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {

                currentAgents = currentAgents.Where(p => Convert.ToString(p.AgentTypeID) == "4").ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {

                currentAgents = currentAgents.Where(p => Convert.ToString(p.AgentTypeID) == "5").ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {

                currentAgents = currentAgents.Where(p => Convert.ToString(p.AgentTypeID) == "6").ToList();
            }




            // Приводим текст поиска к нижнему регистру один раз
     
            string searchText = TBoxSearch.Text.ToLower().Replace("+", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            // Фильтруем список агентов по всем трем полям одновременно
            currentAgents = currentAgents
                .Where(p => p.Title.ToLower().Contains(searchText) ||
                            p.Phone.Replace("+", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Contains(searchText) ||
                            p.Email.ToLower().Contains(searchText))
                .ToList();

            // Обновляем источник данных для списка агентов
            AgentsListView.ItemsSource = currentAgents;






            if (ComboSort.SelectedIndex == 0)
            {
                currentAgents = currentAgents.ToList();
            }
            if (ComboSort.SelectedIndex == 1)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (ComboSort.SelectedIndex == 2)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboSort.SelectedIndex == 3)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }
            if (ComboSort.SelectedIndex == 4)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboSort.SelectedIndex == 5)
            {
                currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
            }
            if (ComboSort.SelectedIndex == 6)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();
            }

            AgentsListView.ItemsSource = currentAgents;
            TableList = currentAgents;
            ChangePage(0, 0);

           
        }













        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
       //     Manager.MainFrame.Navigate(new AddEditPage());
        //}

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

          

            UpdateServices();
        }




        

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           UpdateServices();

        }




    

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateServices();
           // ChangePage(0, 0);

        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }
            Boolean Ifupdate = true;
            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage-1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }


            }


            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);

                }

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();
 

                PageListBox.SelectedIndex = CurrentPage;
                AgentsListView.ItemsSource = CurrentPageList;
                AgentsListView.Items.Refresh();






            }
        }

        private void WrapPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString())-1);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            /*
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
                        BebkoГлазкиSaveEntities.GetContext().Agent.Remove(currentAgent);
                       // BebkoГлазкиSaveEntities.GetContext().ProductSale.Remove(currentProductSale);

                        BebkoГлазкиSaveEntities.GetContext().SaveChanges();

                        AgentsListView.ItemsSource = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();

                        AgentsListView.ItemsSource = CurrentPageList;
                        AgentsListView.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            
            */


        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
