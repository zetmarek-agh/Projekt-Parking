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

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Parking parking = new Parking();
            MiejsceParkingowe mp = new MiejsceParkingowe(RodzajMiejsca.Zwykle, true);
            parking.Repo.Add(mp.Id, mp);
            parking.ZapiszJson("test");
            parking = (Parking)Parking.OdczytajJson("test");
            ParkingList.DataContext = parking;
        }

        private void ParkingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<MiejsceParkingowe> gridList = new List<MiejsceParkingowe>();
            gridList.Add(ParkingList.SelectedItem as MiejsceParkingowe);
            ParkingListGrid.ItemsSource = gridList;
        }
    }

    /*
     * Odczyt zapis do pliku
     * Wyświetlanie historii miejsca
     * Wjazd/wyjazd samochodu na parking
     * Cennik, obliczanie ceny
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */
}
