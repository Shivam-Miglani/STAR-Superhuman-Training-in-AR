using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class XRay : MonoBehaviour {
    public Material PathMaterial;
    public Material XRayMaterial;
    private int MaxViewingDistance = 2;
    public LayerMask mask;
    private int Frames;
    private GameObject[] path;
    public static bool XRayActive = false;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start () {
        //Create keywords for keyword recognizer
        keywords.Add("activate", () =>
        {
            this.ActivateXRay();
        });

        keywords.Add("stop", () =>
        {
            this.DeactivateXRay();
        });

        path = GameObject.FindGameObjectsWithTag("Path");
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;

        if(XRayActive)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, MaxViewingDistance, mask))
            {
                hit.collider.GetComponent<Renderer>().material = XRayMaterial;
            }
            
            
            foreach (GameObject g in path)
            {
                if (!g.name.Equals(hit.transform.gameObject.name)){
                    g.GetComponent<Renderer>().material = PathMaterial;
                }
            }    
        }
    }

    public void ActivateXRay()
    {
        XRayActive = true;
        Debug.Log("XRAY ACTIVATED!!");
    }

    public void DeactivateXRay()
    {
        XRayActive = false;
        foreach (GameObject g in path)
        {
            g.GetComponent<Renderer>().material = PathMaterial;
         }
        Debug.Log("XRAY ACTIVATED!!");
    }
}
