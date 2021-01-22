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
    /// Logika interakcji dla klasy WjazdWindow.xaml
    /// </summary>
    public partial class WjazdWindow : Window
    {
        Parking parking;
        OsobaRepozytorium osobaRepo;
        Pojazd pojazd = null;
        public WjazdWindow(Parking p, OsobaRepozytorium or)
        {
            InitializeComponent();
            this.parking = p;
            this.osobaRepo = or;
            this.MiejsceParkingoweLB.ItemsSource = parking.ListRepo;
        }

        private void Szukaj_Click(object sender, RoutedEventArgs e)
        {
            string nr = NrRejstracyjnyTB.Text;
            try
            {
                var osoba = osobaRepo.List.FirstOrDefault(x => x.Pojazdy.FirstOrDefault(y => y.NrRejstracyjny == nr) != null);
                if (osoba == null)
                    throw new Exception();
                var pojazd = osoba.Pojazdy.FirstOrDefault(y => y.NrRejstracyjny == nr);
                if (pojazd == null)
                    throw new Exception();
                this.pojazd = pojazd;
            }
            catch
            {
                MessageBox.Show("Nie ma takiego pojazdu", "", MessageBoxButton.OK, MessageBoxImage.Error);
                this.pojazd = null;
            }
        }

        private void Zajmij_Click(object sender, RoutedEventArgs e)
        {
            MiejsceParkingowe mp = (MiejsceParkingowe)MiejsceParkingoweLB.SelectedItem;
            if (this.pojazd == null)
            {
                MessageBox.Show("Nie wybrano pojazdu", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (mp == null)
            {
                MessageBox.Show("Nie wybrano miejsca", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    mp.ZajmijMiejsce(pojazd, osobaRepo.List.First(x => x.Pojazdy.Contains(pojazd)));
                    this.MiejsceParkingoweLB.ItemsSource = parking.ListRepo;
                    MessageBox.Show("Sukces", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception ex)
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
