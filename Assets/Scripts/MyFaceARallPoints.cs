using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class MyFaceARallPoints : MonoBehaviour
{

    int maxIndex = 467;

    public Text textIndex;
    int index;

    int INDEX
    {
        get { return index; }
        set
        {
            index = value;
            textIndex.text = index.ToString();
        }
    }

    public void OnClickPlus()
    {
        INDEX++;
        if ( INDEX > maxIndex)
        {
            INDEX = 0;
        }
    }

    public void OnclickMinus()
    {
        INDEX--;
        if(INDEX < 0)
        {
            INDEX = maxIndex;
        }
    }

    public GameObject cube;
    ARFaceManager aRFaceManager;

    void Awake()
    {
        aRFaceManager = GetComponent<ARFaceManager>();
        INDEX = 0;
    }

    private void OnEnable()
    {
        aRFaceManager.facesChanged += OnFaceChanged;
    }

    private void OnDisable()
    {
        aRFaceManager.facesChanged -= OnFaceChanged;

    }

    private void OnFaceChanged(ARFacesChangedEventArgs args)
    {
        if (args.updated.Count > 0)
        {
            var face = args.updated[0];
            Vector3 pos = face.vertices[INDEX];
            pos = face.transform.TransformPoint(pos);
            cube.transform.position = pos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
