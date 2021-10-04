using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Medal : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private CanvasGroup canvasGroup;
    private bool onHook;
    private GameObject NailHookedOn = null;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!onHook)
        {
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1.0f;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onHook = false;

        if (NailHookedOn != null)
        {
            NailHookedOn.GetComponent<MedalDropHandler>().MedalUnhooked();
            NailHookedOn = null;
        }

        MedalManager.SetCurrentMedal(this.transform.gameObject);
        transform.position = Input.mousePosition;
        canvasGroup.alpha = 0.6f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!onHook)
        {
            transform.localPosition = Vector3.zero;
            canvasGroup.alpha = 1.0f;
        }
    }

    public void SetIsHooked(GameObject NailHookedOn)
    {
        onHook = true;
        this.NailHookedOn = NailHookedOn;
        canvasGroup.blocksRaycasts = true;
    }
}
