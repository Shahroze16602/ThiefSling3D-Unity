using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public int coinsCollected = 0;
    public AudioSource coinAudioSource;
    public Text coinsCollectedText;

    private void Start()
    {
        coinsCollectedText.text = coinsCollected.ToString();
    }
    public void AddCoin()
    {
        coinsCollected++;
        coinAudioSource.Play();
        coinsCollectedText.text = coinsCollected.ToString();
    }
}
