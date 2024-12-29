using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serilestirme
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Ogrenci ogr = new Ogrenci() { isim = "Murtaza", soyisim = "Şuayipoğlu", yas=190 };
            //Binary Serialization
            //byte[] gelen = Binaryserialize(ogr);
            //foreach (byte item in gelen)
            //{
            //    Console.Write(item);
            //}
            ////string BinaryToString = (string)BinaryDeserialize(gelen);
            //Console.WriteLine();
            //Ogrenci BinaryToString = (Ogrenci)BinaryDeserialize(gelen);
            //Console.WriteLine(BinaryToString.isim + " " + BinaryToString.soyisim);

            //string gelen = JsonSerialize(ogr);
            //Console.WriteLine(gelen);
            string gelen = XmlSerialize(ogr);
            Console.WriteLine(gelen);

            Ogrenci ogr2 = XmlDeSerialize();
            Console.WriteLine(ogr2.isim + " " + ogr.soyisim);
        }
        public static byte[] Binaryserialize(object veri)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, veri);
                return ms.ToArray();
            }
        }
        public static object BinaryDeserialize(byte[] veri)
        {
            using (MemoryStream ms = new MemoryStream(veri))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }
        public static string JsonSerialize(object veri)
        {
            return JsonConvert.SerializeObject(veri);
        }
        public static object JsonDeserialize(string veri)
        {
            return JsonConvert.DeserializeObject(veri);
        }
        public static string XmlSerialize(object veri)
        {
            using (StreamWriter sr = new StreamWriter("ogrenci.xml"))
            {
                XmlSerializer serilestirici = new XmlSerializer(typeof(Ogrenci));
                serilestirici.Serialize(sr, veri);
                return sr.ToString();
            }
        }
        public static string XmlDeSerialize()
        {
            using (StreamReader sr = new StreamReader("ogrenci.xml"))
            {
                XmlSerializer serilestirici = new XmlSerializer(typeof(Ogrenci));
                Ogrenci ogr = (Ogrenci)serilestirici.Deserialize(sr);
                return ogr;
            }
        }
    }
    [Serializable]
    class Ogrenci
    {
        public string isim;
        public string soyisim;
        [NonSerialized]//Bunu neyin üstüne koyarsak onu serilize etmemeyi sağlar.
        [XmlIgnore] //Xml'de görüntülemeyi kapatır.
        public int yas;
    }
}
