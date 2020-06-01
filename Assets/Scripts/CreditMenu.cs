using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    public GameObject creditClose;
    public GameObject creditOpen;

    bool close = true;

    public void OnButtonCreditPressed()
    {
        close = !close;
        creditClose.SetActive(close);
        creditOpen.SetActive(!close);
    }
}
