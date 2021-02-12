using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindingScript : MonoBehaviour
{
    private Dictionary<string, KeyCode> keybindings = new Dictionary<string, KeyCode>();

    public Text up;
    public Text down;
    public Text left;
    public Text right;
    public Text jump;
    public Text inventory;
    public Text interact;
    public Text micro;
    public Text crouch;

    private Color32 normal = new Color32(255, 255, 255, 255);
    private Color32 selected = new Color32(169, 169, 169, 255);

    public InputField sensiInput;

    private GameObject currentKey;

    public void ReloadBindings()
    {
        sensiInput.text = PlayerPrefs.GetFloat("Sensivity", 1.55f).ToString();

        keybindings.Clear();
        keybindings.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "Z")));
        keybindings.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keybindings.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "Q")));
        keybindings.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keybindings.Add("Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "E")));
        keybindings.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F")));
        keybindings.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keybindings.Add("Micro", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Micro", "V")));
        keybindings.Add("Crouch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl")));

        up.text = keybindings["Up"].ToString();
        down.text = keybindings["Down"].ToString();
        left.text = keybindings["Left"].ToString();
        right.text = keybindings["Right"].ToString();
        jump.text = keybindings["Jump"].ToString();
        inventory.text = keybindings["Inventory"].ToString();
        interact.text = keybindings["Interact"].ToString();
        micro.text = keybindings["Micro"].ToString();
        crouch.text = keybindings["Crouch"].ToString();
    }

    public void Start()
    {
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keybindings[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        clicked.GetComponent<Image>().color = selected;
    }

    public void SaveBindings()
    {
        if (sensiInput.text.Length != 0)
            PlayerPrefs.SetFloat("Sensivity", float.Parse(sensiInput.text));
        foreach (var key in keybindings)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
