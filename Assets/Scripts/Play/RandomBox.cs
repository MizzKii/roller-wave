using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBox : MonoBehaviour {

	[SerializeField]private GameObject uiFail;
	[SerializeField]private CemeraMove cameraMove;
	[SerializeField]private int boxCount = 11;
	[SerializeField]private float boxWidth = 1.8f;
	[SerializeField]private List<GameObject> boxs;
	[SerializeField]private GameObject[] prefabs;
	[SerializeField]private GameObject collision;
	[SerializeField]private Text txtScore;
	[SerializeField]private Text txtEndScore;
	private float lastPosX = -7.93f;
	private int lastLevel = 0;
	private int lostBoxCount = 3;
	private List<GameObject> lostBoxs;
	private List<Vector3> lostBoxPos;
	private List<string> lostBoxTag;
	private int resCount = 0;
	private List<GameObject> collisions;
	private int score = 0;

	// Use this for initialization
	void Start () {
		lostBoxs = new List<GameObject> ();
		lostBoxPos = new List<Vector3> ();
		lostBoxTag = new List<string> ();
		collisions = new List<GameObject> ();
		randomNewBox (false);
		randomNewBox (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void chooseBox (GameObject prefab) {
		lostBoxs.Add (GameObject.Instantiate (prefab, lostBoxPos[resCount++], prefab.transform.rotation));
		if (lostBoxs [resCount - 1].transform.tag == lostBoxTag [resCount - 1]) {
			Destroy (collisions [resCount - 1]);
		}
		if (resCount == lostBoxCount) {
			bool isValid = true;
			for (int i = 0; i < lostBoxs.Count; i++) {
				if (lostBoxs [i].transform.tag != lostBoxTag [i]) {
					isValid = false;
					break;
				}
			}
			if (isValid) {
				for (int i = 0; i < lostBoxs.Count; i++) {
					boxs.Add (lostBoxs [i]);
				}
				collisions.Clear ();
				randomNewBox (true);
				cameraMove.nextPos (boxs[boxs.Count - 1].transform.position);
				score += lostBoxCount;
				txtScore.text = score.ToString ();
				txtEndScore.text = score.ToString ();
			} else {
				uiFail.SetActive (true);
				Invoke ("closeUiFail", 0.5f);
				for (int i = 0; i < lostBoxs.Count; i++) {
					Destroy (lostBoxs [i]);
				}
				for (int i = 0; i < collisions.Count; i++) {
					Destroy (collisions [i]);
				}
				collisions.Clear ();
				for (int i = 0; i < lostBoxPos.Count; i++) {
					collisions.Add (GameObject.Instantiate (collision, lostBoxPos[i], collision.transform.rotation));
				}
			}
			lostBoxs.Clear ();
			resCount = 0;
		}
	}

	private void closeUiFail () {
		uiFail.SetActive (false);
	}

	private void randomNewBox (bool isLost) {
		List<GameObject> newBoxs = new List<GameObject> ();
		float[] levelY = Constants.getInstance ().LEVEL_Y;
		for (int i = 0; i < boxCount; i++) {
			lastPosX += boxWidth;
			int max = lastLevel == 3 ? 1 : 2, min = lastLevel == 0 ? 1 : 0;
			int action = Random.Range (min, max + 1);

			float y = levelY [lastLevel];
			if (action == 2) {
				y = levelY [lastLevel + 1];
			} else if (lastLevel == 0 && action == 1) {
				y = -0.97f;
			}
			GameObject prefab = prefabs [action];

			newBoxs.Add (GameObject.Instantiate (
				prefab,
				new Vector3 (lastPosX, y, 0),
				prefab.transform.rotation));
			if (action == 0) {
				lastLevel--;
			} else if (action == 2) {
				lastLevel++;
			}
			boxs.AddRange (newBoxs);
		}
//		List<GameObject> newBoxs = genarateBox ();

		if (isLost) {
			List<int> lostIndexs = new List<int>();
			while (true) {
				int index = Random.Range (0, newBoxs.Count - 2);
				bool isHave = false;
				for (int j = 0; j < lostIndexs.Count; j++) {
					if (lostIndexs[j] == index || lostIndexs[j] == index + 1 || lostIndexs[j] == index - 1 || newBoxs [j].transform.tag == "Untagged") {
						isHave = true;
					}
				}
				if (!isHave) {
					lostIndexs.Add (index);
					if (lostIndexs.Count == lostBoxCount) {
						break;
					}
				}
			}

			for (int j = 0; j < lostIndexs.Count; j++) {
				for (int k = j + 1; k < lostIndexs.Count; k++) {
					if (lostIndexs[k] < lostIndexs[j]) {
						int tmp = lostIndexs [j];
						lostIndexs [j] = lostIndexs [k];
						lostIndexs [k] = tmp;
					}
				}
			}
			//			lostBoxs.Clear ();
			lostBoxPos.Clear ();
			lostBoxTag.Clear ();
			collisions.Clear ();
			for (int j = 0; j < lostIndexs.Count; j++) {
				//					lostBoxs.Add (newBoxs[index]);
				lostBoxPos.Add (newBoxs [lostIndexs[j]].transform.position);
				lostBoxTag.Add (newBoxs [lostIndexs[j]].transform.tag);
				//					newBoxs [index].SetActive (false);
				collisions.Add (GameObject.Instantiate (
					collision,
					newBoxs [lostIndexs[j]].transform.position,
					collision.transform.rotation
				));
				Destroy (newBoxs [lostIndexs[j]]);
			}
		}
	}

	private List<GameObject> genarateBox () {
		List<GameObject> newBoxs = new List<GameObject> ();
		float[] levelY = Constants.getInstance ().LEVEL_Y;

		for (int i = 0; i < boxCount; i++) {
			lastPosX += boxWidth;
			int max = lastLevel == 2 ? 1 : 2, min = lastLevel == 0 ? 1 : 0;
			int action = Random.Range (min, max + 1);
			if (action == 2) {
				int height = Random.Range (1, 3 - lastLevel);
				for (int j = 0; j < height + 1; j++) {
					float y = levelY [lastLevel++];
					GameObject prefab = prefabs [1];
					if (j == 0) {
						if (lastLevel - 1 == 0) {
							y = 0.22f;
						} else if (lastLevel - 1 == 1) {
							y = 2.08f;
						}
						prefab = prefabs [0];
					} else if (j == height) {
						lastLevel--;
						if (lastLevel == 2) {
							y = 3.79f;
						} else if (lastLevel == 1) {
							y = 1.94f;
						}
						prefab = prefabs [2];
					} else {
						y = 2.14f;
					}
					newBoxs.Add (GameObject.Instantiate (
						prefab,
						new Vector3 (lastPosX, y, 0),
						prefab.transform.rotation));
					if (j < height) {
						lastPosX += boxWidth;
						i++;
					}
				}
			} else if (action == 0) {
				int height = Random.Range (1, lastLevel);
				for (int j = 0; j < height + 1; j++) {
					float y = levelY [lastLevel--];
					GameObject prefab = prefabs [5];
					if (j == 0) {
						if (lastLevel + 1 == 2) {
							y = 3.41f;
						} else if (lastLevel + 1 == 1) {
							y = 1.57f;
						}
						prefab = prefabs [4];
					} else if (j == height) {
						lastLevel++;
						if (lastLevel == 1) {
							y = 2.04f;
						} else if (lastLevel == 0) {
							y = 0.24f;
						}
						prefab = prefabs [6];
					}
					newBoxs.Add (GameObject.Instantiate (
						prefab,
						new Vector3 (lastPosX, y, 0),
						prefab.transform.rotation));

					if (j < height) {
						lastPosX += boxWidth;
						i++;
					}
				}
			} else {
				float y = levelY [lastLevel];
				if (lastLevel == 0) {
					y--;
				} else if (lastLevel == 2) {
					y = 3.42f;
				} else {
					y = 1.57f;
				}
				newBoxs.Add (GameObject.Instantiate (
					prefabs [3],
					new Vector3 (lastPosX, y, 0),
					prefabs [3].transform.rotation));
			}
		}
		boxs.AddRange (newBoxs);
		return newBoxs;
	}
}