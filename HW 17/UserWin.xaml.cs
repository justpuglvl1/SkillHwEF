using SQLCon;
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
using System.Windows.Shapes;

namespace HW_17
{
    /// <summary>
    /// Логика взаимодействия для UserWin.xaml
    /// </summary>
    public partial class UserWin : Window
    {
        public UserWin()
        {
            InitializeComponent();

            dtg.ItemsSource = AppData.db.Users.ToList();
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new hwEntities())
            {
                Users cart = new Users();
                cart = dtg.SelectedItem as Users;
                var user1 = context.Users.Single(o => o.user_id == cart.user_id);
                context.Users.Remove(user1);
                context.SaveChanges();
            }
            var user = AppData.db.Users.ToList();

            dtg.ItemsSource = user;
            dtg.Items.Refresh();
        }
    }
}
