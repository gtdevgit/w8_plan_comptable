using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace PlanComptableW8.Modele
{
    public class StorageHelper
    {
        public static string Serialiser<T>(T data)
        {
            using (var memoryStream = new MemoryStream())
            {
                // L'utilisation du XMLSerialiser ajoute <?xml version="1.0"?> au début de la chaîne
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(memoryStream, data);

                // var serializer = new DataContractSerializer(typeof(T));
                // serializer.WriteObject(memoryStream, data);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var reader = new StreamReader(memoryStream);
                var content = reader.ReadToEnd();

                return content;
            }
        }

        public static T Deserialiser<T>(string xml)
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
            {
                // var serializer = new DataContractSerializer(typeof(T));
                // var theObject = (T)serializer.ReadObject(stream);

                var serializer = new XmlSerializer(typeof(T));
                var theObject = (T)serializer.Deserialize(stream);

                return theObject;
            }
        }

        public static async Task EcrireDansLocalStorage(string fic, string data)
        {
            StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile newFile = await localFolder.CreateFileAsync(fic, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(newFile, data);
        }

        public static async Task<string> LireDansLocalStorage(string fic)
        {
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(fic);
            string texte = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            return texte;
        }
    }
}
