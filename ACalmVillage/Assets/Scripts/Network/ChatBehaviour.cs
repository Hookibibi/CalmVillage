using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;
    private String pseudo;

    private static event Action<string> OnMessage;

    public override void OnStartAuthority()
    {
        pseudo = PlayerPrefs.GetString("Pseudo", "Anonyme");
        OnMessage += HandleNewMessage;
        CmdSendMessage(pseudo + " a rejoint la partie");
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }
        OnMessage -= HandleNewMessage;
        CmdSendMessage(pseudo + " a quitté la partie");
    }
    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send(string message)
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }

        if (string.IsNullOrWhiteSpace(message)) { return; }

        CmdSendMessage(pseudo + ": " + message);
        inputField.text = string.Empty;
    }

    public void SendServer()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        CmdSendMessage("Starting in 5");
        yield return new WaitForSeconds(1);
        CmdSendMessage("Starting in 4");
        yield return new WaitForSeconds(1);
        CmdSendMessage("Starting in 3");
        yield return new WaitForSeconds(1);
        CmdSendMessage("Starting in 2");
        yield return new WaitForSeconds(1);
        CmdSendMessage("Starting in 1");
        yield return new WaitForSeconds(1);
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        RpcHandleMessage(message);
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
