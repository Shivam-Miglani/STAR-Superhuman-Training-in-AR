using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public const int MaxHealth = 100;
    public static int CurrentHealth;
    public float HealthAlpha = 0.5f;

    // textures
    public Texture2D HealthBackground; // back segment
    public Texture2D HealthForeground; // front segment
    public GUIStyle HUDSkin; // Styles up the health integer
	public RectTransform healthBar;

	//values   
	private float HealthBarWidth; //a value for creating the health bar size

    private float time = 0.0f;
    public float interpolationPeriod = 1.0f;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth; //MaxEnergy;
        HealthBarWidth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //DecreaseHealth(1);
		healthBar.sizeDelta = new Vector2(CurrentHealth, healthBar.sizeDelta.y);
	}

     public static bool DecreaseHealth(int amount)
    {
        if (CurrentHealth - amount < 0)
        {
            return false;
        }

        CurrentHealth -= amount;
        return true;
    }



    void OnGUI()
    {
        int posX = 10;
        int posY = 27;
        int height = 15;

        float percentage = HealthBarWidth * ((float)CurrentHealth / (float)MaxHealth);
        GUI.color = new Color(1, 1, 1, HealthAlpha);
        GUI.DrawTexture(new Rect(posX, posY, (HealthBarWidth * 2), height), HealthBackground);
        GUI.DrawTexture(new Rect(posX, posY, (percentage * 2), height), HealthForeground);

    }
}
