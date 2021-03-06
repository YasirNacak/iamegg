﻿using System.Collections.Generic;
using Assets.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Panels
{
    public class MainMenuPanel : GUIPanel
    {
        public GameObject TitleText;
        public GameObject PlayButton;
        public GameObject PlayText;
        public GameObject HowToPlayButton;

        private RectTransform TitleTextRectTransform;
        private RectTransform PlayButtonRectTransform;
        private RectTransform PlayTextRectTransform;
        private RectTransform HowToPlayButtonRectTransform;

        private float TitleTextDefaultY;
        private float PlayButtonDefaultY;
        private float PlayTextDefaultY;
        private float HowToPlayButtonDefaultY;

        public Vector3 PlayButtonSmallScale;

        public List<Sprite> PlayButtonStages;

        private int _clicks;

        public void Awake()
        {
            IsInitialized = false;
        }

        public override void Initialize()
        {
            TitleTextRectTransform = TitleText.GetComponent<RectTransform>();
            PlayButtonRectTransform = PlayButton.GetComponent<RectTransform>();
            PlayTextRectTransform = PlayText.GetComponent<RectTransform>();
            HowToPlayButtonRectTransform = HowToPlayButton.GetComponent<RectTransform>();

            TitleTextDefaultY = TitleTextRectTransform.anchoredPosition.y;
            PlayButtonDefaultY = PlayButtonRectTransform.anchoredPosition.y;
            PlayTextDefaultY = PlayTextRectTransform.anchoredPosition.y;
            HowToPlayButtonDefaultY = HowToPlayButtonRectTransform.anchoredPosition.y;

            InnerObjects = new List<GameObject> { TitleText, PlayButton, PlayText };

            _clicks = 0;

            IsInitialized = true;
        }

        public override void Open()
        {
            LeanTween.moveY(TitleTextRectTransform, TitleTextDefaultY + 1200, 0);
            LeanTween.moveY(PlayButtonRectTransform, PlayButtonDefaultY + 1200, 0);
            LeanTween.moveY(PlayTextRectTransform, PlayTextDefaultY - 1200, 0);
            LeanTween.moveY(HowToPlayButton, HowToPlayButtonDefaultY - 1200, 0);
            LeanTween.move(gameObject, gameObject.transform.position, 0).setOnComplete(() =>
            {
                base.Open();
                LeanTween.moveY(TitleTextRectTransform, TitleTextDefaultY, GUIManager.Instance.TransitionTime).setEaseOutSine();
                LeanTween.moveY(PlayButtonRectTransform, PlayButtonDefaultY, GUIManager.Instance.TransitionTime).setEaseOutSine();
                LeanTween.moveY(PlayTextRectTransform, PlayTextDefaultY, GUIManager.Instance.TransitionTime).setEaseOutSine();
                LeanTween.moveY(HowToPlayButtonRectTransform, HowToPlayButtonDefaultY, GUIManager.Instance.TransitionTime).setEaseOutSine();
            });
        }

        public override void Close()
        {
            LeanTween.moveY(TitleTextRectTransform, TitleTextDefaultY + 1200, GUIManager.Instance.TransitionTime).setEaseInSine();
            LeanTween.moveY(PlayButtonRectTransform, PlayButtonDefaultY + 1200, GUIManager.Instance.TransitionTime).setEaseInSine();
            LeanTween.moveY(PlayTextRectTransform, PlayTextDefaultY - 1200, GUIManager.Instance.TransitionTime).setEaseInSine();
            LeanTween.moveY(HowToPlayButtonRectTransform, HowToPlayButtonDefaultY - 1200, GUIManager.Instance.TransitionTime).setEaseInSine();
            LeanTween.move(gameObject, gameObject.transform.position, GUIManager.Instance.TransitionWaitTime).setOnComplete(() =>
            {
                base.Close();
                PlayButton.GetComponent<Image>().sprite = PlayButtonStages[0];
                LeanTween.moveY(TitleTextRectTransform, TitleTextDefaultY, 0);
                LeanTween.moveY(PlayButtonRectTransform, PlayButtonDefaultY, 0);
                LeanTween.moveY(PlayTextRectTransform, PlayTextDefaultY, 0);
                LeanTween.moveY(HowToPlayButtonRectTransform, HowToPlayButtonDefaultY, 0);
            });
        }

        public void OnPlayButtonClicked()
        {
            if (_clicks < 3)
            {
                _clicks++;
            }

            if (_clicks == 3)
            {
                _clicks = 0;
                Close();
                GUIManager.Instance.OpenPanel(GUIManager.Instance.InGamePanel);
                GameManager.Instance.StartGame();
            }
            else
            {
                PlayButton.GetComponent<Image>().sprite = PlayButtonStages[_clicks];
            }

        }
        public void OnPlayButtonPointerDown()
        {
            LeanTween.scale(PlayButtonRectTransform, PlayButtonSmallScale, 0.1f).setEaseInBack();
        }

        public void OnPlayButtonPointerUp()
        {
            LeanTween.scale(PlayButtonRectTransform, Vector3.one, 0.1f).setEaseInBack();
        }

        public void OnHowToPlayButtonClicked()
        {
            _clicks = 0;
            GUIManager.Instance.OpenPanel(GUIManager.Instance.HowToPlayPanel);
        }

        public void OnHowToPlayButtonPointerDown()
        {
            LeanTween.scale(HowToPlayButtonRectTransform, PlayButtonSmallScale, 0.1f).setEaseInBack();
        }

        public void OnHowToPlayButtonPointerUp()
        {
            LeanTween.scale(HowToPlayButtonRectTransform, Vector3.one, 0.1f).setEaseInBack();
        }
    }
}
