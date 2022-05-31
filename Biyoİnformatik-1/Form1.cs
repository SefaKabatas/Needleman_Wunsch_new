using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Biyoİnformatik_1
{
    public partial class Form1 : Form
    {
   
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*
             Belirli bir kural kümesi için diziler arasında en uygun küresel hizalamayı bulun.
        Üç adım vardır:
        1. Başlatma adımı
        2. Matris doldurma adımı
        3. Geri izleme adımı

        1. Birinci dizinin uzunluğu, ikinci dizinin uzunluğu için beş değişken girin
        sıra, eşleşme, uyumsuzluk ve boşluk.
        2.  birinci ve ikinci dizi uzunluğu + 1 olan iki matris tanımlayın ikincisi geri izleme için.
        3. Her iki matrisi de puan değerine göre doldurun (maksimum puan değeri opt).
        4. Geri izleme matrisinde, köşegen için D, sol için S, üst puan için Y kullanın
        5. Verileri GRIVİEW görünümünde görselleştirmek için her iki matrisi de veri tablosuna dönüştürün.
        6. Her iki hizalanmış diziyi etiketlerde görüntülemek için geri izleme işlevini çağırın.
        7. Değer D ise, o zaman her iki dizinin her iki karakterini de yazın;
        aksi takdirde, S ve Y skor değerleri durumunda, sadece bir karakter yazıyoruz.
        karşılık gelen dizi.
        */

            int gen_1 = textBox1.Text.Length + 1; //GEN 1
            int gen_2 = textBox2.Text.Length + 1; //GEN2
            if (textBox1.Text.Trim() == string.Empty || textBox2.Text.Trim() == string.Empty)//boş mu kontrol
            {
                MessageBox.Show("Gen1 ya da gen2 boş"); // hata mesajı göster

            }
            else //değilse
            {

                try //hata yakalama
                {
                    int eslesme = Convert.ToInt32(textBox3.Text); //MATCHT
                    int esleme_olmadı = Convert.ToInt32(textBox4.Text); //DiSSMATCHT
                    int bosluk = Convert.ToInt32(textBox5.Text); //GAP
                    int[,] sonuc = new int[gen_1, gen_2]; //SONUCU DİZİDE TUTLA
                    char[,] izlemeMat = new char[gen_1, gen_2]; //SONUCU YAZDIRMAYI DİZİDE TUTMA
                    sonuc[0, 0] = 0; //DİZİ İÇİN BAŞLANGIÇ ATAMASI
                    for (int i = 1; i < gen_1; i++) //Döngü 1 den başlıyor çünkü gen+1
                    {
                        sonuc[i, 0] = i * bosluk; //sonuc'a dikey kısmının boşluk ile çarpımını ekle
                        izlemeMat[i, 0] = 'Y'; // Yukarı kısmı oluyor bu
                    }
                    for (int j = 1; j < gen_2; j++) //Döngü 1 den başlıyor çünkü gen2+1
                    {
                        sonuc[0, j] = j * bosluk; ////sonuc'a dikey kısmının boşluk ile çarpımını ekle
                        izlemeMat[0, j] = 'S'; // Sol kısım oluyor bu
                    }
                    for (int i = 1; i < gen_1; i++) //i_= 1 den gen1 e kadar
                    {
                        for (int j = 1; j < gen_2; j++)//i=1 den gen2 ye kadar
                        {
                            int puanDiag = 0;
                            if (textBox2.Text.Substring(j - 1, 1) == textBox1.Text.Substring(i - 1, 1)) //j-1 den 1 kadar al i-1 den j ye kadar al birbirine eşit ise
                                puanDiag = sonuc[i - 1, j - 1] + eslesme; //puan'a sonuc i ve j değerlerini ve eşleşmeyi ekle
                            else
                                puanDiag = sonuc[i - 1, j - 1] + esleme_olmadı; //değilse eşleşme olmadıyı ekle
                            int puanSol = sonuc[i, j - 1] + bosluk;
                            int puanYukarı = sonuc[i - 1, j] + bosluk; //yukarı'ya sonuc un i-1 ve j kısmına boşlukla beraber ekle
                            int maxPuan = Math.Max(Math.Max(puanDiag, puanSol), puanYukarı);
                            //maksimum puan için puanDiag ve puan solun maxini al ve sonra çıkan sonucun puan yukarıdan hangisi yüksek ise onu al
                            sonuc[i, j] = maxPuan; //max puanı sonuca eşitle
                            if (sonuc[i, j] == puanDiag) //şayet sonuç puanDiag ile eşitse
                            {
                                izlemeMat[i, j] = 'D'; //izleme matrisi D
                               

                            }
                            else if (sonuc[i, j] == puanSol) //sola eşitse S
                            {
                                izlemeMat[i, j] = 'S';
                            }
                            else if (sonuc[i, j] == puanYukarı) //yukarıya eşitse Y
                            {
                                izlemeMat[i, j] = 'Y';
                            }

                        }
                    }
                    dataGridView1.DataSource = DiziGridViewAtla(sonuc, gen_1, gen_2); //datagridview e ekle
                    GeriIzleme(izlemeMat, textBox1.Text, textBox2.Text); //geri izlemeyi ekle
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Girmeyi unuttuğunuz bir değer var",ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //hata mesajı göster
                }
            }
        }
        public string tersYazdırma(string giris) //ters yazdırma metodu
        {
            char[] dizi = giris.ToCharArray();
            Array.Reverse(dizi); //ters çevir
            return new string(dizi); //geri döndür
        }
        public DataTable DiziGridViewAtla(int[,] maxSkor, int a, int b)
        {
            DataTable gridview = new DataTable();
            for (int j = 0; j < b; j++)
            {
                gridview.Columns.Add(j.ToString()); //datagridview'e ekle
            }
            for (int i = 0; i < a; i++)
            {
                gridview.Rows.Add();
                for (int j = 0; j < b; j++)
                    gridview.Rows[i][j] = maxSkor[i, j]; //maxskora gridview'in Row'una ekle
            }
            gridview.AcceptChanges(); //değşiklikleri kaydet
            return gridview; //geri döndür

        }
        //geri izleme metodu
        public void GeriIzleme(char[,] geri_izleme_mat, string genA, string genB)
        {
            int i = geri_izleme_mat.GetLength(0) - 1;
            int j = geri_izleme_mat.GetLength(1) - 1;
            string hizalamaA = ""; //tanımlamalar
            string hizalamaB = ""; //tanımlamalar
            while (i != 0 || j != 0) //0'a eşit oluncaya kadar dön
            {
                switch (geri_izleme_mat[i, j])
                {
                    case 'D': //geri izlemede D ise
                        hizalamaA += genA[i - 1];
                        hizalamaB += genB[j - 1];
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                        i--;
                        j--;
                        break;
                    case 'Y'://geri izlemede Yukarı ise
                        hizalamaA += genA[i - 1];
                        hizalamaB += "-";
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                        i--;
                        break;
                    case 'S'://geri izlemede Sol ise
                        hizalamaA += "-";
                        hizalamaB += genB[j - 1];
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                        j--;
                        break;
                }

            }
            label7.Text = tersYazdırma(hizalamaA); //yazdırma
            label8.Text = tersYazdırma(hizalamaB); //Yazdırma
        }
    }
}
