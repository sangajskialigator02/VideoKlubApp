using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VideoKlubApp
{
    public partial class FormKlijenti : Form
    {
        public FormKlijenti()
        {
            InitializeComponent();
            LoadKlijenti();
        }

        private void LoadKlijenti()
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Klijenti", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text) || string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                MessageBox.Show("Unesite i ime i prezime.");
                return;
            }

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Klijenti (Ime, Prezime) VALUES (@ime, @prezime)", conn);
                cmd.Parameters.AddWithValue("@ime", txtIme.Text);
                cmd.Parameters.AddWithValue("@prezime", txtPrezime.Text);
                cmd.ExecuteNonQuery();
            }

            txtIme.Clear();
            txtPrezime.Clear();
            LoadKlijenti();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            DialogResult dr = MessageBox.Show("Da li ste sigurni?", "Brisanje klijenta", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Klijenti WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadKlijenti();
            }
        }
    }
}