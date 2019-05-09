using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAxisUIManager : MonoBehaviour
{
    public InputAxisUIBar HorizontalP1;
    public InputAxisUIBar VerticalP1;
    public InputAxisUIBar AimHorizontalP1;
    public InputAxisUIBar AimVerticalP1;
    public InputAxisUIBar Fire1P1;
    public InputAxisUIBar Fire2P1;
    public InputButtonUI JumpP1;

    public InputAxisUIBar HorizontalP2;
    public InputAxisUIBar VerticalP2;
    public InputAxisUIBar AimHorizontalP2;
    public InputAxisUIBar AimVerticalP2;
    public InputAxisUIBar Fire1P2;
    public InputAxisUIBar Fire2P2;
    public InputButtonUI JumpP2;

    public InputAxisUIBar HorizontalP3;
    public InputAxisUIBar VerticalP3;
    public InputAxisUIBar AimHorizontalP3;
    public InputAxisUIBar AimVerticalP3;
    public InputAxisUIBar Fire1P3;
    public InputAxisUIBar Fire2P3;
    public InputButtonUI JumpP3;

    public InputAxisUIBar HorizontalP4;
    public InputAxisUIBar VerticalP4;
    public InputAxisUIBar AimHorizontalP4;
    public InputAxisUIBar AimVerticalP4;
    public InputAxisUIBar Fire1P4;
    public InputAxisUIBar Fire2P4;
    public InputButtonUI JumpP4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalP1.fillBarLevel = Input.GetAxis("HorizontalP1");
        VerticalP1.fillBarLevel = Input.GetAxis("VerticalP1");
        AimHorizontalP1.fillBarLevel = Input.GetAxis("AimHorizontalP1");
        AimVerticalP1.fillBarLevel = Input.GetAxis("AimVerticalP1");
        Fire1P1.fillBarLevel = Input.GetAxis("Fire1P1");
        Fire2P1.fillBarLevel = Input.GetAxis("Fire2P1");
        JumpP1.buttonOn = (Input.GetAxis("JumpP1") > 0);

        HorizontalP2.fillBarLevel = Input.GetAxis("HorizontalP2");
        VerticalP2.fillBarLevel = Input.GetAxis("VerticalP2");
        AimHorizontalP2.fillBarLevel = Input.GetAxis("AimHorizontalP2");
        AimVerticalP2.fillBarLevel = Input.GetAxis("AimVerticalP2");
        Fire1P2.fillBarLevel = Input.GetAxis("Fire1P2");
        Fire2P2.fillBarLevel = Input.GetAxis("Fire2P2");
        JumpP2.buttonOn = (Input.GetAxis("JumpP2") > 0);

        HorizontalP3.fillBarLevel = Input.GetAxis("HorizontalP3");
        VerticalP3.fillBarLevel = Input.GetAxis("VerticalP3");
        AimHorizontalP3.fillBarLevel = Input.GetAxis("AimHorizontalP3");
        AimVerticalP3.fillBarLevel = Input.GetAxis("AimVerticalP3");
        Fire1P3.fillBarLevel = Input.GetAxis("Fire1P3");
        Fire2P3.fillBarLevel = Input.GetAxis("Fire2P3");
        JumpP3.buttonOn = (Input.GetAxis("JumpP3") > 0);

        HorizontalP4.fillBarLevel = Input.GetAxis("HorizontalP4");
        VerticalP4.fillBarLevel = Input.GetAxis("VerticalP4");
        AimHorizontalP4.fillBarLevel = Input.GetAxis("AimHorizontalP4");
        AimVerticalP4.fillBarLevel = Input.GetAxis("AimVerticalP4");
        Fire1P4.fillBarLevel = Input.GetAxis("Fire1P4");
        Fire2P4.fillBarLevel = Input.GetAxis("Fire2P4");
        JumpP4.buttonOn = (Input.GetAxis("JumpP4") > 0);
    }
}
