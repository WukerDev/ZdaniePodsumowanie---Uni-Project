using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZdaniePodsumowanie
{
    public partial class Form1 : Form
    {
        int selected; 
        public Form1()
        {
            InitializeComponent();
            loadGrid(); //ładujemy siatkę
            string[] tablica = { "Gdańsk", "Warszawa", "Laskowice", "Zawada" }; //ustalamy tablicę w której na sztywno mamy miasta
            comboBox1.Items.AddRange(tablica); //dodajemy "zasięg' comboBoxa żeby był równy wartości tabeli "tablica" oraz przypisujemy mu wartości tej tablicy
            
        }

        public void loadGrid() //funkcja ładująca nam tablicę
        {
            using (var db = new obsluga()) //nowa baza danych zawierające dane obsluga() 
            {

                var query = from p in db.pomiar  //wybieramy dane z tabeli pomiar i przypisujemy ich wartość do zmiennej p
                            join s in db.sensor on p.FK_Sensor equals s.IDSensor  //łączymy tabelę sensor z tabelą pomiar i przypisujemy wartość klucza obcego, równą do wartości IDsensor w tabeli Sensor
                            select new
                            {
                                s.NazwaSensora,  //wczytujemy wartości do gridView, s oznacza tabelę sensor zaś p oznacza tabelę pomiar
                                p.VarWysokosc,  // po kropce wpisujemy DOKŁADNĄ nazwę jaką mamy w bazie danych
                                p.VarSzerokosc
                            };

                dataGridView1.DataSource = query.ToList();  //przypisujemy wartość query przerobionego na listę do GridView za pomocą .DataSource
            }
            dataGridView1.Columns[0].HeaderText = "Lokacja sensora";  //ustalamy nagłówki gridView
            dataGridView1.Columns[1].HeaderText = "Wysokość";
            dataGridView1.Columns[2].HeaderText = "Szerokość";
            

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                //missclick, tu się nic nie dzieje
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) { selected = 1; };  //tutaj przypisujemy wartości combo boxa do wartości w bazie danych. 
            if (comboBox1.SelectedIndex == 1) { selected = 2; };  // zmieniamy wartość z combo boxa na jeden wyższą bo program liczy od 0 a baza od 1
            if (comboBox1.SelectedIndex == 2) { selected = 3; };
            if (comboBox1.SelectedIndex == 3) { selected = 4; };
        }

        private void Zapis_Click(object sender, EventArgs e) // zapisujemy zmiany w bazie
        {
            int wysokosc = Int32.Parse(textBox1.Text);  //wartość wysokość przechowuje wartość textboxa1 zmienionego na int za pomocą Parse
            int szerokosc = Int32.Parse(textBox2.Text); //wartość szerokość przechowuje wartość textboxa2 zmienionego na int za pomocą Parse

            using (var db = new obsluga()) //nowa baza danych zawierające dane obsluga() 
            {
                pomiar pomiar = new pomiar  //ustalamy nowy pomiar
                {
                    VarWysokosc = wysokosc,  //przypisujemy wartość z programu(po prawej) do zmiennej z tabeli (po lewo, nazwa musi się zgadzać)
                    VarSzerokosc = szerokosc, //przypisujemy wartość z programu(po prawej) do zmiennej z tabeli (po lewo, nazwa musi się zgadzać)
                    FK_Sensor = selected,  //przypisujemy wartość z programu(po prawej) do zmiennej z tabeli (po lewo, nazwa musi się zgadzać)

                };
                db.pomiar.Add(pomiar); //dodajemy do tablicy w bazie danych pomiar wartość nowego pomiaru wyżej
                db.SaveChanges();  //zapisujemy zmiany
            }
            loadGrid(); //ładujemy grid view
        }
    }
}
