﻿using System;
using BilgeKafe.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BilgeKafe.UI.Properties;

namespace BilgeKafe.UI
{
    public partial class AnaForm : Form
    {
        KafeVeri db = new KafeVeri();
        public AnaForm()
        {
            OrnekUrunleriOlustur();
            InitializeComponent();
            MasalariOlustur();
        }

        private void OrnekUrunleriOlustur()
        {
            db.Urunler.Add(new Urun() { UrunAd = "Kola", BirimFİyat = 5.99m });
            db.Urunler.Add(new Urun() { UrunAd = "Çay", BirimFİyat = 4.50m });
        }

        private void MasalariOlustur()
        {
            #region Imaj Listesinin Olusturulmasi
            ImageList imageList = new ImageList();
            imageList.Images.Add("bos", Resources.bos);
            imageList.Images.Add("dolu", Resources.dolu);
            imageList.ImageSize = new Size(64, 64);
            lvwMasalar.LargeImageList = imageList;
            #endregion

            for (int i = 1; i <= db.MasaAdet; i++)
            {
                ListViewItem lvi = new ListViewItem($"Masa {i}");
                lvi.Tag = i;
                lvi.ImageKey = "bos";
                lvwMasalar.Items.Add(lvi);
            }
        }

        private void lvwMasalar_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvi = lvwMasalar.SelectedItems[0];
            lvi.ImageKey = "dolu";
            int masaNo = (int)lvi.Tag;

            // Tıklanan masaya ait varsa siparişi bul
            Siparis siparis = db.AktifSiparisler.FirstOrDefault(x => x.MasaNo == masaNo);

            // Eğer sipariş henüz oluşturulmadıysa ( o masaya ait )
            if (siparis == null)
            {
                siparis = new Siparis() { MasaNo = masaNo };
                db.AktifSiparisler.Add(siparis);
            }

            SiparisForm frmSiparis = new SiparisForm(db, siparis);
            frmSiparis.ShowDialog();
        }
    }
}
