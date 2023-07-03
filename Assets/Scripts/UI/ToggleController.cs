using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleController : MonoBehaviour 
{
	/**	ToggleController:
	*	prefab scaricato da GitHub; permette di animare un toggle nei prefab e
	*	nella funzione StopSwitching scatena due eventi: se toggle è off o se toggle è on
	*	ATTENZIONE: se si vuole cambiare il nome ai gameobject dei toggle, cambiare anche
	*	il loro nome che ToggleController cerca nella funzione Start().
	*/
	[SerializeField] private bool isOn;

    [SerializeField] private Color onColorBg;
    [SerializeField] private Color offColorBg;

	[SerializeField] private Image toggleBgImage;
	[SerializeField] private RectTransform toggle;

	[SerializeField] private GameObject handle;
	private RectTransform handleTransform;

	private float handleSize;
	private float onPosX;
	private float offPosX;

	[SerializeField] private float handleOffset;

	[SerializeField] private GameObject onIcon;
	[SerializeField] private GameObject offIcon;

	[SerializeField] private float speed;
	static float t = 0.0f;

	private bool switching = false;


	[SerializeField] private UnityEvent imOn;
	[SerializeField] private UnityEvent imOff;


	void Awake()
	{
		handleTransform = handle.GetComponent<RectTransform>();
		RectTransform handleRect = handle.GetComponent<RectTransform>();
		handleSize = handleRect.sizeDelta.x;
		float toggleSizeX = toggle.sizeDelta.x;
		onPosX = (toggleSizeX / 2) - (handleSize/2) - handleOffset;
		offPosX = -onPosX;
	}


	void Start()
	{
		if (this.gameObject.name.Equals("VolumeToggle"))
		{
			if (PlayerPrefs.GetInt("muted") == 1) isOn = false;
			else isOn = true;
		}
		if (this.gameObject.name.Equals("SFXToggle"))
		{
			if (PlayerPrefs.GetInt("mutedSFX") == 1) isOn = false;
			else isOn = true;
		}

		if(isOn)
		{
			toggleBgImage.color = onColorBg;
			handleTransform.localPosition = new Vector3(onPosX, 0f, 0f);
		}
		else
		{
			toggleBgImage.color = offColorBg;
			handleTransform.localPosition = new Vector3(offPosX, 0f, 0f);
		}
	}
		
	void Update()
	{
		if(switching)
		{
			Toggle(isOn);
		}
	}

	public void Switching()
	{
		switching = true;
	}

	public void Toggle(bool toggleStatus)
	{
		if(toggleStatus)
		{
			toggleBgImage.color = SmoothColor(onColorBg, offColorBg);
			handleTransform.localPosition = SmoothMove(handle, onPosX, offPosX);
		}
		else 
		{
			toggleBgImage.color = SmoothColor(offColorBg, onColorBg);
			handleTransform.localPosition = SmoothMove(handle, offPosX, onPosX);
		}
			
	}

	Vector3 SmoothMove(GameObject toggleHandle, float startPosX, float endPosX)
	{
		
		Vector3 position = new Vector3 (Mathf.Lerp(startPosX, endPosX, t += speed * Time.deltaTime), 0f, 0f);
		StopSwitching();
		return position;
	}

	Color SmoothColor(Color startCol, Color endCol)
	{
		Color resultCol;
		resultCol = Color.Lerp(startCol, endCol, t += speed * Time.deltaTime);
		return resultCol;
	}

	CanvasGroup Transparency (GameObject alphaObj, float startAlpha, float endAlpha)
	{
		CanvasGroup alphaVal;
		alphaVal = alphaObj.gameObject.GetComponent<CanvasGroup>();
		alphaVal.alpha = Mathf.Lerp(startAlpha, endAlpha, t += speed * Time.deltaTime);
		return alphaVal;
	}

	//In questa funzione, nei due casi, metto quel che voglio che accada quando il toggle è true o false.
	void StopSwitching()
	{
		if(t > 1.0f)
		{
			switching = false;
			t = 0.0f;

			switch(isOn)
			{
			case true:
				isOn = false;
				imOff.Invoke();
				break;

			case false:
				isOn = true;
				imOn.Invoke();
				break;
			}

		}
	}

}
