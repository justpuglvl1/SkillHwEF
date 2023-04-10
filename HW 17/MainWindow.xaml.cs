using System;
using System.Collections.Generic;
using System.Data;
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
using SQLCon;

namespace HW_17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            dtgView.ItemsSource = AppData.db.Products.ToList();
        }

        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new hwEntities())
            {
                Products products = new Products();

                var addUser = AppData.db.Users.Where(u => u.name == "Алексей" && u.mail == "skillbox@box.ru");

                Users users = new Users();

                foreach (Users users1 in addUser)
                {
                    users = users1;
                }

                products = dtgView.SelectedItem as Products;

                Cart cart = new Cart()
                {
                    user_id = users.user_id,
                    product_id = products.product_id
                };

                context.Cart.Add(cart);
                context.SaveChangesAsync();

                MessageBox.Show($"Добавлено в корзину {products.product_name}");
            }
        }

        /// <summary>
        /// Переход в корзину
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CasheWin cw = new CasheWin();
            cw.Show();
        }

        /// <summary>
        /// Просмотр клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UserWin userWin = new UserWin();
            userWin.Show();
        }
    }
}
