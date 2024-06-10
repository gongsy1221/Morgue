using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu{
	public class UISettingsManager : MonoBehaviour
	{

		public enum Platform { Desktop, Mobile };
		public Platform platform;
		// toggle buttons
		[Header("MOBILE SETTINGS")]
		public GameObject mobileSFXtext;
		public GameObject mobileMusictext;
		public GameObject mobileShadowofftextLINE;
		public GameObject mobileShadowlowtextLINE;
		public GameObject mobileShadowhightextLINE;

		[Header("VIDEO SETTINGS")]
		public GameObject fullscreentext;
		public GameObject ambientocclusiontext;
		public GameObject shadowofftextLINE;
		public GameObject shadowlowtextLINE;
		public GameObject shadowhightextLINE;
		public GameObject aaofftextLINE;
		public GameObject aa2xtextLINE;
		public GameObject aa4xtextLINE;
		public GameObject aa8xtextLINE;
		public GameObject vsynctext;
		public GameObject motionblurtext;
		public GameObject texturelowtextLINE;
		public GameObject texturemedtextLINE;
		public GameObject texturehightextLINE;
		public GameObject cameraeffectstext;

		[Header("GAME SETTINGS")]
		public GameObject showhudtext;
		public GameObject tooltipstext;
		public GameObject difficultynormaltext;
		public GameObject difficultynormaltextLINE;
		public GameObject difficultyhardcoretext;
		public GameObject difficultyhardcoretextLINE;

		[Header("CONTROLS SETTINGS")]
		public GameObject invertmousetext;

		// sliders
		public GameObject musicSlider;
		public GameObject sensitivityXSlider;
		public GameObject sensitivityYSlider;
		public GameObject mouseSmoothSlider;

		private float sliderValue = 0.0f;
		private float sliderValueXSensitivity = 0.0f;
		private float sliderValueYSensitivity = 0.0f;
		private float sliderValueSmoothing = 0.0f;


		public void Start()
		{

		}

		public void Update()
		{
			//sliderValue = musicSlider.GetComponent<Slider>().value;
			//slidervaluexsensitivity = sensitivityxslider.getcomponent<slider>().value;
			//slidervalueysensitivity = sensitivityyslider.getcomponent<slider>().value;
			//slidervaluesmoothing = mousesmoothslider.getcomponent<slider>().value;
		}

		public void FullScreen()
		{
			Screen.fullScreen = !Screen.fullScreen;

			if (Screen.fullScreen == true)
			{
				fullscreentext.GetComponent<TMP_Text>().text = "on";
			}
			else if (Screen.fullScreen == false)
			{
				fullscreentext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void MusicSlider()
		{
			//PlayerPrefs.SetFloat("MusicVolume", sliderValue);
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
		}


		public void MobileMusicMute()
		{
			if (PlayerPrefs.GetInt("Mobile_MuteMusic") == 0)
			{
				PlayerPrefs.SetInt("Mobile_MuteMusic", 1);
				mobileMusictext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("Mobile_MuteMusic") == 1)
			{
				PlayerPrefs.SetInt("Mobile_MuteMusic", 0);
				mobileMusictext.GetComponent<TMP_Text>().text = "off";
			}
		}

	}
}