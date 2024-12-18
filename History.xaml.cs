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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Бебко_Глазки_save
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
     
        private Agent currentAgent = new Agent();
        public History(Agent SelectedAgent)
        {
            InitializeComponent();


            if (SelectedAgent != null)
            {
                currentAgent = SelectedAgent;
                DataContext = currentAgent; // Устанавливаем контекст данных для отображения информации о текущем агенте

                // Получаем продажи для выбранного агента
                var currentProductSales = BebkoГлазкиSaveEntities.GetContext().ProductSale
                    .Where(ps => ps.AgentID == currentAgent.ID) // Предполагается, что у ProductSale есть поле AgentID
                    .ToList();

                // Устанавливаем источник данных для списка продаж
                LVHistory.ItemsSource = currentProductSales;
            }
            else
            {
                // Если агент не выбран, можно установить пустой источник или обработать это как-то иначе
                LVHistory.ItemsSource = new List<ProductSale>(); // Или null, в зависимости от ваших требований
            }




            var currentAgents = BebkoГлазкиSaveEntities.GetContext().Agent.ToList();

            DataContext = SelectedAgent;
           // LoadProductSale();
            LoadProductName();
      

            this.DataContext = currentAgents;
        }
        /*
    private void LoadProductSale()
    {
        var productSale = BebkoГлазкиSaveEntities.GetContext().ProductSale.ToList();


      //  LVHistory.ItemsSource = productSale;
        //LVHistory.DisplayMemberPath = "ProductID";
     //   LVHistory.DisplayMemberPath = "Product.Title";


    }
         */
        private void LoadProductName()
    {
        var ProductName= BebkoГлазкиSaveEntities.GetContext().Product.ToList();
        //ComboAgentType.Items.Clear();
        ComboBoxProduct.ItemsSource = ProductName;
        ComboBoxProduct.DisplayMemberPath = "Title";
       // ProdData.Text= "ProductionPersonCount";

       // ComboBoxProduct.SelectionChanged += ComboBoxProduct_SelectionChanged;
    }

        /*
    private void ComboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Получаем выбранный продукт
        var selectedProduct = ComboBoxProduct.SelectedItem as Product;

        if (selectedProduct != null)
        {
            // Обновляем TextBox значением ProductionPersonCount
            ProdCount.Text = selectedProduct.ProductionPersonCount.ToString();
        }
    }
   */
        private void ProdCount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LoadProductSalesForCurrentAgent()
        {
            var currentProductSales = BebkoГлазкиSaveEntities.GetContext().ProductSale
                .Where(ps => ps.AgentID == currentAgent.ID) // Предполагается, что у ProductSale есть поле AgentID
                .ToList();

            // Устанавливаем источник данных для списка продаж
            LVHistory.ItemsSource = currentProductSales;
        }



        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // LVHistory.SelectedItems.delete();
            //   var currentProductSale = (sender as Button).DataContext as Agent;
            var selectedItems = LVHistory.SelectedItems.Cast<ProductSale>().ToList();
            // var currentProductSale = BebkoГлазкиSaveEntities.GetContext().ProductSale.ToList();


            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите хотя бы один элемент для удаления.");
                return;
            }
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление? ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var context = BebkoГлазкиSaveEntities.GetContext();

                        // Удаляем каждый выбранный элемент из контекста
                        foreach (var item in selectedItems)
                        {
                            context.ProductSale.Remove(item);
                        }

                        // Сохраняем изменения в базе данных
                        context.SaveChanges();
                        //BebkoГлазкиSaveEntities.GetContext().ProductSale.Remove(currentProductSale);
                        // BebkoГлазкиSaveEntities.GetContext().ProductSale.Remove(currentProductSale);

                        // BebkoГлазкиSaveEntities.GetContext().SaveChanges();





                        //  LVHistory.ItemsSource = context.ProductSale.ToList();



                        LoadProductSalesForCurrentAgent();


                        MessageBox.Show("Элементы успешно удалены.");
                    }
                   
                
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void ProdData_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LVHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
