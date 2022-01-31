using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAction : MonoBehaviour
{
    public void StartDialog()
    {
        gameObject.SetActive(true);
        StartCoroutine(ScaleDialog());
    }

    IEnumerator ScaleDialog()
    {
        Vector3 toScale = transform.localScale * 1.2f;
        gameObject.transform.LeanScale(toScale, 0.4f);
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
        gameObject.transform.localScale = Vector3.one;
        yield return null;
    }
}
