using UnityEngine;
using TMPro;
public class InfoPanel : MonoBehaviour
{
public static InfoPanel instance; // shortcut: InfoPanel.instance.Show(...)
public GameObject panel; // the whole info box
public TextMeshProUGUI titleText;
public TextMeshProUGUI factText;
void Awake()
{
instance = this;
}
void Start()
{
panel.SetActive(false); // hidden at the start
}
public void Show(Planet planet)
{
titleText.text = planet.planetName;
factText.text = planet.fact;
panel.SetActive(true);
}
public void Hide() // also used by the X button
{
panel.SetActive(false);
}
}