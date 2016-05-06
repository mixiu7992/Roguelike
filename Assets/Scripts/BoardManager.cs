using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
	[SerializeField]
	public class Count
	{
		public int minimum;
		public int maxinum;

		public Count(int min,int max)
		{
			minimum = min;
			maxinum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;
	public Count wallCount = new Count(5,9);
	public Count foodCount = new Count(1,5);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitialiseList()
	{
		gridPositions.Clear ();
		for(int x = 1; x < columns - 1; x++)
		{
			for(int y = 1; y< rows - 1; y++)
			{
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	void boardSetup()
	{
		boardHolder = new GameObject ("board").transform;

		for(int x = -1; x < columns + 1; x++)
		{
			for(int y = -1; y < rows + 1; y++)
			{
				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				if(x == -1 || x == columns || y == -1 || y == rows)
				{
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
				}
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
			}
		}
	}

	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPostion = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPostion;
	}

	void LayoutObjectAtRandom(GameObject[] titleArray,int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);
		for(int i = 0; i < objectCount; i++)
		{
			Vector3 randomPositon = RandomPosition ();
			GameObject titleChoice = titleArray [Random.Range (0, titleArray.Length)];
			Instantiate (titleChoice, randomPositon, Quaternion.identity);
		}
	}

	public void SetupScene(int level)
	{
		boardSetup ();
		InitialiseList ();
		LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maxinum);
		LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maxinum);
		int enemyCount = (int)Mathf.Log(level,2f);
		LayoutObjectAtRandom(enemyTiles,enemyCount,enemyCount);
		Instantiate(exit,new Vector3(columns - 1,rows - 1,0f),Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
