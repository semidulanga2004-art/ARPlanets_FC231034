using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ImageTracker : MonoBehaviour
{
// one row in the Inspector: &quot;when you see THIS image, show THIS prefab&quot;
[System.Serializable]
public class ImagePrefab
{
public string imageName; // must match the name in the Reference Image
//Library
public GameObject prefab;
}
public ARTrackedImageManager trackedImageManager; // drag XR Origin here
public List<ImagePrefab> imagePrefabs = new List<ImagePrefab>();
public float heightAboveImage = 0.05f; // 5 cm above the card
[Header("Sound (optional)")]
public AudioSource audioSource;
public AudioClip foundSound;
// remember which object we made for which image
private Dictionary<TrackableId, GameObject> spawned = new Dictionary<TrackableId,
GameObject>();
void OnEnable()
{
trackedImageManager.trackablesChanged.AddListener(OnImagesChanged);
}
void OnDisable()
{
trackedImageManager.trackablesChanged.RemoveListener(OnImagesChanged);
}
// AR Foundation calls this whenever images are found or move
void OnImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
{
foreach (ARTrackedImage image in args.added)
{
SpawnFor(image);
}
foreach (ARTrackedImage image in args.updated)
{
// show the object only while the camera can really see the image
if (spawned.TryGetValue(image.trackableId, out GameObject obj))
{
obj.SetActive(image.trackingState == TrackingState.Tracking);
}
}
}
void SpawnFor(ARTrackedImage image)
{
string name = image.referenceImage.name; // e.g. &quot;Earth&quot;
foreach (ImagePrefab item in imagePrefabs)
{
if (item.imageName == name)
{
// make it a CHILD of the image, so it moves with the card
GameObject obj = Instantiate(item.prefab, image.transform);
obj.transform.localPosition = new Vector3(0f, heightAboveImage, 0f);
spawned[image.trackableId] = obj;
if (audioSource != null && foundSound != null)
audioSource.PlayOneShot(foundSound);
return;
}
}
Debug.Log("No prefab for image: " + name);
}
}