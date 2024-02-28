using FinalProgramacion2023.Entidades;
using System.Windows.Forms;
using Color = FinalProgramacion2023.Entidades.Color;

namespace FinalProgramacion2023.Windows
{
    public partial class frmCuadrilatero : Form
    {
        private Cuadrilatero cuadrilatero;
        public double LadoA { get; set; }
        public double LadoB { get; set; }
        
        public frmCuadrilatero()
        {
            InitializeComponent();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarDatosRelleno();
            if (cuadrilatero != null)
            {
                txtLadoA.Text = cuadrilatero.GetLadoA().ToString();
                txtLadoB.Text = cuadrilatero.GetLadoB().ToString();
                cboRelleno.SelectedItem = cuadrilatero.Relleno;
                if (cuadrilatero.Borde == Borde.Lineal)
                {
                    rbtLineal.Checked = true;
                }
                else if (cuadrilatero.Borde == Borde.Rayas)
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
        private void CargarDatosRelleno()
        {
            var listaRelleno = Enum.GetValues(typeof(Entidades.Color)).Cast<Entidades.Color>().ToList();
            cboRelleno.DataSource = listaRelleno;
            cboRelleno.SelectedIndex = 0;
        }

        public Cuadrilatero GetCuadrilatero()
        {
            return cuadrilatero;
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
                cuadrilatero.SetLadoB(int.Parse(txtLadoB.Text));
                cuadrilatero.Relleno = (Color)cboRelleno.SelectedItem;
                if (rbtRayas.Checked)
                {
                    cuadrilatero.Borde = Borde.Rayas;

                }
                else if (rbtPuntos.Checked)
                {
                    cuadrilatero.Borde = Borde.Puntos;
                }
                else
                {
                    cuadrilatero.Borde = Borde.Lineal;
                }


                DialogResult = DialogResult.OK;
            }
        }

        public void SetCuadrilatero(Cuadrilatero? cuadrilatero)
        {
            this.cuadrilatero = cuadrilatero;
        }
        

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            errorProvider2.Clear();
            if (!int.TryParse(txtLadoA.Text, out int lado))
            {
                valido = false;
                errorProvider1.SetError(txtLadoA, "Numero mal ingresado");
            }
            else if (lado <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtLadoA, "Numero no valido");
            }
            else if (!int.TryParse(txtLadoB.Text, out int ladob))
            {
                valido = false;
                errorProvider2.SetError(txtLadoB, "Numero mal ingresado");
            }
            else if (ladob <= 0)
            {
                valido = false;
                errorProvider2.SetError(txtLadoB, "Numero no valido");
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
