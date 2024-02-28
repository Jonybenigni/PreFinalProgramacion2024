using FinalProgramacion2023.Datos;
using FinalProgramacion2023.Entidades;
using Color = System.Drawing.Color;

namespace FinalProgramacion2023.Windows
{
    public partial class frmPrincipal : Form
    {
        private RepositorioCuadrilatero repo;
        private List<Cuadrilatero> lista;
        int intValor;
        int intArea;
        bool filterOn = false;
        public frmPrincipal()
        {
            InitializeComponent();
            repo = new RepositorioCuadrilatero();
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
            r.Cells[colBorde.Index].Value = cuadrilatero.Borde;
            r.Cells[colRelleno.Index].Value = cuadrilatero.Relleno;
            r.Cells[colSuperficie.Index].Value = cuadrilatero.GetArea();
            r.Cells[colPerimetro.Index].Value = cuadrilatero.GetPerimetro();
            r.Cells[coltipo.Index].Value = cuadrilatero.TipoCuadrilatero();

            r.Tag = cuadrilatero;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmCuadrilatero frm = new frmCuadrilatero() { Text = "Agregar Cuadrilatero" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {

                return;

            }
            Cuadrilatero cuadrilatero = frm.GetCuadrilatero();
            if (!repo.CuadrilateroExiste(cuadrilatero))
            {
                repo.Agregar(cuadrilatero);
                txtCantidad.Text = repo.GetCantidad().ToString();
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, cuadrilatero);
                AgregarFila(r);
                MessageBox.Show("Registro Agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Registro Existente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void QuitarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Remove(r);
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("¿Desea dar de baja el Cuadrilatero?",
                "Confirmar Baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {

                return;

            }
            var filaseleccionada = dgvDatos.SelectedRows[0];
            Cuadrilatero cuadrilatero = filaseleccionada.Tag as Cuadrilatero;
            repo.Borrar(cuadrilatero);
            txtCantidad.Text = repo.GetCantidad().ToString();
            SacarFila(filaseleccionada);
            MessageBox.Show("Registro borrado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var filaSeleccionada = dgvDatos.SelectedRows[0];
            Cuadrilatero cuadrilatero = (Cuadrilatero)filaSeleccionada.Tag;
            Cuadrilatero cuadrilateroCopia = (Cuadrilatero)cuadrilatero.Clone();
            frmCuadrilatero frm = new frmCuadrilatero() { Text = "Editar Cuadrilatero" };
            frm.SetCuadrilatero(cuadrilatero);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {

                return;

            }
            cuadrilatero = frm.GetCuadrilatero();
            if (!repo.CuadrilateroExiste(cuadrilatero))
            {
                repo.Editar(cuadrilateroCopia, cuadrilatero);
                SetearFila(filaSeleccionada, cuadrilatero);
                MessageBox.Show("Registro editado", "Mensaje", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            }
            else
            {
                SetearFila(filaSeleccionada, cuadrilateroCopia);
                MessageBox.Show("Registro existente", "Error", MessageBoxButtons.OK,
              MessageBoxIcon.Error);
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {

            lista = repo.GetLista();
            MostrarDatosEnGrilla();
            ActualizarCantidadRegistros();
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (var cuadrilatero in lista)
            {
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, cuadrilatero);
                AgregarFila(r);
            }
        }
        private void SacarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Remove(r);
        }
        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (repo.GetCantidad() > 0)
            {
                lista = repo.GetLista();
                MostrarDatosEnGrilla();
            }
        }
        private void CargarDatosComboBordes()
        {
            var listaBordes = Enum.GetValues(typeof(Borde))
                .Cast<Borde>().ToList();
            foreach (var itemBorde in listaBordes)
            {
                toolStripComboBox1.Items.Add(itemBorde);
            }
            toolStripComboBox1.SelectedIndex = -1;
        }

        private void tsbFiltrar_Click(object sender, EventArgs e)
        {

        }

        private void ascendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repo.OrdenarAscendente();
            MostrarDatosEnGrilla();
            ActualizarCantidadRegistros();
        }

        private void descendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repo.OrdenarDescendente();
            MostrarDatosEnGrilla();
            ActualizarCantidadRegistros();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!filterOn)
            {
                if (toolStripComboBox1.SelectedIndex == -1)
                {
                    return;
                }
                var bordeFiltro = (Borde)toolStripComboBox1.SelectedItem;
                lista = repo.Filtrar((int)bordeFiltro);
                MostrarDatosEnGrilla();
                filterOn = true;
                tsbFiltrar.BackColor = Color.Orange;

            }
        }
        private void tsbCboArea_Click(object sender, EventArgs e)
        {

        }



        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
