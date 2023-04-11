// See https://aka.ms/new-console-template for more information

using NaveGame;
using System.Drawing;



Ventana ventana = new Ventana(150, 44, ConsoleColor.Black, new Point(5, 3), new Point(145, 40));
ventana.DibujarMarco();
Console.ReadKey();