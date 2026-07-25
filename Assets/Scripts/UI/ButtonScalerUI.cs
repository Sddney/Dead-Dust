using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScalerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scaleTo = 1.1f;
    [SerializeField] private float _defaultScale = 1f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = transform.localScale * _scaleTo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(_defaultScale, _defaultScale, _defaultScale);
    }
}
