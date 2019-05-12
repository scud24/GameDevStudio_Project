using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputAxisUIBar : MonoBehaviour
{
    public GameObject fillBar;
    public float fillBarLevel; //Ranges from -1 to 1
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillBarLevel = Mathf.Clamp(fillBarLevel, -1, 1);
        fillBar.transform.localScale = new Vector3(0.5f + 0.5f * fillBarLevel, 1, 1);
    }
}
