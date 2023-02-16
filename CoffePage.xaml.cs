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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CoffePage.xaml
    /// </summary>
    public partial class CoffePage : Page
    {
        public CoffePage()
        {
            InitializeComponent();
            DGridCoffe.ItemsSource = TourBaseEntities.GetContext().Hotel.ToList();
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            //Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotel));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridCoffe.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующее{hotelsForRemoving.Count()} элементов?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                try
                {
                    TourBaseEntities.GetContext().Hotel.RemoveRange(hotelsForRemoving);
                    TourBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCoffe.ItemsSource = TourBaseEntities.GetContext().Hotel.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        { 
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                TourBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridCoffe.ItemsSource = TourBaseEntities.GetContext().Hotel.ToList();
            }
        }
    }
}
