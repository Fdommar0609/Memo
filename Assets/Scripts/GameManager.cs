using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum Jugador
    {
        Empty,
        Player1,
        Player2
    }


    public static GameManager Instance { get; private set; }
    public Jugador[,] Tablero { get; private set; } = new  Jugador[3, 2];

    public  bool IsGameOver { get; private set; }
    public Jugador Winner { get; private set; } = Jugador.Empty;
    public Jugador CurrentTurn { get; private set; } = Jugador.Player1;
    public List<Jugador> listaDeCartas { get; private set; } = new List<Jugador>();
    private int Carta1;
    private int Carta2;



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

    public bool PlayMove(int id)
    {
        // Verificar si el juego ha terminado
        if (IsGameOver)
        {
            return false;
        }
        
        // Registro la jugada
        // Compruebo si la pareja es igual o no
        // Comprobar si todas las fichas estan levantadas
        // Cambiar el turno si no se han levantado las dos fichas iguales

        return true;
    }

    public bool CheckWin(int id)
    {
        
        return false;
    }

    public void StartNewGame()
    {
        Debug.Log("UiManager StartNewGame");
    }
}
