using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ScrapedItems scrapedItems = new ScrapedItems();
            //scrapedItems.SrapeWebsite('M');
            links link = new links();
            link.SrapeWebsite('a', "https://kuchnialidla.pl/przepisy/kuchnia-amerykanska");
            link.SrapeWebsite('p', "https://kuchnialidla.pl/przepisy/kuchnia-polska");
            link.SrapeWebsite('w', "https://kuchnialidla.pl/przepisy/kuchnia-wloska");
            link.SrapeWebsite('s', "https://kuchnialidla.pl/przepisy/kuchnia-hiszpanska-i-portugalska");
            link.SrapeWebsite('o', "https://kuchnialidla.pl/przepisy/kuchnia-orientalna");
            link.SrapeWebsite('m', "https://kuchnialidla.pl/przepisy/kuchnia-meksykanska");
            link.SrapeWebsite('g', "https://kuchnialidla.pl/przepisy/kuchnia-grecka");
            link.SrapeWebsite('f', "https://kuchnialidla.pl/przepisy/kuchnia-francuska");
            link.SrapeWebsite('t', "https://kuchnialidla.pl/przepisy/kuchnia-tajska");
            link.SrapeWebsite('c', "https://kuchnialidla.pl/przepisy/kuchnia-czeska");
            link.SrapeWebsite('e', "https://kuchnialidla.pl/przepisy/brytyjska");
            link.SrapeWebsite('z', "https://kuchnialidla.pl/przepisy/kuchnia-azjatycka");

        }
    }
}
