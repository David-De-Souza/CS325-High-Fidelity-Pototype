using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrnTransition : MonoBehaviour
{
  public static ScrnTransition inst;
  public Image fader;
  [SerializeField]
  private float defaultFadeSpeed = 1.0f;
  [SerializeField]
  Queue<IEnumerator> fadeQueue = new Queue<IEnumerator>();
  static bool fading = false;
  public static bool CanProceed()
  { return !fading; }

  private void Awake()
  {
    inst = this;
    StartCoroutine(Loop());
  }
  private void Start()
  {
    FadeIn();
  }
  public void FadeIn(float speed = 1)
  {
    speed = Mathf.Clamp(speed, 1, 10);
    fadeQueue.Enqueue(FadeIn(fader, speed));
    fadeQueue.Enqueue(ToggleFader(false));
  }
  public void FadeOut(float speed = 1)
  {
    speed = Mathf.Clamp(speed, 1, 10);
    fadeQueue.Enqueue(ToggleFader(true));
    fadeQueue.Enqueue(FadeOut(fader, speed));
  }
  public void EnqueueEvent(IEnumerator e)
  {
    fadeQueue.Enqueue(e);
  }
  IEnumerator FadeIn(Image img, float speed)
  {
    fading = true;
    for (float t = 0; t <= 1; t += Time.deltaTime * speed * defaultFadeSpeed)
    {
      img.color = Color.Lerp(Color.black, Color.clear, t);
      yield return null;
    }
    img.color = Color.clear;
    fading = false;
  }
  IEnumerator FadeOut(Image img, float speed)
  {
    fading = true;
    for (float t = 0; t <= 1; t += Time.deltaTime * speed * defaultFadeSpeed)
    {
      img.color = Color.Lerp(Color.clear, Color.black, t);
      yield return null;
    }
    img.color = Color.black;
    fading = false;
  }
  IEnumerator ToggleFader(bool boolean)
  {
    fader.gameObject.SetActive(boolean);
    yield return null;
  }
  IEnumerator Loop()
  {
    while (true)
    {
      while (fadeQueue.Count > 0)
      {
        yield return new WaitUntil(CanProceed);
        yield return StartCoroutine(fadeQueue.Dequeue());
        yield return new WaitUntil(CanProceed);
      }
      yield return null;
    }
  }
}
