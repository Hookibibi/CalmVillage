using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{
    public InputField pseudo;
    public InputField portHost;
    public InputField addressJoin;
    public InputField portJoin;
    public GameObject joinButton;

    public Canvas canvasMenu;
    public GameObject menuCanvas;

    [SerializeField] private NetworkManagerLobby networkManager = null;

    private Color32 errorColor = new Color32(222, 122, 122, 255);
    private Color32 whiteColor = new Color32(255, 255, 255, 255);
    private void OnEnable()
    {
        pseudo.text = PlayerPrefs.GetString("Pseudo", "Anonyme");
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
        NetworkManagerLobby.OnClientErrored += HandleClientError;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
        NetworkManagerLobby.OnClientErrored -= HandleClientError;
    }

    private bool CheckForErrors(InputField toTest)
    {
        if (toTest.text.Length == 0)
            return true;
        return false;
    }

    private void CheckForErrorsColor(InputField toTest)
    {
        if (toTest.text.Length == 0)
        {
            toTest.GetComponent<Image>().color = errorColor;
        }
    }

    public void HostGame()
    {
        if (!(CheckForErrors(pseudo) || CheckForErrors(portHost)))
        {
            PlayerPrefs.SetString("Pseudo", pseudo.text);
            networkManager.GetComponent<kcp2k.KcpTransport>().Port = Convert.ToUInt16(portHost.text);
            networkManager.StartHost();
            canvasMenu.enabled = false;
        }
        else
        {
            CheckForErrorsColor(pseudo);
            CheckForErrorsColor(portHost);
        }
    }

    public void JoinGame()
    {
        if (!(CheckForErrors(pseudo) || CheckForErrors(portJoin) || CheckForErrors(addressJoin)))
        {
            PlayerPrefs.SetString("Pseudo", pseudo.text);
            networkManager.networkAddress = addressJoin.text;
            networkManager.GetComponent<kcp2k.KcpTransport>().Port = Convert.ToUInt16(portJoin.text);
            networkManager.StartClient();
            joinButton.SetActive(false);
        }
        else
        {
            CheckForErrorsColor(pseudo);
            CheckForErrorsColor(addressJoin);
            CheckForErrorsColor(portJoin);
        }
    }

    private void HandleClientConnected()
    {
        joinButton.SetActive(true);
        Debug.Log("Client connected");
        canvasMenu.enabled = false;
    }

    private void HandleClientDisconnected()
    {
        joinButton.SetActive(true);
        addressJoin.GetComponent<Image>().color = errorColor;
        portJoin.GetComponent<Image>().color = errorColor;
    }

    private void HandleClientError()
    {
        joinButton.SetActive(true);
        Debug.Log("Error");
        addressJoin.GetComponent<Image>().color = errorColor;
        portJoin.GetComponent<Image>().color = errorColor;
    }


    private void Update()
    {
        pseudo.GetComponent<Image>().color = Color.Lerp(pseudo.GetComponent<Image>().color, whiteColor, Time.deltaTime / 1.2f);
        portHost.GetComponent<Image>().color = Color.Lerp(portHost.GetComponent<Image>().color, whiteColor, Time.deltaTime / 1.2f);
        addressJoin.GetComponent<Image>().color = Color.Lerp(addressJoin.GetComponent<Image>().color, whiteColor, Time.deltaTime / 1.2f);
        portJoin.GetComponent<Image>().color = Color.Lerp(portJoin.GetComponent<Image>().color, whiteColor, Time.deltaTime / 1.2f);
    }

    public void BackToMenu()
    {
        menuCanvas.SetActive(true);
        canvasMenu.enabled = false;
    }
}
