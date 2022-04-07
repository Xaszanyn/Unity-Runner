using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _junk;
    public static Transform junk;
    [SerializeField] Detection detection;

    [SerializeField] PlayerMovement pMovement;

    [SerializeField] CanvasGroup fail;
    [SerializeField] Transform shineFail;

    [SerializeField] CanvasGroup success;
    [SerializeField] Transform shineSuccess;

    [SerializeField] ParticleSystem confettiLeft;
    [SerializeField] ParticleSystem confettiRight;

    void Awake()
    {
        junk = _junk;
    }

    void Start()
    {
        detection.StartCollect();
    }

    public void Fail()
    {
        float x = 0;
        DOTween.To(() => x, x => fail.alpha = x, 1, 1).SetDelay(1);

        Rotate();

        void Rotate()
        {
            shineFail.DORotate(new Vector3(0, 0, -120), 2).SetEase(Ease.Linear)
                .OnComplete(() => shineFail.DORotate(new Vector3(0, 0, -240), 2).SetEase(Ease.Linear)
                    .OnComplete(() => shineFail.DORotate(new Vector3(0, 0, 0), 2).SetEase(Ease.Linear)
                        .OnComplete(Rotate)));
        }
    }

    public void Success()
    {
        confettiLeft.Play();
        confettiRight.Play();

        float x = 0;
        DOTween.To(() => x, x => success.alpha = x, 1, 1).SetDelay(1);

        Rotate();

        void Rotate()
        {
            shineSuccess.DORotate(new Vector3(0, 0, -120), 2).SetEase(Ease.Linear)
                .OnComplete(() => shineSuccess.DORotate(new Vector3(0, 0, -240), 2).SetEase(Ease.Linear)
                    .OnComplete(() => shineSuccess.DORotate(new Vector3(0, 0, 0), 2).SetEase(Ease.Linear)
                        .OnComplete(Rotate)));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }
}
