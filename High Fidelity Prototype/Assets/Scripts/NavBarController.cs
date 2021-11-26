using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarController : MonoBehaviour
{
  [SerializeField]
  private GameObject dropdown, navBar;

  public void ToggleNavBar(bool toggle)
  {
    dropdown.SetActive(!toggle);
    navBar.SetActive(toggle);
  }
}
