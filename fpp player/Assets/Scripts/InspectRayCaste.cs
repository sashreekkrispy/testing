using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectRayCaste : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    private ObjectController rayCasteObj;

    [SerializeField] private Image crosshair;
    private bool isCrossHairActive;
    private bool doOnce;

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if(hit.collider.CompareTag("InteractObject"))
            {
                if(!doOnce)
                {
                    rayCasteObj = hit.collider.gameObject.GetComponent<ObjectController>();
                    rayCasteObj.ShowObjectName();
                    CrosshairChange(true);
                }
                isCrossHairActive= true;
                doOnce = true;
                if(Input.GetMouseButton(0))
                {
                    rayCasteObj.ShowExtraInfo();
                }
            }
        }
        else
        {
            if(isCrossHairActive)
            {
                rayCasteObj.HideObjectName();
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if(on && !doOnce)
        {
            crosshair.color = Color.white;
        }
        else
        {
            crosshair.color = Color.red;
            isCrossHairActive = false;
        }
    }
}
