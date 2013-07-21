using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZonerPostcardManager.ViewModel;

namespace ZonerPostcardManager.Model
{
    public class XmlFile
    {
        private string _filePath;

        public List<PostcardViewModel> Postcards { get; set; }

        public XmlFile(string filePath)
        {
            _filePath = filePath;

            Postcards = readFile();
        }

        public void DeleteCards(IEnumerable<string> cardnames)
        {
            var xmlFile = XDocument.Load(_filePath);

            foreach (var cardname in cardnames)
            {
                var xmlCard = xmlFile.Descendants("card").FirstOrDefault(c => c.Attribute("name").Value == cardname);
                xmlCard.Remove();
            }

            xmlFile.Save(_filePath);
        }

        private List<PostcardViewModel> readFile()
        {
            var postcards = new List<PostcardViewModel>();
            var xmlFile = XDocument.Load(_filePath);

            foreach (var xmlCard in xmlFile.Descendants("card"))
            {
                var name = xmlCard.Attribute("name").Value;
                var xmlRectangles = xmlCard.Descendants("rect");
                var postcard = new PostcardViewModel(name, xmlRectangles);

                postcards.Add(postcard);
            }

            return postcards;
        }
    }
}
