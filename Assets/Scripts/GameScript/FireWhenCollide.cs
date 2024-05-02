using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWhenCollide : MonoBehaviour
{
    public GameObject PrefabFire;
        
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.IsAttaking && (other.transform.tag == "Player" || other.transform.tag == "Weapon"))
        {
            GameObject fire = Instantiate(PrefabFire);
            fire.GetComponent<Transform>().position = GetComponent<Transform>().position;
            Destroy(this.gameObject);
            Destroy(fire.gameObject, 7);
        }
    }
}
