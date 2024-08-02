using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int coinsCollected = 0;

    public TMP_Text coinsCollectedText;

    private void Start()
    {
        coinsCollectedText.text = coinsCollected.ToString();
    }
    public void AddCoin()
    {
        coinsCollected++;
        coinsCollectedText.text = coinsCollected.ToString();
    }
}
