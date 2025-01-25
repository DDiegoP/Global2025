using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupButtonsClient : MonoBehaviour
{
    [SerializeField]
    TMP_InputField _inputField = null;

    [SerializeField]
    Button _button = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputField.onEndEdit.AddListener((_input) => { MyNetworkManager.Instance.ChangeAddress(_input); });
        _button.onClick.AddListener(MyNetworkManager.Instance.JoinServer);
    }
}
