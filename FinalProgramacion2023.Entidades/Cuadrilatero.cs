using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProgramacion2023.Entidades
{
    public class Cuadrilatero
    {
        private int LadoA;
        private int LadoB;


        private Borde borde;


        private Color relleno;



        public Borde Borde { get { return borde; } set { borde = value; } }
        public Color Relleno { get { return relleno; } set { relleno = value; } }



        public Cuadrilatero(int _medidaLadoA, int _medidaLadoB, Color relleno, Borde borde)
        {
            LadoA = _medidaLadoA;
            LadoB = _medidaLadoB;
            Relleno = relleno;
            Borde = borde;
        }

        public Cuadrilatero()
        {
        }

        public object NoesCuadrilatero()
        {
            if (LadoA > 0 && LadoB > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public TipodeCuadrilateros TipoCuadrilatero()
        {

            if (LadoA == LadoB)
            {
                return TipodeCuadrilateros.Cuadrado;
            }
            else
            {
                return TipodeCuadrilateros.Rectangulo;
            }




        }

        public enum TipodeCuadrilateros
        {
            Cuadrado,
            Rectangulo

        }



        public double GetPerimetro() => 2 * LadoA + 2 * LadoB;
        public double GetArea() => LadoA * LadoB;




        public int GetLadoA() => LadoA;
        public int GetLadoB() => LadoB;

        public void SetLadoA(int medidaA)
        {
            if (medidaA > 0)
            {
                LadoA = medidaA;
            }
        }
        public void SetLadoB(int medidaB)
        {
            if (medidaB > 0)
            {
                LadoB = medidaB;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
