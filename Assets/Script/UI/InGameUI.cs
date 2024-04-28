using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance;
    
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject spikedBall;
    
    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        spikedBall.SetActive(true);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        OnHealthChange(Player.instance);
        PauseGame();
    }
    
    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            healthText.gameObject.SetActive(false);
        }
    }
    
    void OnHealthChange(Player player)
    {
        healthText.text = player.Health.ToString();
    }
    
    public void OnDeadPanel()
    {
        Time.timeScale = 0;
        deadPanel.SetActive(true);
        healthText.gameObject.SetActive(false);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void Restart2()
    {
        //player.gameObject.transform.position = new Vector3(-11.75f, 0, 0);
        Time.timeScale = 1;
        deadPanel.SetActive(false);
        healthText.gameObject.SetActive(true);
        
        Debug.Log("Restart2");
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void HardMode()
    {
        Player.instance.Health = 1;
        spikedBall.SetActive(true);
    }
    
    public void EasyMode()
    {
        Player.instance.Health = 10;
        spikedBall.SetActive(false);
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        deadPanel.SetActive(false);
        healthText.gameObject.SetActive(true);
        pausePanel.SetActive(false);
    }
}
