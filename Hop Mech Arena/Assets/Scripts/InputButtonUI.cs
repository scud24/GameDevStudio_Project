using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonUI : MonoBehaviour
{
    public Image inputButtonFill;
    public bool buttonOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color tempColor = inputButtonFill.color;
        if (buttonOn)
        {
            tempColor.a = 255;
        }
        else
        {
            tempColor.a = 0;
        }
        inputButtonFill.color = tempColor;
    }
}
