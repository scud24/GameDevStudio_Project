using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject fillBar;
    public float fillBarLevel; //Ranges from -1 to 1
    public float currentValue;
    public float maxValue;
    public Text fillBarText;
    public float fillBarMaxWidth;
    public string barCaption;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fillBarLevel = currentValue / maxValue;
        fillBarLevel = Mathf.Clamp(fillBarLevel, 0, 1);
        fillBarText.text = barCaption +  (int)currentValue + "/" + maxValue;
        fillBar.transform.localScale = new Vector3(fillBarLevel, 1, 1);
    }
}
