using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Metin2_Efsun_Hesaplayici
{
    public partial class FormEfsun : Form
    {
        public static double maxHp = 0, canlilik = 0, zeka = 0, guc = 0, ceviklik = 0, buyuHiz = 0,
        kritik = 0, delici = 0, yariGuc = 0, blok = 0, oklardanKorunma = 0,
        kilicSav = 0, bicakSav =0, ciftelSav =0, okSav = 0, buyuSav =0, zehreKarsiKoyma = 0,
        hpUretimi = 0, savasciSav = 0, ninjaSav=0, suraSav=0, samanSav=0, 
        savasciGuc=0, ninjaGuc=0, suraGuc=0, samanGuc=0, saldiriDegeri = 0, canavarGuc=0;

        private void checkDeliciTasi_CheckedChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void checkKritikTasi_CheckedChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void checkWayne_CheckedChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void checkAtesAnka_CheckedChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void checkSomuru_CheckStateChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void dataGridEfsun_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
            MessageBox.Show("Error happened " + e.Context.ToString());

            if (e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (e.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((e.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                e.ThrowException = false;
            }
            
        }

        private void checkYasamTasi_CheckStateChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void checkKurnazlikTasi_CheckStateChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void checkKorunma_CheckStateChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void checkBiyolog_CheckStateChanged(object sender, EventArgs e)
        {
            hesapla();
        }

        private void dataGridEfsun_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridEfsun.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dataGridEfsun.CommitEdit(DataGridViewDataErrorContexts.Commit);
                hesapla();
            }
        }

        private void dataGridEfsun_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridEfsun.BeginEdit(true);
                ComboBox com = (ComboBox)dataGridEfsun.EditingControl;
                com.DroppedDown = true;
            }
            catch 
            {
                MessageBox.Show("Bir hata oluştu");
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectSet = new OpenFileDialog();
            selectSet.Title = "Kaydedilmiş Seti Seçiniz";
            selectSet.Filter = "Txt files(*.txt)| *.txt";
            selectSet.FilterIndex = 1;
            selectSet.Multiselect = true;

            if (selectSet.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    string iFileName = selectSet.FileName;
                    reset();
                    makeParse(iFileName);
                    hesapla();
                }
                catch
                {
                    MessageBox.Show("Bir hata oluştu.");
                }
            }
        }
      
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveSetFile = new SaveFileDialog();
            saveSetFile.Title = "Seti kaydedin";
            //saveSetFile.CheckFileExists = true;
            //saveSetFile.CheckPathExists = true;
            saveSetFile.Filter = "Txt files(*.txt)| *.txt";

            if (saveSetFile.ShowDialog() == DialogResult.OK)
            {
                string saveFileName = saveSetFile.FileName;
                //if (!File.Exists(saveFileName))
                //{
                    using (var tw = new StreamWriter(saveFileName, true))
                    {
                        for (int i = 0; i < dataGridEfsun.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridEfsun.ColumnCount; j++)
                            {
                                if (dataGridEfsun.Rows[i].Cells[j].Value == null)
                                {
                                    tw.WriteLine(i.ToString() + "\t" + j.ToString() + "\t" + "N");
                                }
                                else
                                {
                                    tw.WriteLine(i.ToString() + "\t" + j.ToString() + "\t" + dataGridEfsun.Rows[i].Cells[j].Value.ToString());
                                }
                            }
                        }

                        MessageBox.Show("Set bilgisi kaydedildi.");
                    }
                //}
            }

        }

        public FormEfsun()
        {
            InitializeComponent();
        }

        private void FormEfsun_Load(object sender, EventArgs e)
        {
            dataGridYukle();
        }
         
        public void efsunSifirla()
        {
            maxHp = 0;
            canlilik = 0;
            zeka = 0;
            guc = 0;
            ceviklik = 0;
            buyuHiz = 0;
            kritik = 0;
            delici = 0;
            yariGuc = 0;
            blok = 0;
            oklardanKorunma = 0;
            kilicSav = 0;
            bicakSav = 0;
            ciftelSav = 0;
            okSav = 0;
            buyuSav = 0;
            zehreKarsiKoyma = 0;
            hpUretimi = 0;
            savasciSav = 0;
            ninjaSav = 0;
            suraSav = 0;
            samanSav = 0;
            savasciGuc = 0;
            ninjaGuc = 0;
            suraGuc = 0;
            samanGuc = 0;
            saldiriDegeri = 0;
            canavarGuc = 0;
        }

        public void reset()
        {
            this.dataGridEfsun.Rows.Clear();
            checkBiyolog.Checked = false;
            checkSomuru.Checked = false;
            checkKorunmaTasi.Checked = false;
            checkKurnazlikTasi.Checked = false;
            checkAtesAnka.Checked = false;
            checkWayne.Checked = false;
            checkKritikTasi.Checked = false;
            checkDeliciTasi.Checked = false;

            dataGridYukle();
            hesapla();
        }

        public void makeParse(String iFileName)
        {
            try
            {
                string strSource = File.ReadAllText(@" " + iFileName);
                strSource = strSource.TrimEnd(new char[] { '\r', '\n' }); //DELETE LAST LINE (EMPTY)

                using (StringReader reader = new StringReader(strSource))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {

                        string[] words = line.Split('\t');

                        if (words[2] != "N")
                        {
                            dataGridEfsun.Rows[int.Parse(words[0])].Cells[int.Parse(words[1])].Value = words[2].ToString();
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public void dataGridYukle()
        {
            dataGridEfsun.RowCount = 12;

            String[] efsunListesi = new string[]
            {
                   "Max HP",
                   "Canlılık",
                   "Zeka",
                   "Güç",
                   "Çeviklik",
                   "Büyü Hızı",
                   "Kritik",
                   "Delici",
                   "Yarı Güç",
                   "Blok",
                   "Oklardan Korunma Şansı",
                   "Kılıç Sav",
                   "Bıçak Sav",
                   "Çiftel Sav",
                   "Ok Sav",
                   "Büyü Sav",
                   "Zehre Karşı Koyma",
                   "Hp Üretimi",
                   "Savaşçı Sav",
                   "Ninja Sav",
                   "Sura Sav",
                   "Şaman Sav",
                   "Savaşçı Güç",
                   "Ninja Güç",
                   "Sura Güç",
                   "Şaman Güç",
                   "Saldırı Değeri",
                   "Canavarlara Karşı Güç"
             };

            String[] zirhListesi = new string[] {"Şeytan Boynuzu Zırh", "Ejder Süvari Giysisi" , "Kemik Plaka Zırh",
            "Altın Giysi","115 Savunma Zırh","B.Kraliyet Zırh", "A Turkuaz-Bloklu"};
            String[] kaskListesi = new string[] {"Korku Maskesi","Usta Savaşçı Kaskı","Ork Kapşonu","Örümcek Kapşon",
            "Boynuzlu Kask","Büyülü Kask","Kardinal Şapkası","Ruh Yongası Şapkası"};
            String[] kalkanListesi = new string[] { "Titan Kalkan" };
            String[] bileklikListesi = new string[] { "Yakut Bileklik", "Grena Bileklik", "Zümrüt Bileklik", "Safir Bileklik" };
            String[] kupeListesi = new string[] { "Yakut Küpe", "Grena Küpe", "Zümrüt Küpe", "Safir Küpe" };
            String[] kolyeListesi = new string[] { "Yakut Kolye", "Grena Kolye", "Zümrüt Kolye", "Safir Kolye",
                "Cennetin Gözü Kolye", "Mor Yakut Kolye", "Altın Kolye"};
            String[] ayakkabiListesi = new string[] { "Coşku Ayakkabısı", "Kirin Ayakkabı", "Deri Çizmesi", "Şöhret Çizmesi"};
            String[] kemerListesi = new string[] { "Keten Kemer", "Kral Kemeri", "Gölge Kemeri", "Runik Kemer", "Büyükayı Kemeri","Ruh Kemeri" };

            //İTEM VE EFSUNLARI YÜKLEME
            for (int i = 0; i < dataGridEfsun.RowCount; i++)
            {
                for (int j = 0; j < dataGridEfsun.ColumnCount; j++)
                {
                    //j=0 ilk sütunu, itemi temsil eder.
                    if (j > 0)
                    {
                        //EFSUNLARI YÜKLE
                        dataGridEfsun.Rows[i].Cells[j].Value = null;
                        DataGridViewComboBoxCell cellEfsun = new DataGridViewComboBoxCell();
                        for (int k = 0; k < efsunListesi.Length; k++)
                        {
                            cellEfsun.Items.Add(efsunListesi[k]);
                        }
                        dataGridEfsun.Rows[i].Cells[j] = cellEfsun;
                    }
                    else
                    {
                        //İTEMLERİ YÜKLE
                        dataGridEfsun.Rows[i].Cells[j].Value = null; //this is important.
                        DataGridViewComboBoxCell cellEfsun = new DataGridViewComboBoxCell();

                        if (i == 0)
                        {
                            //ZIRH
                            for (int k = 0; k < zirhListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(zirhListesi[k]);
                            }
                        }

                        else if (i == 1)
                        {
                            //KASK
                            for (int k = 0; k < kaskListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(kaskListesi[k]);
                            }
                        }

                        else if (i == 2)
                        {
                            //KALKAN
                            for (int k = 0; k < kalkanListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(kalkanListesi[k]);
                            }
                        }

                        else if (i == 3)
                        {
                            //BİLEKLİK
                            for (int k = 0; k < bileklikListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(bileklikListesi[k]);
                            }
                        }

                        else if (i == 4)
                        {
                            //KÜPE
                            for (int k = 0; k < kupeListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(kupeListesi[k]);
                            }
                        }

                        else if (i == 5)
                        {
                            //KOLYE
                            for (int k = 0; k < kolyeListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(kolyeListesi[k]);
                            }
                        }

                        else if (i == 6)
                        {
                            //AYAKKABI
                            for (int k = 0; k < ayakkabiListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(ayakkabiListesi[k]);
                            }
                        }

                        else if (i == 7)
                        {
                            cellEfsun.Items.Add("Kostüm");
                        }

                        else if (i == 8)
                        {
                            cellEfsun.Items.Add("Tengu Maskesi");
                            cellEfsun.Items.Add("Ay Yıldız Saç");
                        }

                        else if (i == 9)
                        {
                            cellEfsun.Items.Add("Fiş");
                        }

                        else if (i == 10)
                        {
                            cellEfsun.Items.Add("Evcil");
                        }

                        else if (i == 11)
                        {
                            for (int k = 0; k < kemerListesi.Length; k++)
                            {
                                cellEfsun.Items.Add(kemerListesi[k]);
                            }
                        }

                        dataGridEfsun.Rows[i].Cells[j] = cellEfsun;
                    }

                }
            }

            //BAZI SEÇİMLERİ İPTAL ETME
            for (int i = 0; i < dataGridEfsun.RowCount; i++)
                for (int j = 0; j < dataGridEfsun.ColumnCount; j++)
                {
                    if ( ( (i == 7 || i == 8 || i == 9) && j > 5) || (i == 10 && j > 3) || (i == 11 && j > 0))
                    {
                        dataGridEfsun.Rows[i].Cells[j].ReadOnly = true;
                    }
                }
        }

        public void hesapla()
        {
            efsunSifirla();
            //[sütun,satır]

            //DEFAULT OYUN DEĞERLERİNİN EKLENMESİ
            //1 TANESİ 91 OLMALI
            guc += 105;
            zeka += 105;
            canlilik += 105;
            ceviklik += 105;

            //KRİTİK DELİCİ İSABET
            kritik += 20;
            delici += 20;
            //İTEMLERİN EKLENMESİ
            for (int i = 0; i < dataGridEfsun.RowCount; i++)
            {
                String data = (string)dataGridEfsun[0, i].Value;

                if (data != null)
                {
                    itemEkle(data);
                }
            }

            //EFSUNLARIN EKLENMESİ
            for (int i = 1; i < dataGridEfsun.ColumnCount; i++)
            {
                //8 SÜTUN, 11 SATIR
                for (int j = 0; j < dataGridEfsun.RowCount; j++)
                {
                    String itemAdi = (string)dataGridEfsun[0, j].Value;
                    String efsun = (string)dataGridEfsun[i, j].Value;
                    bool enerji = true;
                    if (i > 5 || ((itemAdi == "Kostüm" || itemAdi == "Tengu Maskesi" || itemAdi == "Ay Yıldız Saç" || itemAdi == "Fiş") && (i > 3)) || itemAdi == "Evcil")
                    {
                        enerji = false;
                    }

                    if (itemAdi != null && efsun != null)
                    {
                        efsunEkle(itemAdi, efsun, enerji);
                    }
                }
            }

            if (checkBiyolog.Checked == true)
            {
                biyologGoreviYap();
            }

            if (checkSomuru.Checked == true)
            {
                somuru();
            }

            if (checkKorunmaTasi.Checked == true)
            {
                korunmaTasi();
            }

            if (checkKurnazlikTasi.Checked == true)
            {
                kurnazlikTasi();
            }

            if (checkAtesAnka.Checked == true)
            {
                atesAnkasi();
            }

            if (checkKritikTasi.Checked == true)
            {
                kritikTasi();
            }

            if (checkDeliciTasi.Checked == true)
            {
                deliciTasi();
            }

            if (checkWayne.Checked == true)
            {
                Wayne();
            }

            oranKutularıGuncelle();
        }

        public void itemEkle(String itemAdi)
            {
                double kemerDeger = 14;

                switch (itemAdi)
                {
                //ZIRHLAR
                //"Şeytan Boynuzu Zırh", "Ejder Süvari Giysisi" , "Kemik Plaka Zırh", "Altın Giysi","115 Savunma Zırh","B.Kraliyet Zırh"
                case "Şeytan Boynuzu Zırh":
                    zehreKarsiKoyma += 40;
                    buyuSav += 30;
                    guc += 20;
                    break;

                case "Ejder Süvari Giysisi":
                    zehreKarsiKoyma += 40;
                    buyuSav += 30;
                    ceviklik += 20;
                    break;

                case "Kemik Plaka Zırh":
                    zehreKarsiKoyma += 40;
                    buyuSav += 30;
                    zeka += 20;
                    break;

                case "Altın Giysi":
                    zehreKarsiKoyma += 40;
                    buyuSav += 30;
                    zeka += 20;
                    break;

                case "115 Savunma Zırh":
                    buyuSav += 30;
                    break;

                case "B.Kraliyet Zırh":
                    buyuSav += 40;
                    hpUretimi += 25;
                    break;

                case "A Turkuaz-Bloklu":
                    buyuSav += 25;
                    blok += 20;
                    break;
                    
                //KASKLAR
                //"Korku Maskesi","Usta Savaşçı Kaskı","Ork Kapşonu","Örümcek Kapşon","Boynuzlu Kask","Büyülü Kask","Kardinal Şapkası","Ruh Yongası Şapkası"
                case "Korku Maskesi":
                    blok += 10;
                    break;

                case "Usta Savaşçı Kaskı":
                    oklardanKorunma += 12;
                    break;

                case "Ork Kapşonu":
                    blok += 10;
                    break;
                /*
                case "Örümcek Kapşon":
                    break;
                */
                case "Boynuzlu Kask":
                    blok += 10;
                    break;
                /*
                case "Büyülü Kask":
                   break;
                */
                case "Kardinal Şapkası":
                    blok += 10;
                    break;
                /*
                case "Ruh Yongası Şapkası":
                    break;
                */

                //KALKANLAR
                /*
                case "Titan Kalkan":
                    break;
                */

                //BİLEKLİKLER
                //"Yakut Bileklik", "Grena Bileklik", "Zümrüt Bileklik", "Safir Bileklik"
                case "Yakut Bileklik":
                    savasciSav += 14;
                    break;

                case "Grena Bileklik":
                    ninjaSav += 14;
                    break;

                case "Zümrüt Bileklik":
                    suraSav += 14;
                    break;

                case "Safir Bileklik":
                    maxHp += 2520;
                    samanSav += 14;
                    break;

                //KÜPELER
                //"Yakut Küpe", "Grena Küpe", "Zümrüt Küpe", "Safir Küpe"
                case "Yakut Küpe":
                    zeka += 19;
                    break;

                case "Grena Küpe":
                    ceviklik += 19;
                    maxHp += 2310;
                    break;

                case "Zümrüt Küpe":
                    guc += 19;
                    break;
                
                case "Safir Küpe":
                    canlilik += 19;
                    hpUretimi += 40;
                    break;

                //KOLYELER
                //"Yakut Kolye", "Grena Kolye", "Zümrüt Kolye", "Safir Kolye", "Cennetin Gözü Kolye", "Mor Yakut Kolye", "Altın Kolye"
                case "Yakut Kolye":
                    ceviklik += 7;
                    savasciGuc += 14;
                    buyuHiz += 39;
                    break;

                case "Grena Kolye":
                    canlilik += 7;
                    ninjaGuc += 14;
                    buyuHiz += 39;
                    break;

                case "Zümrüt Kolye":
                    zeka += 7;
                    suraGuc += 14;
                    buyuHiz += 39;
                    break;

                case "Safir Kolye":
                    guc += 7;
                    samanGuc += 14;
                    buyuHiz += 39;
                    break;

                case "Cennetin Gözü Kolye":
                    buyuHiz += 24; //buyuHiz += 33; 
                    delici += 20; //delici += 28; 
                    break;

                case "Mor Yakut Kolye":
                    buyuHiz += 33;
                    hpUretimi += 78;
                    break;

                case "Altın Kolye":
                    buyuHiz += 28;
                    okSav += 14;
                    break;

                    //AYAKKABILAR
                case "Coşku Ayakkabısı":
                    buyuHiz += 20;
                    break;

                case "Kirin Ayakkabı":
                    blok += 20;
                    break;

                case "Deri Çizmesi":
                    okSav += 20;
                    break;

                case "Şöhret Çizmesi":
                    oklardanKorunma += 20;
                    break;

                //KOSTÜM
                case "Kostüm":
                    maxHp += 2500;
                    break;

                //SAÇLAR
                //Tengu Maskesi, Ay Yıldız Saç
                case "Tengu Maskesi":
                    buyuHiz += 20;
                    canavarGuc += 30;
                    break;

                case "Ay Yıldız Saç":
                    maxHp += 1000;
                    break;
                //KEMERLER
                //"Keten Kemer", "Kral Kemeri", "Gölge Kemeri", "Runik Kemer", "Büyükayı Kemeri","Ruh Kemeri"
                case "Keten Kemer":
                    maxHp += (kemerDeger * 100);
                    break;

                    case "Kral Kemeri":
                    savasciSav += kemerDeger;
                    break;

                    case "Gölge Kemeri":
                    ninjaSav += kemerDeger;
                    break;

                    case "Runik Kemer":
                    suraSav += kemerDeger;
                    break;

                    case "Büyükayı Kemeri":
                    samanSav += kemerDeger;
                    break;

                    case "Ruh Kemeri":
                    canavarGuc += kemerDeger;
                    break;
                }
            }
            
        public void efsunEkle(String itemAdi, String efsun, bool enerji)
         {
            double efsunDeger = 0;

            if (itemAdi == "Kostüm")
            {
                //SALDIRI DEĞERİ,BLOK,ZEHRE KARŞI KOYMA HESABA KATILMIYOR.
                if (efsun == "Kılıç Sav" || efsun == "Bıçak Sav" || efsun == "Çiftel Sav" || efsun == "Ok Sav" ||
                efsun == "Büyü Sav")
                {
                    efsunDeger = 10;
                }

                else if (efsun == "Canlılık" || efsun == "Zeka" || efsun == "Güç" || efsun == "Çeviklik"
                 || efsun == "Savaşçı Sav" || efsun == "Ninja Sav" || efsun == "Sura Sav" || efsun == "Şaman Sav")
                {
                    efsunDeger = 5;
                }

                else if (efsun == "Max HP")
                {
                    efsunDeger = 500;
                }

                else if (efsun == "Kritik" || efsun == "Delici" || efsun == "Yarı Güç" || efsun == "Canavarlara Karşı Güç"
                || efsun == "Savaşçı Güç" || efsun == "Ninja Güç" || efsun == "Sura Güç" || efsun == "Şaman Güç")
                {
                    efsunDeger = 10;
                }

                else if (efsun == "Büyü Hızı" || efsun == "Oklardan Korunma Şansı")
                {
                    efsunDeger = 15;
                }

                else if (efsun == "Hp Üretimi")
                {
                    efsunDeger = 30;
                }

                else
                {
                    efsunDeger = 0;
                }
            }
            else if (itemAdi == "Tengu Maskesi" || itemAdi == "Ay Yıldız Saç" || itemAdi == "Fiş")
            {
                if (efsun == "Büyü Sav")
                {
                    efsunDeger = 10;
                }

                else if (efsun == "Canlılık" || efsun == "Zeka" || efsun == "Güç" || efsun == "Çeviklik"
                 || efsun == "Savaşçı Sav" || efsun == "Ninja Sav" || efsun == "Sura Sav" || efsun == "Şaman Sav")
                {
                    efsunDeger = 5;
                }

                else if (efsun == "Max HP")
                {
                    efsunDeger = 500;
                }

                else if (efsun == "Kritik" || efsun == "Delici" || efsun == "Yarı Güç" || efsun == "Canavarlara Karşı Güç"
                || efsun == "Savaşçı Güç" || efsun == "Ninja Güç" || efsun == "Sura Güç" || efsun == "Şaman Güç")
                {
                    efsunDeger = 10;
                }

                else if (efsun == "Oklardan Korunma Şansı")
                {
                    efsunDeger = 15;
                }

                else if (efsun == "Hp Üretimi")
                {
                    efsunDeger = 30;
                }

                else
                {
                    efsunDeger = 0;
                }
            }
            else if (itemAdi == "Evcil")
            {
                //EKLEMELER YAPILACAK.
                if (efsun == "Büyü Hızı")
                {
                    efsunDeger += 17;
                }

                else if (efsun == "Savaşçı Sav" || efsun == "Ninja Sav" || efsun == "Sura Sav" || efsun == "Şaman Sav")
                {
                    efsunDeger += 11;
                }

                else if (efsun == "Savaşçı Güç" || efsun == "Ninja Güç" || efsun == "Sura Güç" || efsun == "Şaman Güç"
                    || efsun == "Canavarlara Karşı Güç")
                {
                    efsunDeger += 11;
                }

                else if (efsun == "Delici")
                {
                    efsunDeger += 10;
                }

                else
                {
                    efsunDeger += 0;
                }

            }
            else
            {
                if (efsun == "Oklardan Korunma Şansı" || efsun == "Kılıç Sav" || efsun == "Bıçak Sav" || efsun == "Çiftel Sav"
                || efsun == "Ok Sav" || efsun == "Büyü Sav" || efsun == "Zehre Karşı Koyma" || efsun == "Blok"
                || efsun == "Savaşçı Sav" || efsun == "Ninja Sav" || efsun == "Sura Sav" || efsun == "Şaman Sav")
                {
                    efsunDeger = 15;
                }

                else if (efsun == "Canlılık" || efsun == "Zeka" || efsun == "Güç" || efsun == "Çeviklik" || efsun == "Büyü Hızı"
                || efsun == "Kritik" || efsun == "Delici" || efsun == "Yarı Güç" || efsun == "Hp Üretimi"
                    || efsun == "Savaşçı Güç" || efsun == "Ninja Güç" || efsun == "Sura Güç" || efsun == "Şaman Güç"
                    || efsun == "Saldırı Değeri" || efsun == "Canavarlara Karşı Güç")
                {
                    efsunDeger = 25;
                }

                else if (efsun == "Max HP")
                {
                    efsunDeger = 2500;
                }
            }

                if (enerji == true)
                {
                    efsunDeger += efsunDeger * 0.1;
                }

                switch (efsun)
                {
                    case "Max HP":
                        maxHp += efsunDeger;
                        break;

                    case "Canlılık":
                        canlilik += efsunDeger;
                        break;

                    case "Zeka":
                        zeka += efsunDeger;
                        break;

                    case "Güç":
                        guc += efsunDeger;
                        break;

                    case "Çeviklik":
                        ceviklik += efsunDeger;
                        break;

                    case "Büyü Hızı":
                        buyuHiz += efsunDeger;
                        break;

                    case "Kritik":
                        kritik += efsunDeger;
                        break;

                    case "Delici":
                        delici += efsunDeger;
                        break;

                    case "Yarı Güç":
                        yariGuc += efsunDeger;
                        break;

                    case "Blok":
                        blok += efsunDeger;
                        break;

                    case "Oklardan Korunma Şansı":
                        oklardanKorunma += efsunDeger;
                        break;

                    case "Kılıç Sav":
                        kilicSav += efsunDeger;
                        break;

                    case "Bıçak Sav":
                        bicakSav += efsunDeger;
                        break;

                    case "Çiftel Sav":
                        ciftelSav += efsunDeger;
                        break;

                    case "Ok Sav":
                        okSav += efsunDeger;
                        break;

                    case "Büyü Sav":
                        buyuSav += efsunDeger;
                        break;

                    case "Zehre Karşı Koyma":
                        zehreKarsiKoyma += efsunDeger;
                        break;

                    case "Hp Üretimi":
                        hpUretimi += efsunDeger;
                        break;

                    case "Savaşçı Sav":
                        savasciSav += efsunDeger;
                        break;

                    case "Ninja Sav":
                        ninjaSav += efsunDeger;
                        break;

                    case "Sura Sav":
                        suraSav += efsunDeger;
                        break;

                    case "Şaman Sav":
                        samanSav += efsunDeger;
                        break;
    
                    case "Savaşçı Güç":
                        savasciGuc += efsunDeger;
                        break;

                    case "Ninja Güç":
                        ninjaGuc += efsunDeger;
                        break;

                    case "Sura Güç":
                        suraGuc += efsunDeger;
                        break;

                    case "Şaman Güç":
                        samanGuc += efsunDeger;
                        break;

                    case "Saldırı Değeri":
                        saldiriDegeri += efsunDeger;
                        break;

                    case "Canavarlara Karşı Güç":
                        canavarGuc += efsunDeger;
                        break;
                }
            }

        public void biyologGoreviYap()
        {
            savasciSav += 10;
            ninjaSav += 10;
            suraSav += 10;
            samanSav += 10;
            savasciGuc += 10;
            ninjaGuc += 10;
            suraGuc += 10;
            samanGuc += 10;
        }

        public void somuru()
        {
            maxHp += 1666;
        }
        public void korunmaTasi()
        {
            blok += 15;
        }

        public void yasamTasi()
        {
            maxHp += 1000;
        }

        public void kurnazlikTasi()
        {
            oklardanKorunma += 15;
        }

        public void atesAnkasi()
        {
            okSav += 20;
        }

        public void kritikTasi()
        {
            kritik += 15;
        }

        public void deliciTasi()
        {
            delici += 15;
        }

        public void Wayne()
        {
            kritik += 11;
            delici += 11;
        }

        public void oranKutularıGuncelle()
        {
            textKilic.Text = Convert.ToString(kilicSav);
            textBicak.Text = Convert.ToString(bicakSav);
            textCiftEl.Text = Convert.ToString(ciftelSav);
            textBuyuSav.Text = Convert.ToString(buyuSav);
            textOkSav.Text = Convert.ToString(okSav);
            textBlok.Text = Convert.ToString(blok);
            textSavascıSav.Text = Convert.ToString(savasciSav);
            textNinjaSav.Text = Convert.ToString(ninjaSav);
            textSuraSav.Text = Convert.ToString(suraSav);
            textSamanSav.Text = Convert.ToString(samanSav);
            textHp.Text = Convert.ToString(maxHp);
            textCanlılık.Text = Convert.ToString(canlilik);
            textKritik.Text = Convert.ToString(kritik);
            textDelici.Text = Convert.ToString(delici);
            textBuyuHız.Text = Convert.ToString(buyuHiz);
            textZeka.Text = Convert.ToString(zeka);
            textGuc.Text = Convert.ToString(guc);
            textCeviklik.Text = Convert.ToString(ceviklik);
            textHpUretimi.Text = Convert.ToString(hpUretimi);
            textSavasciGuc.Text = Convert.ToString(savasciGuc);
            textNinjaGuc.Text = Convert.ToString(ninjaGuc);
            textSuraGuc.Text = Convert.ToString(suraGuc);
            textSamanGuc.Text = Convert.ToString(samanGuc);
            textYariGuc.Text = Convert.ToString(yariGuc);
            textZehreKarsiDayaniklilik.Text = Convert.ToString(zehreKarsiKoyma);
            textOklardanKorunma.Text = Convert.ToString(oklardanKorunma);
            textSaldiriDegeri.Text = Convert.ToString(saldiriDegeri);
            textCanavarGuc.Text = Convert.ToString(canavarGuc);
        }

    }

}