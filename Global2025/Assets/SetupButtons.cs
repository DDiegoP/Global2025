using System.Net.Sockets;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupButtons : MonoBehaviour
{
    [SerializeField]
    TMP_InputField _inputField = null;

    [SerializeField]
    Button _button = null;

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        Debug.Log(host.AddressList);
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                _inputField.GetComponentInChildren<TextMeshProUGUI>().text = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetLocalIPAddress();
        _inputField.onEndEdit.AddListener((_input) => { MyNetworkManager.Instance.ChangeAddress(_input); });
        _button.onClick.AddListener(MyNetworkManager.Instance.StartServer);
    }
}
