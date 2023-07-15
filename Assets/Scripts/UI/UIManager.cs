using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private ChangeLivesEvent _changeLivesEvent;

    [SerializeField]
    private IntSO _playerStartingLife;

    private TMP_Text _score;
    private HealthBarScript _healthBar;

    

    void OnEnable() {
        _changeLivesEvent.Subscribe(OnChangeLives);
    }

    void OnDisable()
    {
        _changeLivesEvent.Unsubscribe(OnChangeLives);
    }

    void Start() {
        _score = GetComponentInChildren<TMP_Text>();
        
        _healthBar = GetComponentInChildren<HealthBarScript>();

        _healthBar.ClearHearts();
        for (int i = 0; i < _playerStartingLife.value; i+=2) {
            _healthBar.CreateNewHeart();
        }
    }
    // Update is called once per frame
    void Update()
    {
        _score.SetText(ScoreManager.Instance.GetScore().ToString("#,##0"));
    }

    private void UpdateHealth(float health) {
        _healthBar.DrawHearts(health);
    }

    private void OnChangeLives(GameEvent evt) 
    { 
        ChangeLivesEvent changeLivesEvt = evt as ChangeLivesEvent;

        //Debug.LogFormat("Change Lives called - number of life: {0}", changeLivesEvt.numberOfLives);

        if (changeLivesEvt != null)
        {
            UpdateHealth(changeLivesEvt.numberOfLives);
        }
    }
}
