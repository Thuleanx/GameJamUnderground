using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
	public static HealthDisplay Instance;
	public static HealthDisplay EnemyInstance;

	[SerializeField] Status playerStatus;

	[SerializeField] float offsetAmount;
	[SerializeField] Vector2 heartSize;
	[SerializeField] Sprite heartFull;
	[SerializeField] Sprite heartEmpty;
	[SerializeField] bool mobStatus;

	List<HeartImage> hearts = new List<HeartImage>();

	void Awake() {
		Instance = this;
		if (mobStatus) EnemyInstance = this;
	}

	public void Attach(Status status) {
		this.playerStatus = status;
	}

	Image CreateHeartImage(Vector2 anchoredPosition) {
		GameObject heartGameObject = new GameObject("Heart", typeof(Image));

		heartGameObject.transform.SetParent(transform);
		heartGameObject.transform.localPosition = Vector3.zero;

		heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
		heartGameObject.GetComponent<RectTransform>().sizeDelta = heartSize;

		Image heartImageUI = heartGameObject.GetComponent<Image>();

		HeartImage heart = new HeartImage(heartImageUI);
		heart.SetHeart(true);
		hearts.Add(heart);
		heart.full = heartFull;
		heart.empty = heartEmpty;

		return heartImageUI;
	}

	void Update()
	{
		if ((playerStatus == null || !playerStatus.gameObject.activeSelf) && mobStatus) {
			foreach (GameObject mob in GameObject.FindGameObjectsWithTag("Enemy"))
				if (mob.activeSelf)
					playerStatus = mob.GetComponent<Status>();	
			if (playerStatus != null && !playerStatus.gameObject.activeSelf)
				playerStatus = null;
		}

		if (playerStatus != null)
		{
			while (hearts.Count < playerStatus.MaxHealth) CreateHeartImage(Vector2.right * (offsetAmount + heartSize.x) * hearts.Count);

			for (int i = 0; i < hearts.Count; i++)
			{
				if (i < playerStatus.MaxHealth)
				{
					hearts[i].SetActive(true);
					hearts[i].SetHeart(i < playerStatus.CurrentHealth);
				}
				else
				{
					hearts[i].SetActive(false);
				}
			}
		}
		else
		{
			foreach (HeartImage heart in hearts)
				heart.SetActive(false);
		}

	}

	public class HeartImage {
		Image heartImage;
		public Sprite full, empty;

		public HeartImage(Image heartImage) {
			this.heartImage = heartImage;
		}

		public void SetActive(bool active) {
			heartImage.gameObject.SetActive(active);	
		}

		public void SetHeart(bool isFill) {
			if (isFill) heartImage.sprite = full;
			else 		heartImage.sprite = empty;
		}
	}
}
