using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Squad_Rus
{
    public partial class Form1 : Form
    {
        private static readonly string putKonfiga = "config.ini";
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            string posledniyPut = ZagruzitPut();
            textBox1.Text = posledniyPut;
            label2.Visible = false;
            label4.Visible = false;
            progressBar1.Value = 0;

            linkLabel1.Text = "By Salutik";
            linkLabel1.Links.Add(0, 0,"https://github.com/Sa1utik");
        }
        public string poisk()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filePath = Path.Combine(localAppData, @"SquadGame\Saved\Config\WindowsNoEditor\GameUserSettings.ini");
            return filePath;
        }

        private void btnPoisk_Click(object sender, EventArgs e)
        {
            string filePath = poisk();
            if (File.Exists(filePath))
            {
                // Файл найден, можно с ним работать
                //MessageBox.Show("Файл найден:\n" + filePath);
                textBox1.Text = filePath;
            }
            else
            {
                MessageBox.Show("Файл не найден:\n" + filePath);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "INI files (*.ini)|*.ini|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите .ini файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Путь к файлу вставляем в TextBox (предположим, его имя textBox1)
                    textBox1.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string posledniyPut = textBox1.Text;
            string putFaila;

            putFaila = textBox1.Text;

            if (comboBox1.SelectedIndex == 0)
            {
                if (string.IsNullOrWhiteSpace(putFaila))
                {
                    if (!string.IsNullOrEmpty(posledniyPut))
                    {
                        putFaila = posledniyPut;
                    }
                    else
                    {
                        label4.Visible = true;
                        MessageBox.Show("Путь не указан.");
                        return;
                    }
                }
                else
                {
                    SokhranitPut(putFaila);
                }

                try
                {
                    ZamenitStroku(putFaila, "Language=en-US", "Language=ru-RU");
                    ZamenitStroku(putFaila, "Locale=en-US", "Locale=ru-RU");
                    label4.Visible = false;
                    label2.Visible = true;
                    zagrus();
                    MessageBox.Show("Готово, игра переведена на русский");
                }
                catch (Exception oshibka)
                {
                    label4.Visible = true;
                    MessageBox.Show($"Ошибка: {oshibka.Message}");    
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(putFaila))
                {
                    if (!string.IsNullOrEmpty(posledniyPut))
                    {
                        putFaila = posledniyPut;
                    }
                    else
                    {
                        label4.Visible = true;
                        MessageBox.Show("Путь не указан.");
                        return;
                    }
                }
                else
                {
                    SokhranitPut(putFaila);
                }

                try
                {
                    ZamenitStroku(putFaila, "Language=ru-RU", "Language=en-US");
                    ZamenitStroku(putFaila, "Locale=ru-RU", "Locale=en-US");
                    label4.Visible = false;
                    label2.Visible = true;
                    zagrus();
                    MessageBox.Show("Готово, игра переведена на английский");   
                }
                catch (Exception oshibka)
                {
                    label4.Visible = true;
                    MessageBox.Show($"Ошибка: {oshibka.Message}");
                }
            }
        }
        
            




        public async void zagrus()
        {
            for (int i = 0; i < 101; i++)
            {
                progressBar1.Value = i;
            }

        }

        static string ZagruzitPut()
        {
            if (File.Exists(putKonfiga))
                return File.ReadAllText(putKonfiga);
            return string.Empty;
        }

        static void SokhranitPut(string put)
        {
            File.WriteAllText(putKonfiga, put);
        }

        static void ZamenitStroku(string putFaila, string chtoIskat, string naChtoZamenit)
        {
            string[] stroki = File.ReadAllLines(putFaila);
            bool naydena = false;

            for (int i = 0; i < stroki.Length; i++)
            {
                if (stroki[i] == chtoIskat)
                {
                    stroki[i] = naChtoZamenit;
                    naydena = true;
                }
            }

            if (naydena)
            {
                File.WriteAllLines(putFaila, stroki);
                Console.WriteLine($"Строка \"{chtoIskat}\" заменена на \"{naChtoZamenit}\"");
            }
            else
            {
                Console.WriteLine($"Строка \"{chtoIskat}\" не найдена");
                Console.WriteLine("Всё и так на русском");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }
    }
}
