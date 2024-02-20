using FinalProgramacion2023.Datos;
using FinalProgramacion2023.Entidades;

namespace FinalProgramacion2023.Windows
{
    public partial class frmPrincipal : Form
    {
        private RepositorioDeCuadrilateros repo;
        private List<Cuadrilatero> lista;
        int intValor;
        bool filterOn = false;
        public frmPrincipal()
        {
            InitializeComponent();
            repo = new RepositorioDeCuadrilateros();
            ActualizarCantidadRegistros();
        }

        private void ActualizarCantidadRegistros()
        {
            if (intValor > 0)
            {
                txtCantidad.Text = repo.GetCantidad(intValor).ToString();

            }
            else
            {
                txtCantidad.Text = repo.GetCantidad().ToString();

            }
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void SetearFila(DataGridViewRow r, Cuadrilatero cuadrilatero)
        {
            r.Cells[colLadoA.Index].Value = cuadrilatero.GetLadoA();
            r.Cells[colLadoB.Index].Value = cuadrilatero.GetLadoB();
            r.Cells[colBorde.Index].Value = cuadrilatero.TipoDeBorde;
            r.Cells[colRelleno.Index].Value = cuadrilatero.ColorRelleno;
            r.Cells[colSuperficie.Index].Value = cuadrilatero.GetSuperficie();
            r.Cells[colPerimetro.Index].Value = cuadrilatero.GetPerimetro();

            r.Tag = cuadrilatero;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmCuadrilatero frm = new frmCuadrilatero() { Text = "Agregar Cuadrilátero" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            Cuadrilatero cuadrilatero = frm.GetCuadrilatero();
            if (!repo.Existe(cuadrilatero))
            {
                repo.Agregar(cuadrilatero);
                txtCantidad.Text = repo.GetCantidad().ToString();

                DataGridViewRow r = ConstruirFila();
                SetearFila(r, cuadrilatero);
                AgregarFila(r);

                MessageBox.Show("Registro agregado", "Mensaje", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Registro existente", "Error", MessageBoxButtons.OK,
        MessageBoxIcon.Error);

            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
           

        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {

        }
    }
}
