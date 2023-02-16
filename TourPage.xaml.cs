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
    /// Логика взаимодействия для TourPage.xaml
    /// </summary>
    public partial class TourPage : Page
    {
        public TourPage()
        {
            InitializeComponent();

            var actualType = TourBaseEntities.GetContext().Type.ToList();
            actualType.Insert(0, new Type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = actualType;

            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;

            var currentTours = TourBaseEntities.GetContext().Tour.ToList();
            LV.ItemsSource = currentTours;
            
        }

        private void UpdateTours()
        {
            var currentTours = TourBaseEntities.GetContext().Tour.ToList();

            if(ComboType.SelectedIndex > 0)
            {
                currentTours = currentTours.Where(p => p.Type.Contains(ComboType.SelectedItem as Type)).ToList();

                currentTours = currentTours.Where(p => p.Name.ToLower().Contains(TBoSearch.Text.ToLower())).ToList();

                if (CheckActual.IsChecked.Value)
                    currentTours = currentTours.Where(p => p.IsActual).ToList();

                LV.ItemsSource = currentTours.OrderBy(p => p.Ticketount).ToList();

            }
        }

        private void TBoSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void CheckActual_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
    }
}
