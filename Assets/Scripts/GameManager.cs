using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //[Header("Ammo Spawning")] public GameObject ammo;
    //[SerializeField] private float ammoRepeatRate = 7.0f;

    [Header("Enemy Spawning")]
    public GameObject enemy;

    [SerializeField]
    private float enemyRepeatRate;

    public float enemySpeed;
    public int enemyKilled;
    public TMP_Text killsText;

    private float cameraSize;

    private Vector2 bottomLeft;

    private new Camera camera;

    private Vector2 topRight;
    private PlayerHealthController playerHealthController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Il y'a plus d'une instance de Game Manager dans la scene");
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
            if (PlayerHealthController.health <= 0)
            {
                CancelInvoke(nameof(SpawnEnemy));
            }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // private void SpawnAmmo() => InstantiateGameObject(ammo);

    private void SpawnEnemy() => InstantiateGameObject(enemy);

    private void InstantiateGameObject(GameObject gameObject) => Instantiate(gameObject, GetRandomCoordinates(), Quaternion.identity);

    private Vector2 GetRandomCoordinates()
    {
        float randomXAxisValue = Random.Range(bottomLeft.x, topRight.x);
        float randomYAxisValue = Random.Range(bottomLeft.y, topRight.y);

        return new Vector2(randomXAxisValue, randomYAxisValue);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        CancelInvoke();
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            camera = Camera.main;

            //Calculate bounds of the screen
            if (camera != null)
            {
                bottomLeft = camera.ViewportToWorldPoint(Vector2.zero);
                topRight = camera.ViewportToWorldPoint(Vector2.one);
                camera.orthographicSize = cameraSize;

                // InvokeRepeating(nameof(SpawnAmmo), 5, ammoRepeatRate);

                InvokeRepeating(nameof(SpawnEnemy), 3, enemyRepeatRate);
            }

            playerHealthController = GameObject.Find("Player").GetComponent<PlayerHealthController>();
            killsText = GameObject.Find("Kills").GetComponent<TextMeshProUGUI>();
        }
    }

    public void StartEasy()
    {
        enemyRepeatRate = 2.0f;
        cameraSize = 3.0f;
        enemySpeed = 2.0f;
        SceneManager.LoadScene("SampleScene");
    }

    public void StartMedium()
    {
        enemyRepeatRate = 1.5f;
        cameraSize = 4.0f;
        enemySpeed = 2.5f;
        SceneManager.LoadScene("SampleScene");
    }

    public void StartHard()
    {
        enemyRepeatRate = 1.0f;
        cameraSize = 4.0f;
        enemySpeed = 3.0f;
        SceneManager.LoadScene("SampleScene");
    }

    public void StartImpossible()
    {
        enemyRepeatRate = 0.5f;
        cameraSize = 4.0f;
        enemySpeed = 4.0f;
        SceneManager.LoadScene("SampleScene");
    }
}