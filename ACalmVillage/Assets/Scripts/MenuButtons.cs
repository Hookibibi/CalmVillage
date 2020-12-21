using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject baseMenu;
    public GameObject optionsMenu;
    public GameObject playMenu;
    public Toggle fullscreen;
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        if (Screen.fullScreen)
            fullscreen.isOn = true;
        else
            fullscreen.isOn = false;
        int currentResolutionIndex = 0;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate.ToString() + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void OptionsButton()
    {
        baseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BaseMenu()
    {
        if (optionsMenu.activeSelf)
            optionsMenu.SetActive(false);
        if (playMenu.activeSelf)
            playMenu.SetActive(false);
        baseMenu.SetActive(true);
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
        // Application.Quit() ne marche pas dans l'éditeur, il faut donc utiliser cette variable pour quitter le jeu dans l'éditeur
        UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }

    public void FullscreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetResolution(int resoIndex)
    {
        Resolution resolution = resolutions[resoIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
