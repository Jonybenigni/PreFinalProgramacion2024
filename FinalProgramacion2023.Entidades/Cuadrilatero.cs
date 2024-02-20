using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProgramacion2023.Entidades
{

    public class Cuadrilatero : ICloneable
    {
        //Atributos de la clase
        private const int _CantidadLados = 2;
        private double _ladoA;
        private double _ladoB;
        private Borde tipoDeBorde;

        public Borde TipoDeBorde
        {
            get { return tipoDeBorde; }
            set { tipoDeBorde = value; }
        }

        private Color colorRelleno;

        public Color ColorRelleno
        {
            get { return colorRelleno; }
            set { colorRelleno = value; }
        }

        //Constructores
        public Cuadrilatero()
        {

        }
        public Cuadrilatero(double ladoA, double ladoB, Borde borde, Color color)
        {
            _ladoA = ladoA;
            _ladoB = ladoB;
            TipoDeBorde = borde;
            ColorRelleno = color;
            //if (MedidaLado>0)
            //{

            //}
            //else
            //{
            //    throw new ArgumentException("Medida del lado no válida");
            //}
        }
        //Métodos acceden a atributos
        public bool Validar()
        {
            return _ladoA > 0 && _ladoB > 0;
        }
        public double GetLadoA() => _ladoA;
        public double GetLadoB() => _ladoB;
        public void SetLadoA(double ladoA)
        {
            if (ladoA > 0)
            {
                _ladoA = ladoA;
            }
        }
        public void SetLadoB(double ladoB)
        {
            if (ladoB > 0)
            {
                _ladoB = ladoB;
            }
        }

        
        public double GetPerimetro() => 2 * (_ladoA + _ladoB);
        public double GetSuperficie() => _ladoA * _ladoB;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
