using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Singleton;

    [SerializeField] Message messagePrefab;
    [SerializeField] CanvasGroup chatContent;
    [SerializeField] InputField chatInputField;

    public string playerName;

    private void Awake()
    {
        ChatManager.Singleton = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(chatInputField.text, playerName);
            chatInputField.text = "";
        }
    }

    public void SendChatMessage(string text, string playerName)
    {
        if (string.IsNullOrWhiteSpace(text)) { return; }

        string S = playerName + " > " + text;
        SendChatMessageServerRpc(S);
    }

    void AddMessage(string text)
    {
        Message message = Instantiate(messagePrefab, chatContent.transform);
        message.SetText(text);
    }

    [ServerRpc(RequireOwnership = false)]
    void SendChatMessageServerRpc(string msg)
    {
        ReceiveChatMessageClientRpc(msg);
    }

    [ClientRpc]
    void ReceiveChatMessageClientRpc(string msg)
    {
        ChatManager.Singleton.AddMessage(msg);
    }
}
