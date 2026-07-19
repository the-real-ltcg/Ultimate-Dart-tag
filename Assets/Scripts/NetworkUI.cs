using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hostAddressText; // Drag your UI text element here
    [SerializeField] private Button exitButton;
//    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TMP_InputField hostAddressInput;
    [SerializeField] private Button connectToHostButton;
    

    private void Awake()
    {
        exitButton.onClick.AddListener(OnExitButtonClicked);
        // serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        hostButton.onClick.AddListener(OnHostButtonClicked); // () => NetworkManager.Singleton.StartHost());
        clientButton.onClick.AddListener(OnClientButtonClicked);
        connectToHostButton.onClick.AddListener(OnConnectToHostButtonClicked);
    }

    private void OnClientButtonClicked()
    {
        // Display the IP address input field and the Connect To Host button
        hostAddressInput.gameObject.SetActive(true);
        connectToHostButton.gameObject.SetActive(true);

        // Set the hostAddressText to the current IP address, minus the last number
        string localIP = GetLocalIPAddress();
        if (!string.IsNullOrEmpty(localIP))
        {
            string[] ipParts = localIP.Split('.');
            if (ipParts.Length == 4)
            {
                hostAddressInput.text = $"{ipParts[0]}.{ipParts[1]}.{ipParts[2]}.";
            }
        }
    }

    private void OnConnectToHostButtonClicked()
    {
        
         string ipAddress = hostAddressInput.text;
        
        if (string.IsNullOrEmpty(ipAddress))
        {
            Debug.LogWarning("Please enter a valid IP address.");
            return;
        }

        // Set the IP address for the client
        var unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        // Beware the magic number!
        unityTransport.SetConnectionData(ipAddress, port: 7777); // Ensure port matches the host's port

        // Start the client
        if (NetworkManager.Singleton.StartClient())
        {
            PopulateHostAddressText($"Connected to {ipAddress}");
            DisableMenu();
        }
        else
        {
            PopulateHostAddressText($"Unable to connect to {ipAddress}");
        }
    }

    private void OnHostButtonClicked()
    {
        NetworkManager.Singleton.StartHost();

        PopulateHostAddressText(FindLocalIpAddress());
        DisableMenu();
    }

    private void DisableMenu()
    {
        // serverButton.gameObject.SetActive(false);
        hostButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
        hostAddressInput.gameObject.SetActive(false);
        connectToHostButton.gameObject.SetActive(false);
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void PopulateHostAddressText(string textToShow)
    {
        // set the hostAddressText to visible
        hostAddressText.gameObject.SetActive(true);

        hostAddressText.text = textToShow;
    }

    private string FindLocalIpAddress()
    {
        string localIP = GetLocalIPAddress();
        if (!string.IsNullOrEmpty(localIP))
        {
            return $"Host IP: {localIP}";
        }
        else
        {
            return "Host IP: Not Found";
        }
    }

    private string GetLocalIPAddress()
    {
        try
        {
            foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // Only IPv4 addresses
                {
                    return ip.ToString();
                }
            }
        }
        catch (SocketException ex)
        {
            Debug.LogError($"Error retrieving local IP address: {ex.Message}");
        }
        return null;
    }

}
