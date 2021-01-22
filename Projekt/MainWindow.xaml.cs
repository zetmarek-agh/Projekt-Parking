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
using System.IO;
using Microsoft.Win32;
using System.Data.Odbc;

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Parking parking;
        OsobaRepozytorium osobaRepo;
        public MainWindow()
        {
            InitializeComponent();
            parking = new Parking();
            osobaRepo = new OsobaRepozytorium();
            Wczytaj_parking.IsEnabled = false;
            Zapisz_parking.IsEnabled = false;
            Wyjazd.IsEnabled = false;
            Wjazd.IsEnabled = false;
            //PopulateDB();
            //osobaRepo.ZapiszJson("osoby.json");
        }

        private void PopulateDB()
        {
            MiejsceParkingowe mp;
            for (int i = 0; i < 12; i++)
            {
                mp = new MiejsceParkingowe(RodzajMiejsca.Zwykle, false);
                parking.Repo.Add(mp.Id, mp);
            }
            mp = new MiejsceParkingowe(RodzajMiejsca.Zwykle, true);
            parking.Repo.Add(mp.Id, mp);
            mp = new MiejsceParkingowe(RodzajMiejsca.Male, false);
            parking.Repo.Add(mp.Id, mp);
            mp = new MiejsceParkingowe(RodzajMiejsca.Duze, false);
            parking.Repo.Add(mp.Id, mp);

            Osoba osoba;
            Pojazd testPojazd;

            osoba = new Osoba("Jan", "Kowalski", "45121478644", "jk@test.com", "123123123");
            testPojazd = new Samochod("12341", "Ford", "Czarny");
            osoba.Pojazdy.Add(testPojazd);
            testPojazd = new Jednoslad("12342", "Yamaha", "Czerwony");
            osoba.Pojazdy.Add(testPojazd);
            testPojazd = new DuzySamochod("12343", "Mack", "Czarny");
            osoba.Pojazdy.Add(testPojazd);
            osobaRepo.Repo.Add(osoba.Id, osoba);

            osoba = new Osoba("Anna", "Nowak", "64032377989", "an@test.com", "123123124");
            osoba.Cennik = new AbonamentCennik();
            testPojazd = new Samochod("12345", "Ford", "Czarny");
            osoba.Pojazdy.Add(testPojazd);
            testPojazd = new Jednoslad("12346", "Yamaha", "Czerwony");
            osoba.Pojazdy.Add(testPojazd);
            testPojazd = new DuzySamochod("12347", "Mack", "Czarny");
            osoba.Pojazdy.Add(testPojazd);
            osobaRepo.Repo.Add(osoba.Id, osoba);

            osoba = new Osoba("Jarosław", "Gęsiowski", "93092197758", "jg@test.com", "123123125", true);
            testPojazd = new Samochod("12345", "Ford", "Czarny");
            osoba.Pojazdy.Add(testPojazd);
            testPojazd = new Jednoslad("12346", "Yamaha", "Czerwony");
            osoba.Pojazdy.Add(testPojazd);
            testPojazd = new DuzySamochod("12347", "Mack", "Czarny");
            osoba.Pojazdy.Add(testPojazd);
            osobaRepo.Repo.Add(osoba.Id, osoba);
        }

        private void Zapisz_parking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pliki JSON (*.json)|*.json";
                sfd.DefaultExt = "json";
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == true)
                {
                    parking.ZapiszJson(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Wczytaj_parking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Pliki JSON (*.json)|*.json";
                ofd.DefaultExt = "json";
                ofd.AddExtension = true;
                if (ofd.ShowDialog() == true)
                {
                    parking = (Parking)Parking.OdczytajJson(ofd.FileName);
                }
                Zapisz_parking.IsEnabled = true;
                Wyjazd.IsEnabled = true;
                Wjazd.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Wczytaj_osoby_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Pliki JSON (*.json)|*.json";
                ofd.DefaultExt = "json";
                ofd.AddExtension = true;
                if (ofd.ShowDialog() == true)
                {
                    osobaRepo = (OsobaRepozytorium)OsobaRepozytorium.OdczytajJson(ofd.FileName);
                }
                Wczytaj_parking.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Wjazd_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<WjazdWindow>().FirstOrDefault() == null && Application.Current.Windows.OfType<WjazdWindow>().FirstOrDefault() == null)
            {
                WjazdWindow wjw = new WjazdWindow(parking, osobaRepo);
                wjw.Show();
            }
        }

        private void Wyjazd_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<WjazdWindow>().FirstOrDefault() == null && Application.Current.Windows.OfType<WjazdWindow>().FirstOrDefault() == null)
            {
                WyjazdWindow wyw = new WyjazdWindow(parking, osobaRepo);
                wyw.Show();
            }
        }
    }
}
