using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlaceOnPlane : MonoBehaviour
{
public ARRaycastManager raycastManager;
public GameObject objectToPlace;
[Header("Sounds (optional)")]
public AudioSource audioSource; // on XR Origin
public AudioClip placeSound;
public AudioClip selectSound;
private GameObject placedObject;
private List<ARRaycastHit> hits = new List<ARRaycastHit>();
public bool HasPlacedObject => placedObject != null; // used by the UI
void Update()
{
if (!TryGetTap(out Vector2 tapPosition)) return;
// A. Ignore taps on UI buttons
if (IsTapOnUI()) return;
// B. Did the tap hit a planet? -&gt; select it and show its info
Ray ray = Camera.main.ScreenPointToRay(tapPosition);
if (Physics.Raycast(ray, out RaycastHit hit))
{
Planet planet = hit.collider.GetComponentInParent<Planet>();
if (planet != null)
{
InfoPanel.instance.Show(planet);
PlaySound(selectSound);
return;
}
}
// C. Otherwise: place or move the object on a real surface
if (raycastManager.Raycast(tapPosition, hits, TrackableType.PlaneWithinPolygon))
{
Pose hitPose = hits[0].pose;
if (placedObject == null)
{
placedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
}
else
{
placedObject.transform.position = hitPose.position;
}
PlaySound(placeSound);
}
}
// called by the Reset button
public void RemovePlacedObject()
{
if (placedObject != null) Destroy(placedObject);
InfoPanel.instance.Hide();
}
bool TryGetTap(out Vector2 position)
{
position = Vector2.zero;
if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
{
position = Input.GetTouch(0).position;
return true;
}
if (Input.touchCount == 0 && Input.GetMouseButtonDown(0))
{
position = Input.mousePosition;
return true;
}
return false;
}
bool IsTapOnUI()
{
if (EventSystem.current == null) return false;
if (Input.touchCount > 0)
return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
return EventSystem.current.IsPointerOverGameObject();
}
void PlaySound(AudioClip clip)
{
if (audioSource != null &&clip != null) audioSource.PlayOneShot(clip);
}
}