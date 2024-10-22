using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class MultiPlayerUI : MonoBehaviour
{
    [SerializeField] Button hostBtn, joinBtn;
    [SerializeField] InputField nicknameField;
    [SerializeField] ChatManager chatManager;

    private void Awake()
    {
        AssignInputs();
    }

    void AssignInputs()
    {
        hostBtn.onClick.AddListener(delegate { SetNickName(nicknameField.text); NetworkManager.Singleton.StartHost();  });
        joinBtn.onClick.AddListener(delegate { SetNickName(nicknameField.text); NetworkManager.Singleton.StartClient(); });
    }

    void SetNickName(string name)
    {
        chatManager.playerName = name;
    }
}
