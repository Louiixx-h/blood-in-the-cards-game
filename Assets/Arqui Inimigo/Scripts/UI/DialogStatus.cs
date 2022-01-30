using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogStatus : MonoBehaviour
{
    public void StartDialog()
    {
        Vector3 toScale = Vector3.one;
        
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x, 
            -563, 
            gameObject.transform.position.z
        );

        gameObject.transform.LeanScale(toScale, 0.4f);
        gameObject.transform.LeanMoveLocalY(0, 0.4f); 
    }
}
