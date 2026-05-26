using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject itemGetEffectPrefab;
    private GameController gameController;
    private float rotateSpeed;
    public FeverManager feverManager;
    public float gaySex = 1f;
    public void Setup(GameController gameController)
    {
        
        this.gameController = gameController;

        itemGetEffectPrefab = Instantiate(itemGetEffectPrefab, transform.position, Quaternion.identity);
        itemGetEffectPrefab.SetActive(false);


    }

    public void Setup1(FeverManager feverManager)
    {

        this.feverManager = feverManager;

    }
    private void OnEnable()
    {
        rotateSpeed = Random.Range(10, 100);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(1, 1, 0) * rotateSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            itemGetEffectPrefab.transform.position = transform.position;
            itemGetEffectPrefab.SetActive(true);
            feverManager.AddGauge(gaySex);
            gameController.IncreaseScore(5);
            gameObject.SetActive(false);
        }
     
    }
}
