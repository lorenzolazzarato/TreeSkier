using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    private int _score = 0;
    private int _multiplier = 1;

    public static ScoreManager Instance { get { return _instance; } }


    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void AddScore(int points) {
        _score += points * _multiplier;

        Debug.Log("Current score: " + _score.ToString());
    }

    public int GetScore() {
        return _score;
    }

    IEnumerator SetMultiplier() {
        _multiplier = 2;
        yield return new WaitForSeconds(10);
        _multiplier = 1;
    }
}
