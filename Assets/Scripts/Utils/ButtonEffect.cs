using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image buttonImage;
    private Color originalColor;
    public Color clickColor = new Color(1, 1, 1, 0.5f);

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.color = originalColor;
    }
}