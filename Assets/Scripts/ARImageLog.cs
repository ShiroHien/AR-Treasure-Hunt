using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ImageTrackingInfo : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public TMP_Text trackingInfoText;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        string trackingInfo = "";

        foreach (var trackedImage in eventArgs.added)
        {
            UpdateTrackingInfo(trackedImage, ref trackingInfo, "Added");
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateTrackingInfo(trackedImage, ref trackingInfo, "Updated");
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            UpdateTrackingInfo(trackedImage, ref trackingInfo, "Removed");
        }

        trackingInfoText.text = trackingInfo;
    }

    private void UpdateTrackingInfo(ARTrackedImage trackedImage, ref string trackingInfo, string action)
    {
        string imageName = trackedImage.referenceImage.name;
        TrackingState trackingState = trackedImage.trackingState;

        trackingInfo += $"{action} '{imageName}': State={trackingState}\n";

        Debug.Log($"{action} image '{imageName}': State={trackingState}");
    }
}
