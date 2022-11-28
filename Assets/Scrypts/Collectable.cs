using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    protected int value;
    protected float rotationSpeed = 0.001f;

    protected abstract void RestoreValue();

    protected void AnimateObject()
    {
        for (int i = 0; i <= 360; i++)
        {
            transform.Rotate(new Vector3(0f, i * Time.deltaTime * rotationSpeed, 0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Player>() != null)
        {
            RestoreValue();
            Debug.Log($"Pick-up has collided with player: ammo available: {Player.Instance.ammunition}");
            gameObject.SetActive(false);
        }
    }
}
