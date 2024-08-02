using UnityEngine;

public class CoinsScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float floatAmplitude = 0.5f;
    [SerializeField] private float floatFrequency = 1f;
    
    private Vector3 startPosition;

    public CoinCounter coinCounter;

    private void Awake()
    {
        LoadGameData();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }


    public void SaveGameData()
    {
        PlayerPrefs.SetInt("CoinsCollected", coinCounter.coinsCollected);
        PlayerPrefs.Save();
    }

    public void LoadGameData()
    {
        if (PlayerPrefs.HasKey("CoinsCollected"))
        {
            coinCounter.coinsCollected = PlayerPrefs.GetInt("CoinsCollected");
            coinCounter.coinsCollectedText.text = coinCounter.coinsCollected.ToString();
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            coinCounter.AddCoin();
            SaveGameData();
            Destroy(gameObject);
        }
    }
}
