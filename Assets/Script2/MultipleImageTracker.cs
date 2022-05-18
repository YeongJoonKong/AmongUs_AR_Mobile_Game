using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImageTracker : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    public static int clearAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnTrackedImage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            string imageName = trackedImage.referenceImage.name;

            GameObject imagePrefab = Resources.Load<GameObject>(imageName);

            if (imagePrefab != null)
            {
                if (trackedImage.transform.childCount < 1)
                {
                    GameObject go = Instantiate(imagePrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    go.transform.SetParent(trackedImage.transform);

                    if (imageName.Equals("Emergency"))
                    {
                        SoundManager.instance.EmergencyPlay();
                    }
                }
            }
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
            }

            if (clearAsteroid < 5 && Resources.Load<GameObject>(trackedImage.referenceImage.name) != null && trackedImage.referenceImage.name == "Arsenal")
            {
                if (trackedImage.transform.GetChild(0).GetChild(0).childCount < 1)
                {
                    Destroy(trackedImage.transform.GetChild(0).gameObject);
                    clearAsteroid++;
                    GameObject go = Instantiate(Resources.Load<GameObject>(trackedImage.referenceImage.name), trackedImage.transform.position, trackedImage.transform.rotation);
                    go.transform.SetParent(trackedImage.transform);
                }
            }
        }
    }
}
