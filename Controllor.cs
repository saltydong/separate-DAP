using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Controllor : MonoBehaviour, Handle, UserAction {
	public Director MySD;
	public int boatState = 0;
	

	public int gameResult = 0;


	public GameObject[] onBoat=new GameObject[2];
	public GameObject boat;

	public float gap = 1.5f;

	public Stack<GameObject> SPriest = new Stack<GameObject>();
	public Stack<GameObject> EPriest = new Stack<GameObject>();
	public Stack<GameObject> SDevil = new Stack<GameObject>();
	public Stack<GameObject> EDevil = new Stack<GameObject>();

	public Vector3 boatStart = new Vector3 (-2f, 0.5f, 0);
	public Vector3 boatEnd = new Vector3 (2f, 0.5f, 0);

	public float speed=10;

	public void Load(){
		GameObject myGO = Instantiate<GameObject> (Resources.Load<GameObject> ("prefabs/major"), Vector3.zero, Quaternion.identity);
		myGO.name = "major";
		boat = Instantiate (Resources.Load ("prefabs/boat"), boatStart, Quaternion.identity)as GameObject;
		for (int i = 0; i < 3; i++) {
			SPriest.Push (Instantiate (Resources.Load ("prefabs/priest")) as GameObject);
			SDevil.Push (Instantiate (Resources.Load ("prefabs/devil"))as GameObject);
		}
	}		

	void Awakemethod(){
		Director x = Director.getInstance ();
		x.currentSceneController = this;
		x.currentSceneController.Load ();
	}

	void Awake(){
		Awakemethod ();
	}

	int boatCapacity(){
		int cnt = 0;
		for(int i=0;i<2;i++){
			if (onBoat [i] == null)
				cnt++;
		}
		return cnt;
	}

	void setCharacterPositions(Stack<GameObject> que, Vector3 pos) {
		GameObject[] array = que.ToArray();
		for (int i = 0; i < que.Count; ++i) {  
			array[i].transform.position = new Vector3(pos.x + gap*i, pos.y, pos.z);  
		}  
	}

	public void getOn(GameObject obj){
		if (boatCapacity () != 0) {
			obj.transform.parent = boat.transform;
			if (onBoat [0] == null) {
				onBoat [0] = obj;
				obj.transform.localPosition = new Vector3 (-0.2f, 1f, 0);
			} else {
				onBoat [1] = obj;
				obj.transform.localPosition = new Vector3 (0.2f, 1f, 0);
			}
		}
	}

	public void priestGetOn(){
		if (SPriest.Count != 0 && boatCapacity () != 0 && this.boatState == 0) {
			getOn (SPriest.Pop ());
		} else if (EPriest.Count != 0 && boatCapacity () != 0 && this.boatState == 1) {
			getOn (EPriest.Pop ());
		}
	}

	public void devilGetOn(){
		if (SDevil.Count != 0 && boatCapacity () != 0 && this.boatState == 0) {
			getOn (SDevil.Pop ());
		} else if (EDevil.Count != 0 && boatCapacity () != 0 && this.boatState == 1) {
			getOn (EDevil.Pop ());
		}
	}

	public void priestGetOff(){
		for(int i=0;i<2;i++){
			if(onBoat[i]!=null){
				if(this.boatState==1){
					if(onBoat[i].name=="priest(Clone)"){
						EPriest.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat [i] = null;
						break;
					}
				}
				else if(this.boatState==0){
					if(onBoat[i].name=="priest(Clone)"){
						SPriest.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat[i]=null;
						break;
					}
				}
			}
		}
	}

	public void devilGetOff(){
		for(int i=0;i<2;i++){
			if(onBoat[i]!=null){
				if(this.boatState==1){
					if(onBoat[i].name=="devil(Clone)"){
						EDevil.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat [i] = null;
						break;
					}
				}
				else if(this.boatState==0){
					if(onBoat[i].name=="devil(Clone)"){
						SDevil.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat[i]=null;
						break;
					}
				}
			}
		}
	}	

	public void moveBoat(){
		if(boatCapacity()==2)return ;
		
			boatState = 3;*/
		this.boatState += 2;
		
	}

	public void restart(){
	
		EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
		boatState = gameResult = 0;
	}

	public int getResult(){
		return gameResult;
	}

	public int result(){
		if (EPriest.Count == 3 && EDevil.Count == 3) {
			gameResult = 1;
		}
		int cntPriest = 0, cntDevil = 0;
		for (int i = 0; i < 2; i++) {
			if (onBoat [i] != null) {
				if (onBoat [i].name == "priest(Clone)")
					cntPriest++;
				else
					cntDevil++;
			}
		}
		int cntPriestStart = 0, cntPriestEnd = 0;
		int cntDevilStart = 0, cntDevilEnd = 0;
		if (boatState == 0) {
			cntPriestStart = SPriest.Count + cntPriest;
			cntDevilStart = SDevil.Count + cntDevil;
			cntPriestEnd = EPriest.Count;
			cntDevilEnd = EDevil.Count;
		} else if (boatState == 1) {
			cntPriestStart = SPriest.Count;
			cntDevilStart = SDevil.Count;
			cntPriestEnd = EPriest.Count + cntPriest;
			cntDevilEnd = EDevil.Count + cntDevil;
		}
		
		if ((cntPriestStart != 0 && cntPriestStart < cntDevilStart) || (cntPriestEnd != 0 && cntPriestEnd < cntDevilEnd)) {
			gameResult = 2;
		}
		return gameResult;
	}


	void Start () {
		
	}

		

	void Update () {
		setCharacterPositions (SPriest, new Vector3 (-13f, 3, 0));
		setCharacterPositions (EPriest, new Vector3 (9f, 3, 0));
		setCharacterPositions (SDevil, new Vector3 (-8f, 3, 0));
		setCharacterPositions (EDevil, new Vector3 (4f, 3, 0));

	}
}
