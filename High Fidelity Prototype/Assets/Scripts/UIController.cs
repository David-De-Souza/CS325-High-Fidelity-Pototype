    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIController : MonoBehaviour
    {
    public GameObject MainScreen;
    public GameObject Library;
    public GameObject Geography, Biology, Chemistry, Physics;
    public GameObject Saved;
    public GameObject DownloadButton;
    public Text PageCounter;
    public Text Description;
    public Renderer CurrentModel;
    public Button LibraryButton;

    public Texture[] Models;
    public Sprite BackSprite;
    public Sprite LibrarySprite;

    bool IsLocalSimulation = false;

    public int page_count;
    int page_number = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeModel();
    }

    void ChangeModel()
    {
        if (page_number == 1)
        {
            CurrentModel.material.mainTexture = Models[0];
            Description.text = "An oxbow lake starts out as a curve, or meander, in a river.\nA lake forms as the river finds a different, shorter, course.\nThe meander becomes an oxbow lake along the side of the river.\n\nOxbow lakes usually form in flat, low - lying plains close to\nwhere the river empties into another body of water.\nOn these plains, rivers often have wide meanders.";
        }
        else if (page_number == 2)
        {
            CurrentModel.material.mainTexture = Models[1];
            Description.text = "During a flood, the rapid flow of high water\nmight cut a new channel across the meander.";
        }
        else if (page_number == 3)
        {
            CurrentModel.material.mainTexture = Models[2];
            Description.text = "As sediment is deposited along the edges of the\nnew channel, the former meander might be\nisolated from the river and form an oxbow lake.";
        }
    }

    public void ToLibrary()
    {
        ScrnTransition.inst.FadeOut();
        ScrnTransition.inst.EnqueueEvent(ToLibraryEvent());
        ScrnTransition.inst.FadeIn();
    }
    IEnumerator ToLibraryEvent()
    {
        if (IsLocalSimulation)
        {
            MainScreen.SetActive(false);
            Geography.SetActive(true);
        }
        else
        {
            MainScreen.SetActive(false);
            Library.SetActive(true);
        }
        yield return null;
    }

    public void ToSubject(GameObject subject)
    {
        ScrnTransition.inst.FadeOut();
        ScrnTransition.inst.EnqueueEvent(ToSubjectEvent(subject));
        ScrnTransition.inst.FadeIn();
    }
    IEnumerator ToSubjectEvent(GameObject subject)
    {
        Library.SetActive(false);
        subject.SetActive(true);
        yield return null;
    }

    public void Next()
    {
        if (page_number == page_count)
            page_number = 1;
        else
            ++page_number;

        PageCounter.text = page_number + "/3";
    }

    public void Back()
    {
        if (page_number == 1)
            page_number = page_count;
        else
            --page_number;

        PageCounter.text = page_number + "/3";
    }

    public void ToMainFromLibrary()
    {
        ScrnTransition.inst.FadeOut();
        ScrnTransition.inst.EnqueueEvent(ToMainFromLibraryEvent());
        ScrnTransition.inst.FadeIn();
    }
    IEnumerator ToMainFromLibraryEvent()
    {
        IsLocalSimulation = false;
        LibraryButton.image.sprite = LibrarySprite;
        Library.SetActive(false);
        DownloadButton.SetActive(true);
        MainScreen.SetActive(true);
        yield return null;
    }

    public void ToLibraryFromGeography()
    {
        ScrnTransition.inst.FadeOut();
        ScrnTransition.inst.EnqueueEvent(ToLibraryFromGeographyEvent());
        ScrnTransition.inst.FadeIn();
    }

    IEnumerator ToLibraryFromGeographyEvent()
    {
        Geography.SetActive(false);
        Biology.SetActive(false);
        Chemistry.SetActive(false);
        Physics.SetActive(false);
        Library.SetActive(true);
        yield return null;
    }

    public void Download()
    {
        StartCoroutine("SavedToLibrary");
    }

    IEnumerator SavedToLibrary()
    {
        Saved.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Saved.SetActive(false);
    }

    public void ToMainFromGeography()
    {
        ScrnTransition.inst.FadeOut();
        ScrnTransition.inst.EnqueueEvent(ToMainFromGeographyEvent());
        ScrnTransition.inst.FadeIn();
    }
    IEnumerator ToMainFromGeographyEvent()
    {
        Geography.SetActive(false);
        Biology.SetActive(false);
        Chemistry.SetActive(false);
        Physics.SetActive(false);
        DownloadButton.SetActive(false);
        MainScreen.SetActive(true);
        IsLocalSimulation = true;
        LibraryButton.image.sprite = BackSprite;
        yield return null;
    }

    public void DropdownScreen1()
    {
        page_number = 1;
    }
    public void DropdownScreen2()
    {
        page_number = 2;
    }
    public void DropdownScreen3()
    {
        page_number = 3;
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}