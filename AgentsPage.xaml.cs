﻿using System;
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


           
        }













        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

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
    }
}
