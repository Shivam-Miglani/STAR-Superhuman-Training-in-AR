using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    public GameObject EnergyObject;
    public LayerMask EnergyMask;
    public int MaxItemsSpawned; // Maximum number of Energy pickups at the same time available in-game.
    public const int MaxEnergy = 100;
    public int CurrentEnergy;
    public int EnergyPickupWorth;
    public float EnergyAlpha = 0.4f;
    public int ScannerEnergyRate = 1;
    
    // textures
    public Texture2D EnergyBackground; // back segment
    public Texture2D EnergyForeground; // front segment
    public GUIStyle HUDSkin; // Styles up the health integer

    //values   
    private float previousEnergy; //a value for reducing previous current health through attacks
    private float EnergyBarWidth; //a value for creating the health bar size
    private float myFloat; // an empty float value to affect drainage speed

    private GameObject[] path;
    private List<GameObject> EnergyObjects;
    private float time = 0.0f;
    public float interpolationPeriod = 1.0f;

    // Use this for initialization
    void Start () {
        path = GameObject.FindGameObjectsWithTag("Path");
        EnergyObjects = new List<GameObject>();
        CurrentEnergy = 50; //MaxEnergy;
        previousEnergy = MaxEnergy;
        EnergyBarWidth = 100f;
    }

    // Update is called once per frame
    void Update () {
        if(EnergyObjects.Count < MaxItemsSpawned)
        {
            //TODO Prevent spawning on same part of path.
            int rand = Random.Range(0, path.Length-1); //Get a random path index, make sure path has tag Path
            SpawnEnergy(path[rand].transform.position);
        }

        time += Time.deltaTime;

        if (XRay.XRayActive)
        {
            if (time >= interpolationPeriod)
            {
                time = 0.0f;
                DecreaseEnergyScanner();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out hit, Mathf.Infinity, EnergyMask))
        {
            PickupEnergy(hit);
        }
    }

    private void DecreaseEnergyScanner()
    {
        DecreaseEnergy(ScannerEnergyRate);
    }

    private void PickupEnergy(RaycastHit hit)
    {
        EnergyObjects.Remove(hit.transform.gameObject);
        Destroy(hit.transform.gameObject);
        IncreaseEnergy(EnergyPickupWorth);
    }

    private void SpawnEnergy(Vector3 PathLocation)
    {
        Vector3 loc = PathLocation;
        loc.y = loc.y - 0.2f; //Spawn below path
        GameObject g = Instantiate(EnergyObject, new Vector3(PathLocation.x, PathLocation.y - 0.1f, PathLocation.z), Quaternion.identity);
        g.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        g.layer = LayerMask.NameToLayer("Energy");
        g.AddComponent<BoxCollider>();
        EnergyObjects.Add(g);
    }

    public bool DecreaseEnergy(int amount)
    {
        if(CurrentEnergy - amount < 0)
        {
            return false;
        }

        CurrentEnergy -= amount;
        return true;
    }

    public void IncreaseEnergy(int amount)
    {
        CurrentEnergy += amount;
        if (CurrentEnergy > MaxEnergy)
        {
            CurrentEnergy = MaxEnergy;
        }
    }

    void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int height = 15;

        float percentage = EnergyBarWidth * ((float) CurrentEnergy / (float) MaxEnergy);
        GUI.color = new Color(1, 1, 1, EnergyAlpha);
        GUI.DrawTexture(new Rect(posX, posY, (EnergyBarWidth * 2), height), EnergyBackground);
        GUI.DrawTexture(new Rect(posX, posY, (percentage * 2), height), EnergyForeground);

        HUDSkin = new GUIStyle();

        if (CurrentEnergy == MaxEnergy)
        {
            HUDSkin.normal.textColor = Color.green;
            HUDSkin.fontStyle = FontStyle.BoldAndItalic;
            HUDSkin.fontSize = 16;
            GUI.Label(new Rect(30, 28, 100, 50), (int)(CurrentEnergy) + "/" + MaxEnergy.ToString(), HUDSkin);
        }
        else if (CurrentEnergy < MaxEnergy)
        {

            if (percentage <= 50  && percentage >= 25){
                HUDSkin.normal.textColor = Color.yellow;
                HUDSkin.fontStyle = FontStyle.BoldAndItalic;
                HUDSkin.fontSize = 16;
                GUI.Label(new Rect(30, 28, 100, 50), (int)(CurrentEnergy) + "/" + MaxEnergy.ToString(), HUDSkin);

            } else if (percentage < 25)
            {
                HUDSkin.normal.textColor = Color.red;
                HUDSkin.fontStyle = FontStyle.BoldAndItalic;
                HUDSkin.fontSize = 16;
                GUI.Label(new Rect(30, 28, 100, 50), (int)(CurrentEnergy) + "/" + MaxEnergy.ToString(), HUDSkin);

            }
            else
            {
                HUDSkin.normal.textColor = Color.white;
                HUDSkin.fontStyle = FontStyle.BoldAndItalic;
                HUDSkin.fontSize = 16;
                GUI.Label(new Rect(30, 28, 100, 50), (int)(CurrentEnergy) + "/" + MaxEnergy.ToString(), HUDSkin);
            }
        }
    }
}
