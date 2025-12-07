using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VideoKlubApp
{
    public partial class FormZaduzenja : Form
    {
        public FormZaduzenja()
        {
            InitializeComponent();
            this.Load += FormZaduzenja_Load;
        }

        private void FormZaduzenja_Load(object sender, EventArgs e)
        {
            UcitajKlijente();
            UcitajFilmove();
            UcitajZaduzenja();
        }

        private void UcitajKlijente()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Id, Ime + ' ' + Prezime AS PunoIme FROM Klijenti ORDER BY Prezime";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboKlijent.DataSource = dt;
                    comboKlijent.DisplayMember = "PunoIme";
                    comboKlijent.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju klijenata: " + ex.Message);
            }
        }

        private void UcitajFilmove()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Id, Naziv + ' (' + CAST(Godina AS VARCHAR(4)) + ')' AS NazivGodina FROM Filmovi ORDER BY Naziv";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboFilm.DataSource = dt;
                    comboFilm.DisplayMember = "NazivGodina";
                    comboFilm.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju filmova: " + ex.Message);
            }
        }

        private void UcitajZaduzenja()
        {
            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            z.Id,
                            k.Ime + ' ' + k.Prezime AS Klijent,
                            f.Naziv AS Film,
                            f.Godina,
                            f.Zanr,
                            z.DatumZaduzenja
                        FROM Zaduzenja z
                        JOIN Klijenti k ON z.KlijentId = k.Id
                        JOIN Filmovi f ON z.FilmId = f.Id
                        ORDER BY z.DatumZaduzenja DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;

                    // Formatiranje datuma
                    if (dataGridView1.Columns["DatumZaduzenja"] != null)
                    {
                        dataGridView1.Columns["DatumZaduzenja"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju zaduženja: " + ex.Message);
            }
        }

        private void btnZaduzi_Click(object sender, EventArgs e)
        {
            if (comboKlijent.SelectedValue == null || comboFilm.SelectedValue == null)
            {
                MessageBox.Show("Izaberite klijenta i film!");
                return;
            }

            int klijentId = (int)comboKlijent.SelectedValue;
            int filmId = (int)comboFilm.SelectedValue;
            string klijent = comboKlijent.Text;
            string film = comboFilm.Text;

            try
            {
                using (SqlConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    // Provjera da li je film već zadužen
                    string provjeraSql = "SELECT COUNT(*) FROM Zaduzenja WHERE FilmId = @FilmId";
                    SqlCommand provjeraCmd = new SqlCommand(provjeraSql, conn);
                    provjeraCmd.Parameters.AddWithValue("@FilmId", filmId);
                    int brojZaduzenja = (int)provjeraCmd.ExecuteScalar();

                    if (brojZaduzenja > 0)
                    {
                        MessageBox.Show("Ovaj film je već zadužen!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Unos zaduženja
                    string unosSql = @"INSERT INTO Zaduzenja (KlijentId, FilmId, DatumZaduzenja) 
                                       VALUES (@KlijentId, @FilmId, GETDATE())";
                    SqlCommand unosCmd = new SqlCommand(unosSql, conn);
                    unosCmd.Parameters.AddWithValue("@KlijentId", klijentId);
                    unosCmd.Parameters.AddWithValue("@FilmId", filmId);

                    int rows = unosCmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show($"Film '{film}' zadužen klijentu '{klijent}'!",
                                        "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UcitajZaduzenja();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVrati_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Izaberite zaduženje za vraćanje!");
                return;
            }

            int zaduzenjeId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            string klijent = dataGridView1.CurrentRow.Cells["Klijent"].Value.ToString();
            string film = dataGridView1.CurrentRow.Cells["Film"].Value.ToString();

            DialogResult result = MessageBox.Show($"Vratiti film '{film}' od klijenta '{klijent}'?",
                                                  "Potvrda vraćanja",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DBHelper.GetConnection())
                    {
                        conn.Open();
                        string sql = "DELETE FROM Zaduzenja WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Id", zaduzenjeId);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Film vraćen!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UcitajZaduzenja();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}