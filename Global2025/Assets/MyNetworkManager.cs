using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyNetworkManager : MonoBehaviour
{
    #region Singleton
    public static MyNetworkManager Instance { get; private set; } = null;

    public void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    #endregion

    public enum USER_TYPE { Server, Client };

    [SerializeField]
    USER_TYPE _userType = USER_TYPE.Server;

    [SerializeField]
    GameObject _serverScene;

    [SerializeField]
    GameObject _clientScene;

    NetworkManager _manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _manager = GetComponent<NetworkManager>();

        // Este es el servidor donde suceden las cosas :)
        if (_userType == USER_TYPE.Server)
        {
            Instantiate(_serverScene);
        }
        // Este claramente no lo es
        else
        {
            Instantiate(_clientScene);
        }
    }

    public void ChangeAddress(TMP_InputField field)
    {
        // Debug.Log("mecachis " + field.text);
        _manager.networkAddress = field.text;
    }

    public void ChangeAddress(string address)
    {
        _manager.networkAddress = address;
    }

    public void StartServer()
    {
        _manager.StartServer();
    }

    public void Host()
    {
        _manager.StartHost();
    }
    public void JoinServer()
    {
        _manager.StartClient();
    }
}
