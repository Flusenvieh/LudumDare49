using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MedalDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private GameObject CorrectMedal;

    [SerializeField]
    private int NailIndex;

    private bool HoldCorrectMedal = false;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject Medal = MedalManager.GetCurrentMedal();
        MedalManager.SetCurrentMedal(null);

        if (Medal == null)
            return;

        Medal.transform.position = transform.position;
        Medal.GetComponent<Medal>().SetIsHooked(this.gameObject);

        if (Medal == CorrectMedal)
        {
            HoldCorrectMedal = true;
            MedalManager.AddCorrectNail(NailIndex);
        }
        else
        {
            HoldCorrectMedal = false;
        }
    }

    public void MedalUnhooked()
    {
        if (HoldCorrectMedal)
        {
            HoldCorrectMedal = false;
            MedalManager.RemoveCorrectNail(NailIndex);
        }
    }
}
