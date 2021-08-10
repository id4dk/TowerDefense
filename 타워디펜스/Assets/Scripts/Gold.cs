using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public Text goldText;

    void Update()
    {
        goldText.text = "Gold : " + PlayerState.Money.ToString() + "G"; 
    }

}
