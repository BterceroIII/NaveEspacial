using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaveGame.Models
{
    internal class Nave
    {
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public ConsoleColor Color { get; set; }
        public Ventana VentanaC { get; set; }
        public List<Point> PosicionesNave { get; set; }

        public Nave( Point posicion, ConsoleColor color, Ventana ventana )
        {
            Vida = 100;
            Posicion = posicion;
            Color = color;   
            VentanaC = ventana;
            PosicionesNave = new List<Point>();
        }

        // Dibuja la nave en la consola
        public void Dibujar()
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            // Ubica las posiciones de los caracteres 
            Console.SetCursorPosition(x + 3, y);
            Console.Write("A");
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("<{x}>");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("± W W ±");

            PosicionesNave.Clear();
            GuardarPosiciones(x, y);
        }

        //Guarda las posiciones de la nave
        public void GuardarPosiciones(int x, int y)
        {
            PosicionesNave.Add(new Point(x + 3, y));

            PosicionesNave.Add(new Point(x + 1, y + 1));
            PosicionesNave.Add(new Point(x + 2, y + 1));
            PosicionesNave.Add(new Point(x + 3, y + 1));
            PosicionesNave.Add(new Point(x + 4, y + 1));
            PosicionesNave.Add(new Point(x + 5, y + 1));

            PosicionesNave.Add(new Point(x, y + 2));
            PosicionesNave.Add(new Point(x + 2, y + 2));
            PosicionesNave.Add(new Point(x + 4, y + 2));
            PosicionesNave.Add(new Point(x + 6, y + 2));
        }

        // borra las poisiciones anteriores de la nave
        public void Borrar()
        {
            foreach (var item in PosicionesNave)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }

        // decteta si las teclas W S A D son para mover las posiciones
        public void Teclado(ref Point distancia, int velocidad)
        {
           ConsoleKeyInfo tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.W)
            {
                distancia = new Point(0, -1);
            }
            if (tecla.Key == ConsoleKey.S)
            {
                distancia = new Point(0, 1);
            }
            if (tecla.Key == ConsoleKey.D)
            {
                distancia = new Point(1, 0);
            }
            if (tecla.Key == ConsoleKey.A)
            {
                distancia = new Point(-1, 0);
            }

            distancia.X *= velocidad;
            distancia.Y *= velocidad;
            Posicion = new Point(Posicion.X + distancia.X, Posicion.Y + distancia.Y);
        }
         
        // Metodo Mover que llama a otros metodos para su ejecucion
        public void Mover(int velocidad)
        {
            if (Console.KeyAvailable)
            {
                Borrar(); // Elimina la posicion anterior y el dibujo de la nave
                Point distancia = new Point();
                Teclado(ref distancia, velocidad);

                Dibujar(); // Cada ves que se mueva la nave se dibuja de nuevo con la posicion actual
            }
        }



    }
}
