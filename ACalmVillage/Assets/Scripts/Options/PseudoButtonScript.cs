using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PseudoButtonScript : MonoBehaviour
{
    public InputField inputPseudo;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Pseudo"))
            inputPseudo.text = PlayerPrefs.GetString("Pseudo");
    }

    public void savePseudo()
    {
        PlayerPrefs.SetString("Pseudo", inputPseudo.text);
    }
}
