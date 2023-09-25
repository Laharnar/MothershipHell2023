using Combat.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOutputButton : MonoBehaviour
{
    public string data= "undefined";
    public string value;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        UIOutputs.instance.data[data] = value;
    }
}
