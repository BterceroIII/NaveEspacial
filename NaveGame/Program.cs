﻿// See https://aka.ms/new-console-template for more information

using NaveGame.Models;
using System.Drawing;



Ventana ventana;
Nave nave;
Enemigo enemigo1, enemigo2, boss;
bool jugar = true;


// Iniciamos por un metodo todos los componentes
void Iniciar()
{
    Ventana ventana = new Ventana(150, 44, ConsoleColor.Black, new Point(5, 3), new Point(145, 40));
    ventana.DibujarMarco();
    nave = new Nave(new Point(72,30), ConsoleColor.White,ventana);
    enemigo1 = new Enemigo(new Point(50, 10), ventana, ConsoleColor.Cyan, TipoEnemigo.Normal, nave); 
    enemigo2 = new Enemigo(new Point(70, 10), ventana, ConsoleColor.Green, TipoEnemigo.Normal, nave); 
    boss = new Enemigo(new Point(80, 10), ventana, ConsoleColor.Magenta, TipoEnemigo.Boss, nave); 
    nave.Dibujar();
    enemigo1.Dibujar();
    enemigo2.Dibujar();
    nave.enemigos.Add(enemigo1);
    nave.enemigos.Add(enemigo2);
    nave.enemigos.Add(boss);

}

void Game()
{
    while (jugar)
    {
        boss.Mover();
        boss.Informacion(140);

        enemigo1.Mover();
        enemigo1.Informacion(100);
        enemigo2.Mover();
        enemigo2.Informacion(120);

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
