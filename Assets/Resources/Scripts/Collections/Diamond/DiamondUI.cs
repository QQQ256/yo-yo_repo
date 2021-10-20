using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondUI : MonoBehaviour
{
    private int diamondNumber;
    public static DiamondUI instance;
    public Text diamondText;

    public int currentDiamondNumber;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        diamondNumber = 0;
    }

    void Start()
    {
        // currentDiamondNumber = diamondNumber;
    }

    // Update is called once per frame
    void Update()
    {
        // diamondText.text = currentDiamondNumber.ToString();
    }
}
