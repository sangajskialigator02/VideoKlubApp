using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VideoKlubApp
{
    public partial class FormFilmovi : Form
    {
        public FormFilmovi()
        {
            InitializeComponent();

            // Ručno poveži event handlere
            this.Load += FormFilmovi_Load;

            // Pronađi dugmad po imenu i poveži ih
            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    if (button.Name == "btnDodaj" || button.Text == "Dodaj Film")
                        button.Click += btnDodaj_Click;
                    else if (button.Name == "btnObrisi" || button.Text == "Obriši Film")
                        button.Click += btnObrisi_Click;
                }
            }
        }

        private void FormFilmovi_Load(object sender, EventArgs e)
        {
            LoadFilmovi();
        }

        private void LoadFilmovi()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM Filmovi ORDER BY Naziv";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;

                    // Sakrij ID kolonu ako želiš
                    if (dataGridView1.Columns["Id"] != null)
                        dataGridView1.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju: " + ex.Message);
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            // Pronađi textbox-ove
            TextBox txtNaziv = null, txtGodina = null, txtZanr = null;

            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.Name == "txtNaziv") txtNaziv = textBox;
                    else if (textBox.Name == "txtGodina") txtGodina = textBox;
                    else if (textBox.Name == "txtZanr") txtZanr = textBox;
                }
            }

            // Ako nisu pronađeni po imenu, traži po redoslijedu
            if (txtNaziv == null || txtGodina == null || txtZanr == null)
            {
                int textBoxCount = 0;
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBoxCount++;
                        if (textBoxCount == 1) txtNaziv = textBox;
                        else if (textBoxCount == 2) txtGodina = textBox;
                        else if (textBoxCount == 3) txtZanr = textBox;
                    }
                }
            }

            // Validacija
            if (txtNaziv == null || string.IsNullOrWhiteSpace(txtNaziv.Text))
            {
                MessageBox.Show("Unesite naziv filma!");
                return;
            }

            if (txtGodina == null || string.IsNullOrWhiteSpace(txtGodina.Text) || !int.TryParse(txtGodina.Text, out int godina))
            {
                MessageBox.Show("Unesite ispravnu godinu!");
                return;
            }

            if (txtZanr == null || string.IsNullOrWhiteSpace(txtZanr.Text))
            {
                MessageBox.Show("Unesite žanr!");
                return;
            }

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO Filmovi (Naziv, Godina, Zanr) 
                                   VALUES (@Naziv, @Godina, @Zanr)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Naziv", txtNaziv.Text);
                    cmd.Parameters.AddWithValue("@Godina", godina);
                    cmd.Parameters.AddWithValue("@Zanr", txtZanr.Text);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show($"Film '{txtNaziv.Text}' uspješno dodan!");

                        // Očisti polja
                        txtNaziv.Clear();
                        txtGodina.Clear();
                        txtZanr.Clear();

                        // Osvježi prikaz
                        LoadFilmovi();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Izaberite film za brisanje!");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            string naziv = dataGridView1.CurrentRow.Cells["Naziv"].Value.ToString();

            DialogResult result = MessageBox.Show($"Obrisati film '{naziv}'?",
                                                  "Potvrda",
                                                  MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();
                        string sql = "DELETE FROM Filmovi WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Film obrisan!");
                        LoadFilmovi();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška: " + ex.Message);
                }
            }
        }
    }
}