using UnityEngine;
using UnityEngine.UI;

public class MemoCard : MonoBehaviour
{
    private int id;
    private bool flipped;
    private bool resuelta;
    
    [SerializeField] private Sprite imagenFrontal;
    [SerializeField] private Sprite imagenBack;
    
    private Image image;
    private Sprite defaultSprite;
    
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void OnClick()
    {
        if (GameManager.Instance.IsGameOver || resuelta || flipped)
        {
            return;
        }

        // Intentar hacer la jugada
        GameManager.Instance.PlayMove(id);
    }


    public void Refresh()
    {
        if (!resuelta)
        {
            image.sprite = imagenBack;
        }
        else if(resuelta)
        {
            image.sprite = imagenFrontal;
        }
    }


    private void Update()
    {
        Refresh();
    }
}
