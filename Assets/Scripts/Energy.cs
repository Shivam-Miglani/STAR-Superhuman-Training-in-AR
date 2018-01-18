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
    public int EnergyPickupWorth = 25;
    public float EnergyAlpha = 0.4f;
    public int ScannerEnergyRate = 1;
    public int RegenRate = 1;
    public bool EnergyRegeneration = true;
	public static bool MapPlaced = false;
	public RectTransform healthBar;

	// textures
	public Texture2D EnergyBackground; // back segment
    public Texture2D EnergyForeground; // front segment
    public GUIStyle HUDSkin; // Styles up the health integer

    //values   
    private float EnergyBarWidth; //a value for creating the health bar size
    private float myFloat; // an empty float value to affect drainage speed

    private GameObject[] path;
    private List<GameObject> EnergyObjects;
    private float time = 0.0f;
    private float timeRegen = 0.0f;
    public float interpolationPeriod = 1.0f;
    public float RegenInterpolationPeriod = 2.0f;

    // Use this for initialization
    void Start () {
        path = GameObject.FindGameObjectsWithTag("Path");
        EnergyObjects = new List<GameObject>();
        CurrentEnergy = 50; //MaxEnergy;
        EnergyBarWidth = 100f;
    }

    // Update is called once per frame
    void Update () {
		if (EnergyObjects.Count < MaxItemsSpawned && MapPlaced)
        {
            //TODO Prevent spawning on same part of path.
            int rand = Random.Range(0, path.Length-1); //Get a random path index, make sure path has tag Path
            SpawnEnergy(path[rand].transform.position);
        }

        time += Time.deltaTime;
        timeRegen += Time.deltaTime;

        if (XRay.XRayActive)
        {
            if (time >= interpolationPeriod)
            {
                time = 0.0f;
                DecreaseEnergyScanner();
            }

			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out hit, Mathf.Infinity, EnergyMask))
			{
				PickupEnergy(hit);
			}
		}
        else if(EnergyRegeneration)
        {
            if (timeRegen >= RegenInterpolationPeriod)
            {
                timeRegen = 0.0f;
                IncreaseEnergy(RegenRate);
            }
			
		}
		healthBar.sizeDelta = new Vector2(CurrentEnergy, healthBar.sizeDelta.y);
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
		g.GetComponent<BoxCollider>().size = new Vector3(0.6f, 0.6f, 0.6f);
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

}
