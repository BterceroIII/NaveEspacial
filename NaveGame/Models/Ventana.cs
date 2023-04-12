using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaveGame.Models
{
    internal class Ventana
    {
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public ConsoleColor Color { get; set; }
        public Point LimiteSuperior { get; set; }
        public Point LimiteInferior { get; set; }

        public Ventana(int ancho, int altura, ConsoleColor color, Point limiteSuperior, Point liminteInferior)
        {
            Ancho = ancho;
            Altura = altura;
            Color = color;
            LimiteSuperior = limiteSuperior;
            LimiteInferior = liminteInferior;
            Init();

        }

        private void Init()
        {
            Console.SetWindowSize(Ancho, Altura);
            Console.Title = "NaveGame";
            Console.CursorVisible = false;
            Console.BackgroundColor = Color;
            Console.Clear();
        }

        public void DibujarMarco()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            //Este ciclo dibujara el ancho del marco
            for (int i = LimiteSuperior.X; i <= LimiteInferior.X; i++)
            {
                Console.SetCursorPosition(i, LimiteSuperior.Y);
                Console.Write("═");
                Console.SetCursorPosition(i, LimiteInferior.Y);
                Console.Write("═");
            }
            for (int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++)
            {
                Console.SetCursorPosition(LimiteSuperior.X, i);
                Console.Write("║");
                Console.SetCursorPosition(LimiteInferior.X, i);
                Console.Write("║");
            }

            Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
            Console.Write("╔");
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
            Console.Write("╚");
            Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
            Console.Write("╗");
            Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
            Console.Write("╝");

        }
    }
}
