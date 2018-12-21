using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] GameObject winPopUp, losePopUp;
    
    int lives = 3;
    int souls = 0;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public void Awake()
    {
        _instance = this;
    }

    public int Lives {
        get {
            return lives;
        }
        set {
            lives = value;
            GUIController.Instance.UpdateHeartDisplay(value);
            if(lives <= 0) {
                EndGame(false);
            }
        }
    }

    public int Souls {
        get {
            return souls;
        } 
        set {
            souls = value;
            GUIController.Instance.UpdateSoulsCount(value);
        }
    }

    public void EndGame (bool win) {
        if(win) {
            winPopUp.SetActive(true);
        } else {
            losePopUp.SetActive(true);
        }
    }
}
