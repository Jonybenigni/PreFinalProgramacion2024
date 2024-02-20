using FinalProgramacion2023.Entidades;
using System.Windows.Forms;

namespace FinalProgramacion2023.Windows
{
    public partial class frmCuadrilatero : Form
    {
        private Cuadrilatero cuadrilatero;

        public Cuadrilatero GetCuadrilatero()
        {
            return cuadrilatero;
        }
        public frmCuadrilatero()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarDatosComboColorRelleno();
            if (cuadrilatero != null)
            {
                txtLadoA.Text = cuadrilatero.GetLadoA().ToString();
                cboRelleno.SelectedItem = cuadrilatero.ColorRelleno;
                if (cuadrilatero.TipoDeBorde == Borde.Lineal)
                {
                    rbtLineal.Checked = true;
                }
                else if (cuadrilatero.TipoDeBorde == Borde.Rayas)
                {
                    rbtRayas.Checked = true;
                }
                else
                {
                    rbtPuntos.Checked = true;
                }
            }
        }
        private void frmCuadrilatero_Load(object sender, EventArgs e)
        {

        }
        private void CargarDatosComboColorRelleno()
        {
            var listaColores = Enum.GetValues(typeof(Entidades.Color)).Cast<Entidades.Color>().ToList();
            cboRelleno.DataSource = listaColores;
            cboRelleno.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (cuadrilatero == null)
                {
                    cuadrilatero = new Cuadrilatero()
;
                }
                cuadrilatero.SetLadoA(int.Parse(txtLadoA.Text));
                cuadrilatero.ColorRelleno = (Entidades.Color)cboRelleno.SelectedItem;
                if (rbtLineal.Checked)
                {
                    cuadrilatero.TipoDeBorde = Borde.Lineal;
                }
                else if (rbtRayas.Checked)
                {
                    cuadrilatero.TipoDeBorde = Borde.Rayas;
                }
                else
                {
                    cuadrilatero.TipoDeBorde = Borde.Puntos;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (!int.TryParse(txtLadoA.Text, out int lado))
            {
                valido = false;
                errorProvider1.SetError(txtLadoA, "Número mal ingresado");

            }
            else if (lado <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtLadoA, "Valor del lado no válido");
            }
            return valido;
        }
    }
}
