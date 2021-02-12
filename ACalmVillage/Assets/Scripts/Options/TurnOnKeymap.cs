using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnKeymap : MonoBehaviour
{
    public GameObject options;
    public GameObject keymaps;
    public KeybindingScript keymapsManager;

    public void TurnOnKeymaps()
    {
        keymaps.SetActive(true);
        keymapsManager.ReloadBindings();
        options.SetActive(false);
    }

    public void TurnOffKeymaps()
    {
        options.SetActive(true);
        keymaps.SetActive(false);
    }
    public void SaveKeymaps()
    {
        keymapsManager.SaveBindings();
        TurnOffKeymaps();
    }
}
