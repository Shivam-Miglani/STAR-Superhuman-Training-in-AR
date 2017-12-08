using UnityEngine;
using System.Collections;

public class BeamWave : MonoBehaviour {

    public Color col = new Color(0.5f,0.5f,0.5f,0.5f);
    public float RotateSpd = 3.0f;
    public float AnmSpd = 2.0f;
    public float AnmTime = 0;
    public float MaxScale = 1.0f;
    private float DefOutSize;
	// Use this for initialization
	void Start () {
        BM_WaveEffect wav = this.GetComponent<BM_WaveEffect>();
        DefOutSize = wav.OutSize;
        wav.InSize = 0;

        this.transform.Rotate(Vector3.up, Random.Range(0,360.0f));
        this.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
    }

    // Update is called once per frame
    void Update ()
    {
        AnmTime += Time.deltaTime * AnmSpd;
        this.transform.Rotate(Vector3.up, RotateSpd);
        BM_WaveEffect wav = this.GetComponent<BM_WaveEffect>();
        wav.InSize = Mathf.Lerp(0, 1, 1-Mathf.Pow(1-AnmTime,8));
        wav.OutSize = Mathf.Lerp(0, MaxScale+ DefOutSize, 1-Mathf.Pow(1-AnmTime, 9));
        this.GetComponent<Renderer>().material.SetColor("_Color", col);

        if (AnmTime > 1.0f) Destroy(this.gameObject);
    }
}
