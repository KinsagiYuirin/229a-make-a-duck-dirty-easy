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
    
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown("escape"))
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
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void HardMode()
    {
        Player.instance.Health = 1;
    }
    
    public void EasyMode()
    {
        Player.instance.Health = 10;
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        deadPanel.SetActive(false);
        healthText.gameObject.SetActive(true);
        pausePanel.SetActive(false);
    }
}
