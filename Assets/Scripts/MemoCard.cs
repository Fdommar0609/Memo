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
  
    [SerializeField] private Image image;

    private Animator _animator;
  
    private void Awake()
    {
        image.sprite = imagenFrontal;
        Image imgTrasera = GetComponent<Image>();
        imgTrasera.sprite = imagenTrasera;
        
        _animator = GetComponent<Animator>();
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
            //image.gameObject.SetActive(true);
            _animator.SetTrigger("Voltear");
        }
        else
        {
            //image.sprite = imagenTrasera;
            //image.gameObject.SetActive(false);
            _animator.SetTrigger("Desvoltear");
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
        image.sprite = imagenFrontal;
    }
}