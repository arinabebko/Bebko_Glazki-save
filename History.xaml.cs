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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProdData_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
