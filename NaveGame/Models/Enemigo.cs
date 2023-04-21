﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaveGame.Models
{
    enum TipoEnemigo
    {
        Normal, Boss
    }
    internal class Enemigo
    {
        enum Direccion
        {
            Derecha, Izquierda, Arriba, Abajo
        }

        public bool Vivo { get; set; }
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public Ventana VentanaC { get; set; }
        public ConsoleColor Color { get; set; }
        public TipoEnemigo TipoEnemigoE { get; set; }
        public Nave NaveC { get; set; }
        public List<Point> PosicionesEnemigo { get; set; }
        public List<Bala> Balas { get; set; }

        private Direccion _direccion;
        private DateTime _tiempoDireccion;
        private float _tiempoDireccionAleatorio;
        private float _tiempoDisparoAleatorio;
        private DateTime _tiempoMovimiento;
        private DateTime _tiempoBala;

        public Enemigo(Point posicion, Ventana ventana, ConsoleColor color, TipoEnemigo tipoEnemigoE, Nave nave)
        {
            Vivo = true;
            Vida = 100;
            Posicion = posicion;
            VentanaC = ventana;
            Color = color;
            TipoEnemigoE = tipoEnemigoE;
            _direccion = Direccion.Derecha;
            _tiempoDireccion = DateTime.Now;
            _tiempoDireccionAleatorio = 1000;
            _tiempoMovimiento = DateTime.Now;
            PosicionesEnemigo = new List<Point>();
            Balas = new List<Bala>();
            _tiempoBala = DateTime.Now;
            _tiempoDisparoAleatorio = 200;
             NaveC = nave;
        }

        public void Dibujar()
        {
            switch (TipoEnemigoE)
            {
                case TipoEnemigo.Normal:
                    DibujoNormal();
                    break;
                case TipoEnemigo.Boss:
                    DibujoBoss();
                    break;
                
            }
        }

        public void DibujoNormal()
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x + 1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x, y + 1));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 2, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));
        }

        public void DibujoBoss()
        {
            Console.ForegroundColor = Color;
            int x = Posicion.X;
            int y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("█▄▄▄▄█");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("██ ██ ██");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("█▀▀▀▀▀▀█");

            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x + 1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x + 3, y));
            PosicionesEnemigo.Add(new Point(x + 4, y));
            PosicionesEnemigo.Add(new Point(x + 5, y));
            PosicionesEnemigo.Add(new Point(x + 6, y));

            PosicionesEnemigo.Add(new Point(x, y + 1));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x + 4, y + 1));
            PosicionesEnemigo.Add(new Point(x + 6, y + 1));
            PosicionesEnemigo.Add(new Point(x + 7, y + 1));

            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 2, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));
            PosicionesEnemigo.Add(new Point(x + 4, y + 2)); 
            PosicionesEnemigo.Add(new Point(x + 5, y + 2));
            PosicionesEnemigo.Add(new Point(x + 6, y + 2));
            PosicionesEnemigo.Add(new Point(x + 7, y + 2));
        }

        public void Borrar()
        {
            foreach (Point item in PosicionesEnemigo)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }

        public void Mover()
        {
            int tiempo = 30;
            if (TipoEnemigoE == TipoEnemigo.Boss)
            {
                tiempo = 20;
            }
            if (DateTime.Now > _tiempoMovimiento.AddMilliseconds(tiempo))
            {
                Borrar();
                DireccionAleatoria();
                Point posicionAux = Posicion;
                Movimiento(ref posicionAux);
                Colisiones(posicionAux);
                Dibujar();
                _tiempoMovimiento = DateTime.Now;
            }
            CrearBalas();
            Disparar();
        }

        public void Colisiones(Point posicionAux)
        {
            int ancho = 3;
            if (TipoEnemigoE == TipoEnemigo.Boss)
            {
                ancho = 7;
            }

            if (posicionAux.X <= VentanaC.LimiteSuperior.X)
            {
                _direccion = Direccion.Derecha;
                posicionAux.X = VentanaC.LimiteSuperior.X + 1;
            }
            if (posicionAux.X + ancho >= VentanaC.LimiteInferior.X)
            {
                _direccion = Direccion.Izquierda;
                posicionAux.X = VentanaC.LimiteInferior.X - 1 - ancho;
            }
            if (posicionAux.Y <= VentanaC.LimiteSuperior.Y)
            {
                _direccion = Direccion.Abajo;
                posicionAux.Y = VentanaC.LimiteSuperior.Y + 1;
            }
            if (posicionAux.Y + 2 >= VentanaC.LimiteSuperior.Y + 15)
            {
                _direccion = Direccion.Arriba;
                posicionAux.Y = VentanaC.LimiteSuperior.Y + 15 - 2;
            }

            Posicion = posicionAux;
        }

        public void Movimiento(ref Point posicionAux)
        {
            switch (_direccion)
            {
                case Direccion.Derecha:
                    posicionAux.X += 1;
                    break;
                case Direccion.Izquierda:
                    posicionAux.X -= 1;
                    break;
                case Direccion.Arriba:
                    posicionAux.Y -= 1;
                    break;
                case Direccion.Abajo:
                    posicionAux.Y += 1;
                    break;
            }  
        }

        public void DireccionAleatoria()
        {
            if (DateTime.Now > _tiempoDireccion.AddMilliseconds(_tiempoDireccionAleatorio) && (_direccion == Direccion.Derecha || _direccion == Direccion.Izquierda))
            {
                Random random = new Random();
                int numRandom = random.Next(1, 5); //es de 1 a 4 pero se le suma uno mas porque el ramdom no involucra el numero final

                switch (numRandom)
                {
                    case 1:
                        _direccion = Direccion.Derecha;
                        break;
                    case 2:
                        _direccion = Direccion.Izquierda;
                        break;
                    case 3:
                        _direccion = Direccion.Arriba;
                        break;
                    case 4:
                        _direccion = Direccion.Abajo;
                        break;
                }

                _tiempoDireccion = DateTime.Now;
                _tiempoDireccionAleatorio = random.Next(1000,2000);
            }

            if (DateTime.Now > _tiempoDireccion.AddMilliseconds(80) && (_direccion == Direccion.Arriba || _direccion == Direccion.Abajo))
            {
                Random random = new Random();
                int numRandom = random.Next(1, 3); //es de 1 a 4 pero se le suma uno mas porque el ramdom no involucra el numero final

                switch (numRandom)
                {
                    case 1:
                        _direccion = Direccion.Derecha;
                        break;
                    case 2:
                        _direccion = Direccion.Izquierda;
                        break;
                }

                _tiempoDireccion = DateTime.Now;
                _tiempoDireccionAleatorio = random.Next(1000, 2000);
            }

        }

        public void CrearBalas()
        {
            if (DateTime.Now > _tiempoBala.AddMilliseconds(_tiempoDisparoAleatorio))
            {
                Random random = new Random();

                if (TipoEnemigoE == TipoEnemigo.Normal)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 1, Posicion.Y + 2), Color, TipoBala.Enemigo);

                    Balas.Add(bala);
                    _tiempoDisparoAleatorio = random.Next(200, 500);
                }
                if (TipoEnemigoE == TipoEnemigo.Boss)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 4, Posicion.Y + 2), Color, TipoBala.Enemigo);

                    Balas.Add(bala);
                    _tiempoDisparoAleatorio = random.Next(100, 150);
                }
                _tiempoBala = DateTime.Now;
            }
           
        }

        public void Disparar()
        {
            for (int i = 0; i < Balas.Count; i++)
            {
                if (Balas[i].Mover(1, VentanaC.LimiteInferior.Y, NaveC))
                {
                    Balas.Remove(Balas[i]);
                }
            }
        }

        public void Informacion(int distanciaX)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + distanciaX, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"Enemigo: {(int)Vida} % ");
        }
    }
}