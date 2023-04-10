using SQLCon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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

namespace HW_17
{
    /// <summary>
    /// Логика взаимодействия для CasheWin.xaml
    /// </summary>
    public partial class CasheWin : Window
    {
        ObservableCollection<Products> prop = new ObservableCollection<Products>();
        public CasheWin()
        {
            InitializeComponent();

            Adding();
        }

        /// <summary>
        /// Просмотр корзины
        /// </summary>
        void Adding()
        {
            using (var context = new hwEntities())
            {
                var chill1 = AppData.db.Products.ToList();
                var chill = AppData.db.Cart.ToList();
                Products product = new Products();

                var chek = from a in chill1
                           join b in chill on a.product_id equals b.product_id
                           select new { a.product_name, a.price };

                foreach (var item in chek)
                {
                    prop.Add(new Products()
                    {
                        product_name = item.product_name,
                        price = item.price
                    });
                }
            }

            dtg.ItemsSource = prop;
            dtg.Items.Refresh();
        }

        /// <summary>
        /// К оплате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sum = (int)prop.Sum(a => a.price);

            MessageBox.Show($"К оплате {sum} рублей");
        }


        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var context = new hwEntities())
            {
                Products cart = new Products();
                cart = dtg.SelectedItem as Products;
                prop.Remove(cart);
                context.SaveChanges();
            }

            dtg.ItemsSource = prop;
            dtg.Items.Refresh();
        }
    }
}
