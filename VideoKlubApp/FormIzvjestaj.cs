using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VideoKlubApp
{
    public partial class FormIzvjestaj : Form
    {
        public FormIzvjestaj()
        {
            InitializeComponent();
            this.Load += FormIzvjestaj_Load;
        }

        private void FormIzvjestaj_Load(object sender, EventArgs e)
        {
            UcitajZaduzenja();
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
                            k.Ime + ' ' + k.Prezime AS [Klijent],
                            f.Naziv AS [Film],
                            f.Godina,
                            f.Zanr,
                            FORMAT(z.DatumZaduzenja, 'dd.MM.yyyy HH:mm') AS [Datum zaduženja],
                            DATEDIFF(day, z.DatumZaduzenja, GETDATE()) AS [Dana zaduženo]
                        FROM Zaduzenja z
                        JOIN Klijenti k ON z.KlijentId = k.Id
                        JOIN Filmovi f ON z.FilmId = f.Id
                        ORDER BY z.DatumZaduzenja DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;

                    // Podesi širine kolona
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].Width = 150; // Klijent
                        dataGridView1.Columns[1].Width = 200; // Film
                        dataGridView1.Columns[2].Width = 70;  // Godina
                        dataGridView1.Columns[3].Width = 100; // Žanr
                        dataGridView1.Columns[4].Width = 150; // Datum
                        dataGridView1.Columns[5].Width = 120; // Dana zaduženo
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju izvještaja: " + ex.Message);
            }
        }

        // Opcionalno: dugme za osvježavanje
        private void btnOsvjezi_Click(object sender, EventArgs e)
        {
            UcitajZaduzenja();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}