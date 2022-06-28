using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public double timeTotal;
    private double timeCurrent;
    public Text textBox;

    public double percentComplete;

    // Start is called before the first frame update
    void Start()
    {
        percentComplete = 0;
        timeCurrent = timeTotal;
        textBox.text = timeCurrent.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        percentComplete = Math.Abs((timeCurrent - timeTotal) / timeTotal) * 100;
        timeCurrent -= Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(timeCurrent);
        textBox.text = t.ToString(@"mm\:ss");
    }
}
