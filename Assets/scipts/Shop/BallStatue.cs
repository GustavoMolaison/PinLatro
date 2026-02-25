using UnityEngine;
using UnityEngine.EventSystems;

public class BallStatue : MonoBehaviour, IPointerClickHandler
{
    public Ball EnrolledBall;
    public GameObject whenPickedBloom;
    public static BallStatue Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Instance = this;
    }

    // Update is called once per frame
    public void UpdateBallSprite()
    {
        SpriteRenderer spriterend = GetComponent<SpriteRenderer>();
        spriterend.sprite = EnrolledBall.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        

        whenPickedBloom.SetActive(!whenPickedBloom.activeSelf);
        EnrolledBall.whenPickedBloom.SetActive(!EnrolledBall.whenPickedBloom.activeSelf);
        EnrolledBall.isBlooming = !EnrolledBall.isBlooming;
        PinBallsManager.Instance.oneBallPicked(EnrolledBall);

        
        

    }

    public void ResetStatue()
    {
        SpriteRenderer spriterend = GetComponent<SpriteRenderer>();
        spriterend.sprite = UpgradeHolderUI.Instance.DeflaultSprite;
        EnrolledBall = null;
        whenPickedBloom.SetActive(false);
    }
}
