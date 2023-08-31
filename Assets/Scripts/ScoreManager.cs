using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Manager")]
    public int playerKills;
    public int enemyKills;
    public Text playerKillText;
    public Text enemyKillText;
    public Text mainText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("playerKills"))
        {
            playerKills = PlayerPrefs.GetInt("0");
        }
        else if (PlayerPrefs.HasKey("enemyKills"))
        {
            enemyKills = PlayerPrefs.GetInt("0");
        }
    }

    private void Update()
    {
        StartCoroutine(WinOrLose());
    }

    IEnumerator WinOrLose()
    {
        playerKillText.text = "" + playerKills;
        enemyKillText.text = "" + enemyKills;

        if(playerKills >= 10)
        {
            mainText.text = "Bolu Team Win";
            PlayerPrefs.SetInt("playerKills", playerKills);
            Time.timeScale = 0f;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("0");
        }
        else if (enemyKills >= 10)
        {
            mainText.text = "Enemy Team Win";
            PlayerPrefs.SetInt("enemyKills", enemyKills);
            Time.timeScale = 0f;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("0");
        }
    }
}
