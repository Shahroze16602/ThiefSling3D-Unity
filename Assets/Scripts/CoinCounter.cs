using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int coinsCollected = 0;

    private void Start()
    {
        text.text = coinsCollected.ToString();
    }
    public void AddCoin()
    {
        coinsCollected++;
        text.text = coinsCollected.ToString();
    }
}
