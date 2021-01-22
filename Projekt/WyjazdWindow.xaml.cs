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

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy WyjazdWindow.xaml
    /// </summary>
    public partial class WyjazdWindow : Window
    {

        Parking parking;
        OsobaRepozytorium osobaRepo;
        public WyjazdWindow(Parking p, OsobaRepozytorium or)
        {
            InitializeComponent();
            this.parking = p;
            this.osobaRepo = or;
            this.MiejsceParkingoweLB.ItemsSource = parking.ListRepo;
        }

        private void Zwolnij_Click(object sender, RoutedEventArgs e)
        {
            MiejsceParkingowe mp = (MiejsceParkingowe)MiejsceParkingoweLB.SelectedItem;
            if (mp == null)
            {
                MessageBox.Show("Nie wybrano miejsca", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!mp.Zajete)
            {
                MessageBox.Show("Miejsce jest wolne", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    Pojazd pojazd = mp.ObecnyPojazd;
                    var wlasciciel = osobaRepo.List.First(x => x.Pojazdy.Contains(pojazd));
                    var czas = mp.OpuscMiejsce();
                    var doZaplaty = wlasciciel.Cennik.ObliczKoszt(czas, mp, wlasciciel);
                    this.MiejsceParkingoweLB.ItemsSource = parking.ListRepo;
                    MessageBox.Show($"Do zapłaty: {doZaplaty} PLN", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MiejsceParkingoweLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MiejsceParkingowe mp = (MiejsceParkingowe)MiejsceParkingoweLB.SelectedItem;
            HistoriaGrid.ItemsSource = mp.Historia.ZapisHistorii;
        }
    }
}
