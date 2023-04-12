// See https://aka.ms/new-console-template for more information

using NaveGame.Models;
using System.Drawing;



Ventana ventana;
Nave nave;
bool jugar = true;


// Iniciamos por un metodo todos los componentes
void Iniciar()
{
    Ventana ventana = new Ventana(150, 44, ConsoleColor.Black, new Point(5, 3), new Point(145, 40));
    ventana.DibujarMarco();
    nave = new Nave(new Point(72,30), ConsoleColor.White,ventana);
    nave.Dibujar();
}

void Game()
{
    while (jugar)
    {
        nave.Mover(2);
        nave.Disparar();
        if (nave.Vida <= 0)
        {
            jugar = false;
            nave.Muerte();
        }
        
    }
}



Iniciar();
Game();
Console.ReadKey();