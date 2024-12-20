﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Бебко_Глазки_save
{
    /// <summary>
    /// Логика взаимодействия для Prior.xaml
    /// </summary>
    public partial class Prior : Window
    {

        private List<Agent> _currentAgents;
        public Prior(List<Agent> agents)
        {
            InitializeComponent();
          //  var currentAgents_5 = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();
            _currentAgents = agents;
            int maxPriority = _currentAgents.Max(a => a.Priority);
            TBChangePrior.Text = maxPriority.ToString();
        }

        private void BtnChangePrior_Click(object sender, RoutedEventArgs e)
        {
            /*
            var currentAgents=AgentsListView.SelectedItems;
            int p;
            p = 0;
            for (int i =0; i< AgentsListView.SelectedItems.Count; i++) ;
            {
                //нахождение максимального приоритета
            }
            
            */



            if (int.TryParse(TBChangePrior.Text, out int newPriority)  && newPriority >= 0)
            {
                // using (var context_8 = BebkoГлазкиSaveEntities.GetContext())
                // {
                var currentAgents = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();
                // Обновляем приоритет для каждого агента
                foreach (var agent in _currentAgents)
                {
                    agent.Priority = newPriority; // Устанавливаем новый приоритет
                }

                // Сохраняем изменения в базе данных
                //  currentAgents.SaveChanges();
                //  }

                // Сохраняем изменения в базе данных
                //context.SaveChanges();
                BebkoГлазкиSaveEntities.GetContext().SaveChanges();
                //{ Binding Agent.Priority = TBChangePrior.Text};
                MessageBox.Show("Приоритеты обновлены!");
                this.Close();

                //var currentAgents = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();

            }
            else
            {
                if (newPriority < 0)
                {

                    MessageBox.Show("приоритет не может быть отрицательным числом");
                }
                else
                {
                
                        MessageBox.Show("приоритеты оставлены без изменений.");
                    this.Close();
                }
            }
           

        }

        private void TBChangePrior_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if (Convert.ToInt32( TBChangePrior.Text) != 0)
       
        }
    }
}
