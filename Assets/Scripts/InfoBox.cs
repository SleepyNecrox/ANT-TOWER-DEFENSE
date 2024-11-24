using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    public GameObject infoBox;
    public void Close()
    {
        infoBox.SetActive(false);
    }
}
