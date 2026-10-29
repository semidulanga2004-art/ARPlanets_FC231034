using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
public class ARUIManager : MonoBehaviour
{
public ARPlaneManager planeManager; // drag XR Origin here
public PlaceOnPlane placeOnPlane; // drag XR Origin here
public TextMeshProUGUI hintText; // the message at the top
private bool planesVisible = true;
void Update()
{
// show a helpful message depending on what is happening
if (planeManager.trackables.count == 0)
{
hintText.text = "Move your phone slowly to scan the floor or a table...";
}
else if (!placeOnPlane.HasPlacedObject)
{
hintText.text = "Surface found! Tap it to place the solar system.";
}
else
{
hintText.text = "Tap a planet for facts. Pinch to resize, twist to rotate.";
}
}
// Button: show / hide the detected surfaces
public void TogglePlanes()
{
planesVisible = !planesVisible;
planeManager.enabled = planesVisible; // stop / start finding new planes
foreach (ARPlane plane in planeManager.trackables)
{
plane.gameObject.SetActive(planesVisible); // hide / show the old ones
}
}
// Button: remove the solar system so it can be placed again
public void ResetScene()
{
placeOnPlane.RemovePlacedObject();
}
}