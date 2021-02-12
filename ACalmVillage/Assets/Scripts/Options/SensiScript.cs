using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensiScript : MonoBehaviour
{
    public Slider sliderSensi;
    public Text textvalue;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Sensivity"))
            sliderSensi.value = 1.55f;
        else
            sliderSensi.value = PlayerPrefs.GetFloat("Sensivity");
        textvalue.text = sliderSensi.value.ToString("F2");
    }

    public void UpdateValue()
    {
        textvalue.text = sliderSensi.value.ToString("F2");
        //PlayerPrefs.SetFloat("Sensivity", sliderSensi.value);
    }
}
