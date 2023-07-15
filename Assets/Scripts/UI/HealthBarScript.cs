using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _heartPrefab;

    private List<HeartScript> hearts;

    // deletes all hearts in my health bar
    public void ClearHearts() {
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartScript>();
    }

    // creates a new full heart in my health bar
    public void CreateNewHeart() {
        GameObject newHeart = Instantiate(_heartPrefab);
        newHeart.transform.SetParent(transform);
        
        HeartScript heartScript = newHeart.GetComponent<HeartScript>();
        //heartScript.ChangeSprite(HeartValue.Empty); si rompe tutto non so perche LOL

        hearts.Add(heartScript);
    }

    public void DrawHearts(float health) {
        // a full heart is 2, half is 1, empty is 0. So 9 health = 4 and a half hearts.
        int i;
        int wholeHearts = (int)health / 2;

        for (i = 0; i < wholeHearts; i++) {
            hearts[i].ChangeSprite(HeartValue.Full);
        }
        if (health % 2 == 1) {
            hearts[i].ChangeSprite(HeartValue.Half);
            i++;
        }
        for (; i < hearts.Count; i++) {
            hearts[i].ChangeSprite(HeartValue.Empty);
        }

    }
}
