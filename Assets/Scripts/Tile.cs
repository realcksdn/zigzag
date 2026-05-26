using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private float falldownTime = 2;
    private Rigidbody rb;
    private TileSpawner tileSpawner = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Setup(TileSpawner tileSpawner)
    {
        this.tileSpawner = tileSpawner;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            StartCoroutine(nameof(FallDownAndRespawnTile));
        }
    }

    private IEnumerator FallDownAndRespawnTile()
    {
        yield return new WaitForSeconds(0.1f); 

        rb.isKinematic = false;  //물리 작동

        yield return new WaitForSeconds(falldownTime); 

        rb.isKinematic = true; //믈리 작동 X

        if (tileSpawner != null) //4-2
        {
            tileSpawner.SpawnTile(this.transform);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
