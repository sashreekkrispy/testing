using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectController : MonoBehaviour
{
    [SerializeField] private GameObject objectNameBG;
    [SerializeField] private Text objectNameUI;

    [SerializeField] private float onScreenTimer;
    [SerializeField] private Text extraInfoUI;
    [SerializeField] private GameObject extraInfoBG;
    [HideInInspector] public bool startTimer;
    private float Timer;

    private void Start()
    {
        objectNameBG.SetActive(false);
        extraInfoBG.SetActive(false);
    }

    private void Update()
    {
        if(startTimer)
        {
            Timer -= Time.deltaTime;

            if(Timer <= 0 )
            {
                Timer = 0;
                ClearAdditionalInfo();
                startTimer = false;
            }
        }
    }

    public void ShowName(string objectName)
    {
        objectNameBG.SetActive(true);
        objectNameUI.text = objectName;
    }

    public void HideName()
    {
        objectNameBG.SetActive(false);
        objectNameUI.text = "";
    }

    public void ShowAdditionalInfo(string newInfo)
    {
        Timer = onScreenTimer;
        startTimer= true;  
        extraInfoBG.SetActive(true);
        extraInfoUI.text = newInfo;
    }

    void ClearAdditionalInfo()
    {
        extraInfoBG.SetActive(false);
        extraInfoUI.text = "";
    }
}
