using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarController : MonoBehaviour
{
  public static NavBarController inst;
  [SerializeField]
  private GameObject dropdown, navBar;
  public GameObject[] blackBars;

  private void Awake()
  {
    inst = this;
  }

  public void ToggleNavBar(bool toggle)
  {
    dropdown.SetActive(!toggle);
    navBar.SetActive(toggle);
  }
}
