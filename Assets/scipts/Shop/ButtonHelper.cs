using TMPro;
using UnityEngine;
using System.Collections;

public class ButtonHelper : MonoBehaviour
{
    public TMP_Text costTmp;
    public TMP_Text nameTmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform nameTransform = this.transform.Find("UpgradeName");
        nameTmp = nameTransform.GetComponent<TMP_Text>();

        Transform costTransform = this.transform.Find("UpgradeName/Cost (TMP)");
        costTmp = costTransform.GetComponent<TMP_Text>();

        
    }

    // Update is called once per frame
   public void tooPoor()
    {
        Debug.Log("POOOR");
        StopAllCoroutines();
        StartCoroutine(FlashRedRoutine());
    }

    private IEnumerator FlashRedRoutine()
    {
        Debug.Log("reeedDdD");
        costTmp.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        costTmp.color = Color.white;
    }
}
