using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI P01;
    [SerializeField] private TextMeshProUGUI P02;


    public void OnClickReset()
    {
        Reset();
    }

    private void Reset()
    {
        GameManager.Instance.StartNewGame();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (GameManager.Instance.IsGameOver)
        {
            // poner ganador o empate
            if (GameManager.Instance.Winner == GameManager.Jugador.Empty)
            {
                statusText.text = "¡ Empate !";
            }
            else
            {
                statusText.text = $"¡ Ganador: {GameManager.Instance.Winner} !";
            }
        }
        else
        {
            // informar del turno actual
            statusText.text = $"Turno actual: {GameManager.Instance.CurrentTurn}";
        }
    }
}
