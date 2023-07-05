using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour {

    [Header("Colors")]
    [SerializeField] private Color onColorBg;
    [SerializeField] private Color offColorBg;



    protected Toggle _toggle;
    private Vector2 _handlePositionOn;
    private Vector2 _handlePositionOff;
    private Image _backgroundImage;

    [SerializeField] private RectTransform _handle;
    [SerializeField] private float speed;
    static float t = 0.0f;

    protected virtual void Awake() {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnSwitch);
        _handlePositionOn = _handle.anchoredPosition;
        _handlePositionOff = _handle.anchoredPosition * -1;
        _backgroundImage = _handle.parent.GetComponent<Image>();
    }

    protected virtual void OnDestroy() {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }

    public virtual void OnSwitch(bool isOn) {
        if (!isOn) {
            _handle.anchoredPosition = _handlePositionOff;
            _backgroundImage.color = offColorBg;
        } else {
            _handle.anchoredPosition = _handlePositionOn;
            _backgroundImage.color = onColorBg;
        }
    }

    Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX) {

        Vector3 position = new Vector3(Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);

        return position;
    }

    Color SmoothColor(Color startCol, Color endCol) {
        Color resultCol;
        resultCol = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
        return resultCol;
    }

    CanvasGroup Transparency(GameObject alphaObj, float startAlpha, float endAlpha) {
        CanvasGroup alphaVal;
        alphaVal = alphaObj.gameObject.GetComponent<CanvasGroup>();
        alphaVal.alpha = Mathf.Lerp(startAlpha, endAlpha, t += speed * Time.deltaTime);
        return alphaVal;
    }

}
