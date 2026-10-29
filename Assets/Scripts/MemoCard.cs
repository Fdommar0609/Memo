using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MemoCard : MonoBehaviour
{
    public int id;
    private bool resuelta = false;
    private bool flipped = false;
    
    [SerializeField] private Sprite imagenFrontal;
    [SerializeField] private Sprite imagenTrasera;
  
    private Image image;
  
    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = imagenTrasera;
    }
  
    public void OnClick()
    {
        if (GameManager.Instance.IsGameOver || resuelta || flipped)
        {
            return;
        }
      
        GameManager.Instance.RealizarJugada(this);
    }


    public void GirarCarta(bool mostrarFrontal)
    {
        flipped = mostrarFrontal;
        if (mostrarFrontal)
        {
            image.sprite = imagenFrontal;
        }
        else
        {
            image.sprite = imagenTrasera;
        }
    }
    
    
    public void MarcarResuelta()
    {
        resuelta = true;
    }


    public void ResetearCarta()
    {
        resuelta = false;
        flipped = false;
        image.sprite = imagenTrasera;
    }
    
    public void AsignarImagenes(int nuevoId, Sprite nuevaImagenFrontal)
    {
        id = nuevoId;
        imagenFrontal = nuevaImagenFrontal;
    }
}