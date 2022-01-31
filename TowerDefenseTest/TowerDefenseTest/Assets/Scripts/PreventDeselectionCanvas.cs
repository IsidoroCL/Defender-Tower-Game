using UnityEngine;
using UnityEngine.EventSystems;

public class PreventDeselectionCanvas : MonoBehaviour
{
    EventSystem evt;
    GameObject sel;
    private void Start()
    {
        evt = EventSystem.current;
    }

    private void Update()
    {
        if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != sel)
            sel = evt.currentSelectedGameObject;
        else if (sel != null && evt.currentSelectedGameObject == null)
            evt.SetSelectedGameObject(sel);
    }

}