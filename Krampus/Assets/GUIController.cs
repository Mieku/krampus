using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite heartFull, heartEmpty;
    [SerializeField] Text soulsCount;
    [SerializeField] int jumpCost, runCost;

    private static GUIController _instance;

    public static GUIController Instance
    {
        get { return _instance; }
    }

    public void Awake()
    {
        _instance = this;
    }

    public void Start() {
        UpdateSoulsCount(0);

        UpdateHeartDisplay(GameManager.Instance.Lives);
    }

    public void UpdateHeartDisplay(int life) {
        foreach(Image heart in hearts) {
            heart.sprite = heartEmpty;
        }

        switch(life) {
            case 1:
                hearts[0].sprite = heartFull;
                hearts[1].sprite = heartEmpty;
                hearts[2].sprite = heartEmpty;
                return;
            case 2:
                hearts[0].sprite = heartFull;
                hearts[1].sprite = heartFull;
                hearts[2].sprite = heartEmpty;
                return;
            case 3:
                hearts[0].sprite = heartFull;
                hearts[1].sprite = heartFull;
                hearts[2].sprite = heartFull;
                return;
            default:
                hearts[0].sprite = heartEmpty;
                hearts[1].sprite = heartEmpty;
                hearts[2].sprite = heartEmpty;
                return;
        }
    }

    public void UpdateSoulsCount(int numSouls) {
        string msg = "Souls Devoured: ";
        msg += numSouls;
        soulsCount.text = msg;
    }



    public void JumpPressed() {
        if(GameManager.Instance.Souls > jumpCost) {
            Debug.Log("Jump!");
            GameManager.Instance.Souls = GameManager.Instance.Souls - jumpCost;
        }
    }

    public void RunPressed() {
        if(GameManager.Instance.Souls > runCost) {
            Debug.Log("RUUNNNN!");
            GameManager.Instance.Souls = GameManager.Instance.Souls - runCost;
        }
    }

    public void CheatWin() {
        GameManager.Instance.EndGame(true);
    }

    public void CheatLose() {
        GameManager.Instance.EndGame(false);
    }
}
