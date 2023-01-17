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
            loadGrid();
            string[] tablica = { "Gdańsk", "Warszawa", "Laskowice", "Zawada" };
            comboBox1.Items.AddRange(tablica);
            
        }

        public void loadGrid()
        {
            using (var db = new obsluga())
            {

                var query = from p in db.pomiar
                            join s in db.sensor on p.FK_Sensor equals s.IDSensor
                            select new
                            {
                                s.NazwaSensora,
                                p.VarWysokosc,
                                p.VarSzerokosc
                            };

                dataGridView1.DataSource = query.ToList();
            }
            dataGridView1.Columns[0].HeaderText = "Lokacja sensora";
            dataGridView1.Columns[1].HeaderText = "Wysokość";
            dataGridView1.Columns[2].HeaderText = "Szerokość";
            

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) { selected = 1; };
            if (comboBox1.SelectedIndex == 1) { selected = 2; };
            if (comboBox1.SelectedIndex == 2) { selected = 3; };
            if (comboBox1.SelectedIndex == 3) { selected = 4; };
        }

        private void Zapis_Click(object sender, EventArgs e)
        {
            int wysokosc = Int32.Parse(textBox1.Text);
            int szerokosc = Int32.Parse(textBox2.Text);

            using (var db = new obsluga())
            {
                pomiar pomiar = new pomiar
                {
                    VarWysokosc = wysokosc,
                    VarSzerokosc = szerokosc,
                    FK_Sensor = selected,

                };
                db.pomiar.Add(pomiar);
                db.SaveChanges();
            }
            loadGrid();
        }
    }
}
