using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI P01;
    [SerializeField] private TextMeshProUGUI P02;
    
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI textoGanadorPanel;


    private void Start()
    {
        UpdateUI();
    }


    public void OnClickReset()
    {
        GameManager.Instance.StartNewGame();
        UpdateUI();
    }


    public void UpdateUI()
    {
        // Actualizar textos de puntuación
        P01.text = $"Jugador 1: {GameManager.Instance.PuntuacionP01}";
        P02.text = $"Jugador 2: {GameManager.Instance.PuntuacionP02}";


        if (GameManager.Instance.IsGameOver)
        {
            panelGameOver.SetActive(true);
            
            if (GameManager.Instance.Winner == GameManager.Jugador.Empty)
            {
                textoGanadorPanel.text = "¡ Empate !";
            }
            else
            {
                textoGanadorPanel.text = $"¡ Ganador: {GameManager.Instance.Winner} !";
                statusText.text = "Partida terminada";
            }
        }
        else
        {
            panelGameOver.SetActive(false);
            statusText.text = $"Turno actual: {GameManager.Instance.CurrentTurn}";
        }
    }


    private void Update()
    {
        UpdateUI();
    }
}