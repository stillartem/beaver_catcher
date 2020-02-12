using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameOver;
    private GameObject[] pipes;

    private float difficulty;

    private GameObject menuScreen;

    private GameObject gameOverScreen;

    private GameObject gameScreen;

    private AudioSource audioSource;
    private int score = 0;

    [SerializeField] private GameObject game;
    [SerializeField] private Text scoreText; 
    [SerializeField] private Text gameOverText; 
    [SerializeField] private GameObject beaverPrefab;
    [SerializeField] private AudioClip gameClip;
    [SerializeField] private AudioClip menuClip;
    
    public Texture2D cursorTexture;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(menuClip, 0.2f);
        game = GameObject.Find("Game");

        gameScreen = game.transform.Find("GameScreen").gameObject;
        gameOverScreen = game.transform.Find("GameOverScreen").gameObject;
        menuScreen = game.transform.Find("MenuScreen").gameObject;


        gameOverScreen.SetActive(false);
        gameScreen.SetActive(false);

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);        

    }

    public void GameStart(float difficulty)
    {
         audioSource.Stop();
        audioSource.PlayOneShot(gameClip, 0.2f);
        menuScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(true);
        this.score = 0;

        this.difficulty = difficulty;
        UpdateScore(0);
        gameOver = false;
        StartCoroutine(spawn());
    }

    public void GameOver()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(menuClip, 0.2f);
        gameOver = true;
        gameOverScreen.SetActive(true);
        gameScreen.SetActive(false);
        gameOverText.text = "Game is over You Score is : " + score;

    }

    public void Restart()
    {
        GameStart(difficulty);
    }

    public void GoToMenu()
    {   
        audioSource.Stop();
        audioSource.PlayOneShot(menuClip, 0.2f);
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void UpdateScore(int score) 
    {
        this.score += score;
        scoreText.text = "Your score is: " + this.score;
    }

    IEnumerator spawn()
    {
        while (!gameOver) {
            Instantiate(beaverPrefab, getPipeCoords(), beaverPrefab.transform.rotation);
            yield return new WaitForSeconds(difficulty);
        }
    }

    Vector3 getPipeCoords()
    {
        if (pipes == null) {
            pipes = GameObject.FindGameObjectsWithTag("pipe");
        }
        int pipeIndex = Random.Range(0, pipes.Length);
        Vector3 vector = pipes[pipeIndex].transform.position;
         vector.y = vector.y +1;
        return vector;
    }
}
