using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;
    public int baseSeed;


    int prevRoomPlayerHealth;
    int prevRoomPlayerCoin;
    Player player;

    public static GameManager instance;


    // Start is called before the first frame update
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        level = 1;
        baseSeed = PlayerPrefs.GetInt("Seed");
        Random.InitState(baseSeed);
        Generation.instance.Generate();
        UI.instance.UpdateLevelText(level);

        player = FindObjectOfType<Player>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void GoToNextLevel()
    {
        prevRoomPlayerHealth = player.currentHP;
        prevRoomPlayerCoin = player.coins;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Game")
        {
            Destroy(gameObject);
            return;
        }

        player = FindObjectOfType<Player>();
        level++;
        baseSeed += 4;
        Generation.instance.Generate();

        player.currentHP = prevRoomPlayerHealth;
        player.coins = prevRoomPlayerCoin;

        UI.instance.UpdateHealth(prevRoomPlayerHealth);
        UI.instance.UpdateCoinText(prevRoomPlayerCoin);
        UI.instance.UpdateLevelText(level);
    }
}
