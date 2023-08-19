using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private string itemName;
    [TextArea][SerializeField] private string itemExtraInfo;
    [SerializeField] private InspectController inspectController;
    public void ShowObjectName()
    {
        inspectController.ShowName(itemName);
    }

    public void HideObjectName()
    {
        inspectController.HideName();
    }

    public void ShowExtraInfo()
    {
        inspectController.ShowAdditionalInfo(itemExtraInfo);
    }
}
