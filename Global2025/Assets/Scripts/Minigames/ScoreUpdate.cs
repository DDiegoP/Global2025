using TMPro;
using UnityEngine;

public class ScoreUpdate : GameComponent
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = _manager.score.ToString();
    }
}
