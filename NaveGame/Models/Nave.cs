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
        public List<Bala> Balas { get; set; }
        public float SobreCarga { get; set; }
        public bool SobreCargaCondicion { get; set; }
        public float BalaEspecial { get; set; }
        public List<Enemigo> enemigos { get; set; }
        public ConsoleColor ColorAux { get; set; }
        public DateTime TiempoColision { get; set; }

        public Nave( Point posicion, ConsoleColor color, Ventana ventana )
        {
            Vida = 100;
            Posicion = posicion;
            Color = color;   
            VentanaC = ventana;
            PosicionesNave = new List<Point>();
            Balas = new List<Bala>();
            enemigos = new List<Enemigo>();
            ColorAux = color;
            TiempoColision = DateTime.Now;
        }

        // Dibuja la nave en la consola
        public void Dibujar()
        {
            if (DateTime.Now > TiempoColision.AddMilliseconds(1000))
            {
                Console.ForegroundColor = Color;
            }
            else
            {
                Console.ForegroundColor = ColorAux;
            }

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

            TecladoBala(tecla);

        }

        public void TecladoBala(ConsoleKeyInfo tecla)
        {
            if (tecla.Key == ConsoleKey.RightArrow)
            {
                if (!SobreCargaCondicion)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 6, Posicion.Y + 2),
                    ConsoleColor.Yellow, TipoBala.Normal);

                    Balas.Add(bala);
                    SobreCarga += 0.8f;
                    if (SobreCarga >= 100)
                    {
                        SobreCargaCondicion = true;
                        SobreCarga = 100;
                    }
                }
                

            }
            if (tecla.Key == ConsoleKey.LeftArrow)
            {
                if (!SobreCargaCondicion)
                {
                    Bala bala = new Bala(new Point(Posicion.X, Posicion.Y + 2),
                    ConsoleColor.Yellow, TipoBala.Normal);

                    Balas.Add(bala);
                    SobreCarga += 1.2f;
                    if (SobreCarga >= 100)
                    {
                        SobreCargaCondicion = true;
                        SobreCarga = 100;
                    }
                }
                
            }
            if (tecla.Key == ConsoleKey.UpArrow)
            {
                if (BalaEspecial >= 100)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 2, Posicion.Y - 2),
                        ConsoleColor.Red, TipoBala.Especial);
                    Balas.Add(bala);
                    BalaEspecial = 0;
                }
            }
        }

        public void Disparar()
        {
            for (int i = 0; i < Balas.Count; i++)
            {
                if (Balas[i].Mover(1, VentanaC.LimiteSuperior.Y, enemigos))
                {
                    Balas.Remove(Balas[i]);
                }
            }
        }

        // para que la nave no se salga del marco de la consola
        public void Colisiones(Point distancia)
        {
           Point posicionAux = new Point(Posicion.X + distancia.X, Posicion.Y + distancia.Y);
            if (posicionAux.X <= VentanaC.LimiteSuperior.X)
            {
                posicionAux.X = VentanaC.LimiteSuperior.X + 1;
            }
            if (posicionAux.X + 6 >= VentanaC.LimiteInferior.X)
            {
                posicionAux.X = VentanaC.LimiteInferior.X - 7;
            }
            if (posicionAux.Y <= (VentanaC.LimiteSuperior.Y) + 15)
            {
                posicionAux.Y = (VentanaC.LimiteSuperior.Y + 1) +15;
            }
            if (posicionAux.Y + 2 >= VentanaC.LimiteInferior.Y)
            {
                posicionAux.Y = VentanaC.LimiteInferior.Y - 3;
            }

            Posicion = posicionAux;
        }

        public void Informacion()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"VIDA: {(int)Vida} %");

            if (SobreCarga <= 0)
            {
                SobreCarga = 0;
            }
            else
            {
                SobreCarga -= 0.009f;
            }

            if (SobreCarga <= 50)
            {
                SobreCargaCondicion = false;
            }

            if (SobreCargaCondicion)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + 13, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"SOBRECARGA: {(int)SobreCarga} %");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + 32, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"BALA ESPECIAL: {(int)BalaEspecial} %");
            if (BalaEspecial >= 100)
            {
                BalaEspecial = 100;
            }
            else
            {
                BalaEspecial += 0.009f;
            }

        }
         
        // Metodo Mover que llama a otros metodos para su ejecucion
        public void Mover(int velocidad)
        {
            if (Console.KeyAvailable)
            {
                Borrar(); // Elimina la posicion anterior y el dibujo de la nave
                Point distancia = new Point();
                Teclado(ref distancia, velocidad);
                Colisiones(distancia);
            }

            Dibujar(); // Cada ves que se mueva la nave se dibuja de nuevo con la posicion actual
            Informacion();
        }

        public void Muerte()
        {
            Console.ForegroundColor = Color;
            foreach (Point item in PosicionesNave)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write("X");
                Thread.Sleep(200);
            }
            foreach (Point item in PosicionesNave)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
                Thread.Sleep(200);
            }
        }

    }
}
