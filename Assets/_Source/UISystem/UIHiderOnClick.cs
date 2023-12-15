using UnityEngine;
using UnityEngine.EventSystems;

public class UIHiderOnClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _hideObject;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _hideObject.SetActive(false);
    }
}
