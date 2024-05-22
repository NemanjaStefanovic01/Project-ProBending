using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [Header("Buttons(Host, Join, Server)")]
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    private void Awake()
    {
        //Add events for the buttons
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            DisableButtons();
        });
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            DisableButtons();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            DisableButtons();
        });
    }

    void DisableButtons()
    {
        serverBtn.enabled = false;
        hostBtn.enabled = false;
        clientBtn.enabled = false;
    }
}
