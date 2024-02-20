using FinalProgramacion2023.Entidades;

namespace FinalProgramacion2023.Datos
{
    public class RepositorioDeCuadrilateros
    {
        private readonly string _archivo = Environment.CurrentDirectory + "\\Cuadrilateros.txt";
        private readonly string _archivoCopia = Environment.CurrentDirectory + "\\Cuadrilateros.bak";

        private List<Cuadrilatero> listaCuadrilateros;

        public RepositorioDeCuadrilateros()
        {
            listaCuadrilateros = new List<Cuadrilatero>();
            LeerDatos();
        }
        public List<Cuadrilatero> GetLista()
        {
            return listaCuadrilateros;
        }
        private void LeerDatos()
        {
            if (File.Exists(_archivo))
            {
                var lector = new StreamReader(_archivo);
                while (!lector.EndOfStream)
                {
                    string lineaLeida = lector.ReadLine();
                    Cuadrilatero cuadrilatero = ConstuirCuadrilatero(lineaLeida);
                    listaCuadrilateros.Add(cuadrilatero);
                }
                lector.Close();

            }
        }
        public void Editar(Cuadrilatero cuadrilateroEnArchivo, Cuadrilatero cuadradoEditar)
        {
            using (var lector = new StreamReader(_archivo))
            {
                using (var escritor = new StreamWriter(_archivoCopia))
                {
                    while (!lector.EndOfStream)
                    {
                        string lineaLeida = lector.ReadLine();
                        Cuadrilatero cuadrilatero = ConstuirCuadrilatero(lineaLeida);
                        if (cuadrilateroEnArchivo != cuadrilatero)
                        {
                            escritor.WriteLine(lineaLeida);
                        }
                        else
                        {
                            lineaLeida = ConstruirLinea(cuadradoEditar);
                            escritor.WriteLine(lineaLeida);
                        }
                    }
                }
            }
            File.Delete(_archivo);
            File.Move(_archivoCopia, _archivo);
        }
        private Cuadrilatero ConstruirCuadrilatero(string? lineaLeida)
        {
            var campos = lineaLeida.Split('|');
            double ladoA = double.Parse(campos[0]);
            double ladoB = double.Parse(campos[1]);
            Color color = (Color)Enum.Parse(typeof(Color), campos[2]);
            Borde borde = (Borde)Enum.Parse(typeof(Borde), campos[3]);
            Cuadrilatero c = new Cuadrilatero(ladoA, ladoB, color, borde);
            return c;
        }

        public void Agregar(Cuadrilatero cuadrilatero)
        {
            var escritor = new StreamWriter(_archivo, true);

            string lineaEscribir = ConstruirLinea(cuadrilatero);
            escritor.WriteLine(lineaEscribir);
            escritor.Close();

            listaCuadrilateros.Add(cuadrilatero);

        }

        private string ConstruirLinea(Cuadrilatero cuadrilatero)
        {
            return $"{cuadrilatero.GetLadoA}|{cuadrilatero.GetLadoB}|{cuadrilatero.ColorRelleno.GetHashCode()}|{cuadrilatero.TipoDeBorde.GetHashCode()}";
        }

        public bool Existe(Cuadrilatero cuadrilatero)
        {
            listaCuadrilateros.Clear();
            LeerDatos();
            foreach (var itemCuadrilatero in listaCuadrilateros)
            {
                if (itemCuadrilatero.GetLadoA() == cuadrilatero.GetLadoA() &&
                    itemCuadrilatero.GetLadoB() == cuadrilatero.GetLadoB() &&
                    itemCuadrilatero.ColorRelleno == cuadrilatero.ColorRelleno &&
                    itemCuadrilatero.TipoDeBorde == cuadrilatero.TipoDeBorde)
                {
                    return true;
                }
            }
            return false;
        }
        public int GetCantidad(int valorFiltro = 0)
        {
            if (valorFiltro > 0)
            {
                return listaCuadrilateros
                    .Count(c => c.GetLadoA() >= valorFiltro);
            }
            return listaCuadrilateros.Count;
        }

    }
}