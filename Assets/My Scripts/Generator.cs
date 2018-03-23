using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
	public float panjangTile;
	public float panjangBackground;
	public float x,y,z;
	public float questionTimer;
	public float offset;

	public GameObject floor;
	public GameObject hole;
	public GameObject rubble;
	public GameObject arrow1;
	public GameObject arrow2;
	public GameObject arrow3;
	public GameObject gate;
	public GameObject BG1;
	public GameObject BG2;
	public GameObject BG3;
	public GameObject Question;

	public int holePercentage;
	public int rubblePercentage;
	public int arrow1Percentage;
	public int arrow2Percentage;
	public int arrow3Percentage;
	public int gatePercentage;

	private bool isQuestion = false;
	private bool isSpace = false;
	private int temp, index, index2;
	private int tempBefore;

	// Use this for initialization
	void Start () {
		index = 0;
		rubblePercentage += holePercentage;
		arrow1Percentage += rubblePercentage;
		arrow2Percentage += arrow1Percentage;
		arrow3Percentage += arrow2Percentage;
		gatePercentage += arrow3Percentage;
		MakeStartTile ();
		MakeStartBG ();
		MakeTile ();
		MakeBG ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(isQuestion);
	}

	public void MakeStartTile(){
		float i = 20 / panjangTile;
		for (int j = 0; j < i; j++) {
			Instantiate(floor, new Vector3(x,y,z) ,Quaternion.identity);
			x += panjangTile;
		}
	}
	public void MakeStartBG(){
		Instantiate(BG1, new Vector3(-4,0.5f,3) ,Quaternion.identity);
		Instantiate(BG1, new Vector3(4.98f,0.5f,3) ,Quaternion.identity);
	}

	public void FirstQuestion(){
		StartCoroutine (delay3());
	}
	/*public void MakeQuestion(){
		Instantiate(Question, new Vector3(16,-1.3f,-3) ,Quaternion.identity);
		index2 = GameObject.Find ("DatabaseManager").GetComponent<DatabaseManager2> ().id2 [index];
		Debug.Log (index2);
		GameObject.Find ("QuestionBox").GetComponent<TextMesh> ().text = GameObject.Find ("DatabaseManager").GetComponent<DatabaseManager2> ().soal [index2];
		GameObject.Find ("AnswerBox1").GetComponent<TextMesh> ().text = GameObject.Find ("DatabaseManager").GetComponent<DatabaseManager2> ().jawaban1 [index2];
		GameObject.Find ("AnswerBox2").GetComponent<TextMesh> ().text = GameObject.Find ("DatabaseManager").GetComponent<DatabaseManager2> ().jawaban2 [index2];

		if (GameObject.Find ("DatabaseManager").GetComponent<DatabaseManager2> ().kunci [index2] == "01")
			GameObject.Find ("Door2").GetComponent<BoxCollider2D>().enabled = false;
		else
			GameObject.Find ("Door1").GetComponent<BoxCollider2D>().enabled = false;
		
		index++;
		if (index == GameObject.Find ("DatabaseManager").GetComponent<DatabaseManager2> ().n) {
			index = 0;
			GameObject.Find ("DatabaseManager").GetComponent<DatabaseManager2> ().randomize ();
		}
		StartCoroutine (delay3());
	}*/

	public void MakeTile(){
		
		temp = randomizeTile ();
		if(isQuestion)
			Instantiate (floor, new Vector3(x,y,z), Quaternion.identity);
		else if (isSpace) {
			Instantiate (floor, new Vector3 (x, y, z), Quaternion.identity);
			isSpace = false;
			temp = 0;
		}
		else if (tempBefore != 0) {
			Instantiate (floor, new Vector3 (x, y, z), Quaternion.identity);
			isSpace = true;
		}
		else if (temp == 0)
			Instantiate (floor, new Vector3(x,y,z), Quaternion.identity);
		else if(temp == 1)
			Instantiate (hole, new Vector3(x,y,z), Quaternion.identity);
		else if(temp == 2)
			Instantiate (gate, new Vector3(x,y,z), Quaternion.identity);
		else if(temp == 3)
			Instantiate (rubble, new Vector3(x,y,z), Quaternion.identity);
		else if(temp == 4)
			Instantiate (arrow1, new Vector3(x,y,z), Quaternion.identity);
		else if(temp == 5)
			Instantiate (arrow2, new Vector3(x,y,z), Quaternion.identity);
		else if(temp == 6)
			Instantiate (arrow3, new Vector3(x,y,z), Quaternion.identity);
		

		tempBefore = temp;

		StartCoroutine (delay());
	}

	public void MakeBG(){
		int temp = Random.Range (0, 3);
		if(temp == 0)
			Instantiate(BG1, new Vector3(13.96f,0.5f,3) ,Quaternion.identity);
		else if(temp == 1)
			Instantiate(BG2, new Vector3(13.96f,0.5f,3) ,Quaternion.identity);
		else
			Instantiate(BG3, new Vector3(13.96f,0.5f,3) ,Quaternion.identity);
		StartCoroutine (delay2());
	}


	IEnumerator delay(){
		yield return new WaitForSeconds (panjangTile/1.9f);
		MakeTile();
	}

	IEnumerator delay2(){	
		yield return new WaitForSeconds (panjangBackground/1.9f);
		MakeBG();
	}
	IEnumerator delay3(){
		isQuestion = true;
		yield return new WaitForSeconds (offset);
		isQuestion = false;
		yield return new WaitForSeconds (questionTimer-offset);
		//MakeQuestion ();
	}

	public int randomizeTile(){
		List <int> randList= new List <int>();
		for (int j = 0; j < 100; j++) {
			if (j < holePercentage) randList.Add (1);
			else if (j < rubblePercentage) randList.Add (3);
			else if (j < arrow1Percentage) randList.Add (4);
			else if (j < arrow2Percentage) randList.Add (5);
			else if (j < arrow3Percentage) randList.Add (6);
			else if (j < gatePercentage) randList.Add (2);
			else randList.Add (0);
		}
		return(randList [Random.Range (0, 100)]);
	}
}
