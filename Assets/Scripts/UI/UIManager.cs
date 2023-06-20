using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private ChangeLivesEvent _changeLivesEvent;

    private TMP_Text _score;
    private HeartScript[] _healthBar;

    

    void OnEnable() {
        _changeLivesEvent.Subscribe(OnChangeLives);
    }

    void OnDisable()
    {
        _changeLivesEvent.Unsubscribe(OnChangeLives);
    }

    void Start() {
        _score = GetComponentInChildren<TMP_Text>();
        //populate health bar list
        _healthBar = GetComponentsInChildren<HeartScript>();
        Debug.Log("List size is " + _healthBar.Length);
    }
    // Update is called once per frame
    void Update()
    {
        _score.SetText("Score: " + ScoreManager.Instance.GetScore());
        //DrawHearts(6);
    }

    private void DrawHearts(float health) {

        // a full heart is 2, half is 1, empty is 0. So 9 health = 4 and a half hearts.
        int i;
        int wholeHearts = (int) health / 2;

        for (i = 0; i < wholeHearts; i++) {
            _healthBar[i].ChangeSprite(HeartValue.Full);
        }
        if(health % 2 == 1) {
            _healthBar[i].ChangeSprite(HeartValue.Half);
            i++;
        }
        for (; i < _healthBar.Length; i++) {
            _healthBar[i].ChangeSprite(HeartValue.Empty);
        }

    }

    private void OnChangeLives(GameEvent evt) 
    { 
        ChangeLivesEvent changeLivesEvt = evt as ChangeLivesEvent;

        Debug.LogFormat("Change Lives called - number of life: {0}", changeLivesEvt.numberOfLives);

        if (changeLivesEvt != null)
        {
            DrawHearts(changeLivesEvt.numberOfLives);
        }
    }
}
