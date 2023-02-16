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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        public AddEditPage()
        {
            InitializeComponent();
        }
        private Hotel _currentHotel = new Hotel();
        bool f = false;
        public AddEditPage(Hotel selectedHotel )
        {
            InitializeComponent();
            if (selectedHotel != null)
                _currentHotel = selectedHotel;
            else f = true;
            DataContext = _currentHotel;
            cbCountry.ItemsSource = TourBaseEntities.GetContext().Country.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (f)
            {
                TourBaseEntities.GetContext().Hotel.Add(_currentHotel);
            }
            try
            {
                TourBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка -" + ex.Message.ToString()) ;
            }
        }
    }
}
