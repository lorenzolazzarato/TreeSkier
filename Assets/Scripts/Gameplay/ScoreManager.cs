using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private IdContainerGameEvent _scoreUpdatedEvent;

    private static ScoreManager _instance;

    private int _score = 0;
    private int _multiplier = 1;
    private Coroutine _isMultiActive = null;

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
        _scoreUpdatedEvent.Invoke();
        Debug.Log("Current score: " + _score.ToString());
    }

    public int GetScore() {
        return _score;
    }

    public void SetMultiplier() {
        // if another multiplier is active, stop its coroutine and call a new one, effectively prolonging the effect.
        if(_isMultiActive != null)
        {
            StopCoroutine(_isMultiActive);
            _isMultiActive = null;
        }

        _isMultiActive = StartCoroutine(SetMultiplierCO());

    }

    IEnumerator SetMultiplierCO() {
        _multiplier = 2;
        yield return new WaitForSeconds(10);
        _multiplier = 1;
        Debug.Log("Finished multiplier");
    }
}
