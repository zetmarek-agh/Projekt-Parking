using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt;

namespace ProjektTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ParkingAdd_ChangesCount()
        {
            Parking p = new Parking();
            MiejsceParkingowe mp = new MiejsceParkingowe(RodzajMiejsca.Duze, false);

            Assert.IsTrue(p.Repo.Count == 0);
            p.Add(mp);
            Assert.IsTrue(p.Repo.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(OsobaIstniejeException), "Taki same pesel")]
        public void OsobaRepozytoriumAdd_ThrowsException_WhenSamePesel()
        {
            OsobaRepozytorium or = new OsobaRepozytorium();
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", "alanowak@gmail.com", "123321123");
            Osoba os2 = new Osoba("Ala", "Nowak", "45121478644", "alanowak2@gmail.com", "123321123");

            or.Add(os1);
            or.Add(os2);
        }

        [TestMethod]
        [ExpectedException(typeof(OsobaIstniejeException), "Taki sam Email")]
        public void OsobaRepozytoriumAdd_ThrowsException_WhenSameEmail()
        {
            OsobaRepozytorium or = new OsobaRepozytorium();
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", "alanowak@gmail.com", "123321123");
            Osoba os2 = new Osoba("Ala", "Nowak", "64032377989", "alanowak@gmail.com", "123321123");

            or.Add(os1);
            or.Add(os2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Pesel ma złą długość")]
        public void Osoba_ThrowsException_WhenPeselBadLength()
        {
            Osoba os1 = new Osoba("Ala", "Nowak", "1", "alanowak", "123321123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Pesel nie ma tylko cyfr")]
        public void Osoba_ThrowsException_WhenPeselNotOnlyDigits()
        {
            Osoba os1 = new Osoba("Ala", "Nowak", "aaaaaaaaaaa", "alanowak", "123321123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Pesel ma złą sumę kontrolną")]
        public void Osoba_ThrowsException_WhenPeselBadChecksum()
        {
            Osoba os1 = new Osoba("Ala", "Nowak", "11111111111", "alanowak", "123321123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Email jest zły")]
        public void Osoba_ThrowsException_WhenBadEmail()
        {
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", "alanowak", "123321123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Email nie może być null lub pusty")]
        public void Osoba_ThrowsException_WhenEmptyEmail()
        {
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", "", "123321123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Email nie może być null lub pusty")]
        public void Osoba_ThrowsException_WhenNullEmail()
        {
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", null, "123321123");
        }

        [TestMethod]
        [ExpectedException(typeof(MiejsceZajeteException))]
        public void MiejsceParkingoweZajmijMiejsce_ThrowsException_WhenMiejsceZajete()
        {
            MiejsceParkingowe mp = new MiejsceParkingowe(RodzajMiejsca.Duze, false);
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", "alanowak@gmail.com", "123321123");
            Samochod s1 = new Samochod("PO1232", os1, "Ford", "Czerwony");
            Samochod s2 = new Samochod("PO1231", os1, "Ford", "Czerwony");

            mp.ZajmijMiejsce(s1);
            mp.ZajmijMiejsce(s2);
        }

        [TestMethod]
        [ExpectedException(typeof(MiejsceWolneException))]
        public void MiejsceParkingoweOpuscMiejsce_ThrowsException_WhenMiejsceWolne()
        {
            MiejsceParkingowe mp = new MiejsceParkingowe(RodzajMiejsca.Duze, false);

            mp.OpuscMiejsce();
        }

        [TestMethod]
        public void MiejsceParkingowe_Normal()
        {
            MiejsceParkingowe mp = new MiejsceParkingowe(RodzajMiejsca.Duze, false);
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", "alanowak@gmail.com", "123321123");
            Samochod s1 = new Samochod("PO1232", os1, "Ford", "Czerwony");
            Samochod s2 = new Samochod("PO1231", os1, "Ford", "Czerwony");
            
            mp.ZajmijMiejsce(s1);
            mp.OpuscMiejsce();
            mp.ZajmijMiejsce(s2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "dataZakonczenia musi być późniejsza niż dataRozpoczecia!")]
        public void MiejsceParkingoweOpuscMiejsce_ThrowsException_WhenDataZakonczeniaBeforeDataRozpoczecia()
        {
            MiejsceParkingowe mp = new MiejsceParkingowe(RodzajMiejsca.Duze, false);
            Osoba os1 = new Osoba("Ala", "Nowak", "45121478644", "alanowak@gmail.com", "123321123");
            Samochod s1 = new Samochod("PO1232", os1, "Ford", "Czerwony");
            DateTime dataRozpoczecia = DateTime.Parse("2020-05-01");
            DateTime dataZakonczenia = DateTime.Parse("2020-04-01");

            mp.ZajmijMiejsce(s1, dataRozpoczecia);
            mp.OpuscMiejsce(dataZakonczenia);
        }
    }
}
