using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; // import the XR libraries

  

public class TrackedImageColor : MonoBehaviour
{

	// add a field to the unity ui where you can add the 'XR Tracked Image Manager'
	[SerializeField]
	ARTrackedImageManager m_TrackedImageManager;
	
	// setup callbacks
	void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;
	void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

  

	// function called whenevr there is a change in the scene.
	
	void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
	{
		// iterate over added event changes, getting a single image change per step.
		foreach (var newImage in eventArgs.added)
		{
			// priniting out the names/tags of the images
			Debug.Log(newImage.referenceImage.name);
		}
	}

}