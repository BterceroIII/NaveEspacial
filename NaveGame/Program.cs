// See https://aka.ms/new-console-template for more information

using NaveGame.Models;
using System.Drawing;



Ventana ventana;
Nave nave;
Enemigo enemigo1, enemigo2, boss;
bool jugar = false;
bool bossFinal = false;
bool ejecucion = true;


// Iniciamos por un metodo todos los componentes
void Iniciar()
{
    ventana = new Ventana(150, 44, ConsoleColor.Black, new Point(5, 3), new Point(145, 40));
    ventana.DibujarMarco();
    nave = new Nave(new Point(72,30), ConsoleColor.White,ventana);
    enemigo1 = new Enemigo(new Point(50, 10), ventana, ConsoleColor.Cyan, TipoEnemigo.Normal, nave); 
    enemigo2 = new Enemigo(new Point(70, 10), ventana, ConsoleColor.Green, TipoEnemigo.Normal, nave); 
    boss = new Enemigo(new Point(80, 10), ventana, ConsoleColor.Magenta, TipoEnemigo.Boss, nave); 
    nave.enemigos.Add(enemigo1);
    nave.enemigos.Add(enemigo2);
    nave.enemigos.Add(boss);
    

}

void Reiniciar()
{
    Console.Clear();
    ventana.DibujarMarco();

    nave.Vida = 100;
    nave.SobreCarga = 0;
    nave.BalaEspecial = 0;
    nave.Balas.Clear();

    enemigo1.Vida = 100;
    enemigo1.Vivo = true;
    enemigo2.Vida = 100;
    boss.Vivo = true;
    boss.Vivo = true;
    boss.PosicionesEnemigo.Clear();

    bossFinal = false;
}

void Game()
{
    while (ejecucion)
    {
        ventana.VentanaMenu();
        ventana.Teclado(ref ejecucion, ref jugar);
        while (jugar)
        {
            

            if (!enemigo1.Vivo && !enemigo2.Vivo && !bossFinal)
            {
                bossFinal = true;
                ventana.VentanaPeligro();
            }
            if (bossFinal)
            {
                boss.Mover();
                boss.Informacion(70);
            }
            else
            {
                enemigo1.Mover();
                enemigo1.Informacion(100);
                enemigo2.Mover();
                enemigo2.Informacion(120);
            }

            nave.Mover(2);
            nave.Disparar();
            if (nave.Vida <= 0)
            {
                jugar = false;
                nave.Muerte();
                Reiniciar();
            }
            if (!boss.Vivo)
            {
                jugar = false;
                Reiniciar();
            }

        }
    }
    
}



Iniciar();
Game();
