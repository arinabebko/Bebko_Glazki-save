using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        private List<Product> Items;

     //   private Agent currentAgent = new Agent();
        private List<Product> _allProducts; // Список всех продуктов
        private List<ProductSale> _currentProductSales; // Список текущих продаж

        public History(Agent SelectedAgent)
        {
            InitializeComponent();
            LoadItems();

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

            // Загружаем все продукты и устанавливаем источник данных для ComboBox
            _allProducts = BebkoГлазкиSaveEntities.GetContext().Product.ToList();
            ComboBoxProduct.ItemsSource = _allProducts;
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



        private void LoadCurrentProductSales()
        {
            _currentProductSales = BebkoГлазкиSaveEntities.GetContext().ProductSale
                .Where(ps => ps.AgentID == currentAgent.ID)
                .ToList();

            LVHistory.ItemsSource = _currentProductSales; // Устанавливаем источник данных для списка продаж
        }



        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный продукт из ComboBox
            var selectedProduct = ComboBoxProduct.SelectedItem as Product;

            // Получаем количество из TextBox (например, для количества продукта)
            int productCount;
            if (!int.TryParse(ProdCount.Text, out productCount) || productCount <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество.");
                return;
            }

            if (selectedProduct != null)
            {
                // Создаем новый объект ProductSale
                var newSale = new ProductSale
                {
                    AgentID = currentAgent.ID, // Указываем ID агента
                    ProductID = selectedProduct.ID, // Указываем ID выбранного продукта
                    SaleDate = ProdDate.SelectedDate ?? DateTime.Now, // Указываем дату продажи (если выбрана)
                    ProductCount = productCount // Указываем количество, введенное пользователем
                };

                // Добавляем новый объект в контекст и сохраняем изменения
                BebkoГлазкиSaveEntities.GetContext().ProductSale.Add(newSale);
                BebkoГлазкиSaveEntities.GetContext().SaveChanges();

                MessageBox.Show("информация сохранена");

                // Обновляем список продаж
                LoadCurrentProductSales();

                // Очистка полей ввода (по желанию)
                ComboBoxProduct.SelectedItem = null;
                ProdCount.Clear();
                ProdDate.SelectedDate = null;
                ProdSearc.Text = ""; // Очищает текстовое поле

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для добавления.");
            }
        }


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

      

        private void LVHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     

        private List<string> allProducts = new List<string>();
        private void ProdSearc_TextChanged(object sender, TextChangedEventArgs e)
        {


           
            
                // Получаем текст из TextBox и преобразуем в нижний регистр
                string searchText = ProdSearc.Text.ToLower();

                // Фильтруем элементы на основе текста поиска
                var filteredItems = Items.Where(p => p.Title.ToLower().Contains(searchText)).ToList();

                // Устанавливаем источник данных для ComboBox
                ComboBoxProduct.ItemsSource = filteredItems;

                // Если ничего не найдено, показываем все элементы
                if (string.IsNullOrEmpty(searchText))
                {
                    ComboBoxProduct.ItemsSource = Items;
                }
            
        }



        private void LoadItems()
        {
            // Здесь вы загружаете элементы из базы данных
            Items = BebkoГлазкиSaveEntities.GetContext().Product.ToList();

            // Устанавливаем источник данных для ComboBox
            ComboBoxProduct.ItemsSource = Items;
        }

        private void ComboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxProduct.SelectedItem is Product selectedProduct)
            {
                // Подставляем значение Title в TextBox
                ProdSearc.Text = selectedProduct.Title; // Убедитесь, что у вас есть TextBox с именем ProdSearc
            }
            /*
            string searchText = ProdSearc.Text.ToLower(); // Получаем текст из TextBox и преобразуем в нижний регистр

            ComboBoxProduct.Items.Clear(); // Очищаем существующие элементы в ComboBox

            // Фильтруем элементы на основе текста поиска
            var filteredProducts = allProducts.Where(item => item.ToLower().StartsWith(searchText)).ToList();

            // Добавляем отфильтрованные элементы обратно в ComboBox
            foreach (var product in filteredProducts)
            {
                ComboBoxProduct.Items.Add(product); // Добавляем каждый элемент по одному
            }

            // Опционально, устанавливаем фокус на ComboBox, чтобы показать выпадающий список
            if (ComboBoxProduct.Items.Count > 0)
            {
                ComboBoxProduct.Focus(); // Устанавливаем фокус на ComboBox
                ComboBoxProduct.IsDropDownOpen = true; // Открываем выпадающий список
            }


            
            string searchText = ProdSearc.Text.ToLower(); // Получаем текст из TextBox и преобразуем в нижний регистр


            ComboBoxProduct.Items.Clear(); // Очищаем существующие элементы в ComboBox

            // Фильтруем элементы на основе текста поиска
            var filteredProducts = allProducts.Where(item => item.ToLower().StartsWith(searchText)).ToList();

            // Добавляем отфильтрованные элементы обратно в ComboBox
            ComboBoxProduct.Items.AddRange(filteredProducts.ToArray());

            // Опционально, открываем выпадающий список, если есть соответствующие элементы
            if (ComboBoxProduct.Items.Count > 0)
            {
                ComboBoxProduct.DroppedDown = true; // Показываем выпадающий список
            }*/
        }
    }
}
