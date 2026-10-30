using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
   public enum Jugador { Empty, Jugador1, Player2 }
   public static GameManager Instance { get; private set; }
   public bool IsGameOver { get; private set; }
   public Jugador Winner { get; private set; } = Jugador.Empty;
   public Jugador CurrentTurn { get; private set; } = Jugador.Jugador1;
   
   // Variables para generar el tablero
   [SerializeField] private Transform contenedorCartas;
   [SerializeField] private GameObject cartaPrefab;
   [SerializeField] private Sprite[] spritesFrontales;
   
   // Lista para borrar las cartas de la partida anterior
   private List<GameObject> cartasInstanciadas = new List<GameObject>();
   
   // Filas y Columnas que tendrá el tablero
   [SerializeField] private int filas;
   [SerializeField] private int columnas;

   // Puntuaciones
   public int PuntuacionP01 { get; private set; } = 0;
   public int PuntuacionP02 { get; private set; } = 0;


   // Variables para la jugada actual
   private MemoCard primeraCartaLevantada;
   private MemoCard segundaCartaLevantada;
   private bool SeEstaComprobandoPareja = false;


   // Total de parejas encontradas
   private int parejasEncontradas = 0;
   private int totalParejas;






   private void Awake()
   {
       // Verificar si la instancia es nula
       if (Instance == null)
       {
           Instance = this;
           DontDestroyOnLoad(gameObject);
       }
       else
       {
           // Y si ya existe una instancia previa, eliminar este objeto
           Destroy(gameObject);
       }
   }
   
   private void Start() 
   {
       StartNewGame();
   }


   public bool RealizarJugada(MemoCard cartaSeleccionada)
   {
       // Verificar si el juego ha terminado
       if (IsGameOver || SeEstaComprobandoPareja)
       {
           return false;
       }


       if(primeraCartaLevantada == null)
       {
           primeraCartaLevantada = cartaSeleccionada;
           primeraCartaLevantada.GirarCarta(true);
       }else if (segundaCartaLevantada == null && cartaSeleccionada != primeraCartaLevantada)
       {
           segundaCartaLevantada = cartaSeleccionada;
           segundaCartaLevantada.GirarCarta(true);


           ComprobarPareja();
       }
       return true;
   }


   private void ComprobarPareja()
   {
       SeEstaComprobandoPareja = true;


       if (primeraCartaLevantada.id == segundaCartaLevantada.id)
       {
           primeraCartaLevantada.MarcarResuelta();
           segundaCartaLevantada.MarcarResuelta();
           parejasEncontradas++;


           if (CurrentTurn == Jugador.Jugador1)
           {
               PuntuacionP01++;
           }
           else
           {
               PuntuacionP02++;
           }


           CheckWin();


           primeraCartaLevantada = null;
           segundaCartaLevantada = null;
           SeEstaComprobandoPareja = false;
       } else
       {
           Invoke("OcultarCartasYCambiarTurno", 2);
       }
   }


   private void OcultarCartasYCambiarTurno()
   {
       primeraCartaLevantada.GirarCarta(false);
       segundaCartaLevantada.GirarCarta(false);


       CurrentTurn = (CurrentTurn == Jugador.Jugador1) ? Jugador.Player2 : Jugador.Jugador1;
      
       primeraCartaLevantada = null;
       segundaCartaLevantada = null;
       SeEstaComprobandoPareja = false;
   }


   public void CheckWin()
   {
       if (parejasEncontradas >= totalParejas)
       {
           IsGameOver = true;
           if (PuntuacionP01 > PuntuacionP02)
           {
               Winner = Jugador.Jugador1;
           }
           else if(PuntuacionP02 > PuntuacionP01)
           {
               Winner = Jugador.Player2;
           }
           else
           {
               Winner = Jugador.Empty;
           }
       }
   }


   public void StartNewGame()
   {
       IsGameOver = false;
       Winner = Jugador.Empty;
       CurrentTurn = Jugador.Jugador1;
       PuntuacionP01 = 0;
       PuntuacionP02 = 0;
       parejasEncontradas = 0;
       SeEstaComprobandoPareja = false;
      
       primeraCartaLevantada = null;
       segundaCartaLevantada = null;

       int cartasDeseadas = filas * columnas;
       GenerarTablero(cartasDeseadas);
   }

   private void GenerarTablero(int cartasTotales)
   {
       foreach (Transform hijo in contenedorCartas)
       {
           Destroy(hijo.gameObject);
       }
       cartasInstanciadas.Clear();
    
       GridLayoutGroup grid = contenedorCartas.GetComponent<GridLayoutGroup>();
       if (grid != null)
       {
           grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
           grid.constraintCount = columnas; 
       }
       
       if (cartasTotales % 2 != 0)
       {
           cartasTotales--;
           Debug.Log("No se puede generar un tablero impar, se ha adaptado para sea par.");
       }
       
       int limiteMaximoCartas = spritesFrontales.Length * 2;
       if (cartasTotales > limiteMaximoCartas)
       {
           cartasTotales = limiteMaximoCartas;
           Debug.Log("Se ha superado el máximo de cartas posibles para poner en el tablero./nSe ha puesto el tablero más grande posible.");
       }
       
       totalParejas = cartasTotales / 2;
    
       List<int> idsDeCartas = new List<int>();
       for(int i = 0; i < cartasTotales; i++)
       {
           idsDeCartas.Add(i / 2);
       }
    
       for(int i = 0; i < idsDeCartas.Count; i++)
       {
           int temp = idsDeCartas[i];
           int indiceAlAzar = Random.Range(0, idsDeCartas.Count);
        
           idsDeCartas[i] = idsDeCartas[indiceAlAzar];
           idsDeCartas[indiceAlAzar] = temp;
       }
    
       for(int i = 0; i < cartasTotales; i++)
       {
           int idDeLaCarta = idsDeCartas[i];
        
           GameObject nuevaCarta = Instantiate(cartaPrefab, contenedorCartas);
           cartasInstanciadas.Add(nuevaCarta);
        
           MemoCard carta = nuevaCarta.GetComponent<MemoCard>();
           carta.AsignarImagenes(idDeLaCarta, spritesFrontales[idDeLaCarta]);
       }
   }
}
