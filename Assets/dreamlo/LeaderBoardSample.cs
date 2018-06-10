using UnityEngine;
using System.Collections.Generic;

public class LeaderBoardSample : MonoBehaviour {
	float startTime = 10.0f;
	float timeLeft = 0.0f;
	
	int totalScore = 0;
	string playerName = "";
	string code = "";

	public enum gameState {
		waiting,
		running,
		enterscore,
		leaderboard
	};
	
	public static gameState gs;
	
	
	// Reference to the dreamloLeaderboard prefab in the scene
	
	dreamloLeaderBoard dl;
	dreamloPromoCode pc;

	void Start () 
	{
		// get the reference here...
		this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

		// get the other reference here
		this.pc = dreamloPromoCode.GetSceneDreamloPromoCode();

		this.timeLeft = startTime;
		gs = gameState.waiting;
	}

	void Update () 
	{
		if (gs == gameState.running)
		{
			gs = gameState.enterscore;
		}
		this.totalScore = (int)Runner.score;

	}

	void OnGUI()
	{
		GUILayoutOption[] width200 = new GUILayoutOption[] {GUILayout.Width(200)};
		
		float width = 400;  // Make this wider to add more columns
		float height = 200;

		Rect r = new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height), width, height);
		GUILayout.BeginArea(r, new GUIStyle("box"));
		
		GUILayout.BeginVertical();
		if (gs == gameState.waiting || gs == gameState.running)
		{	
			GUILayout.Label("Total Score: " + this.totalScore.ToString());
		}
		
		
		
		if (gs == gameState.enterscore)
		{
			GUILayout.Label("Total Score: " + this.totalScore.ToString());
			GUILayout.BeginHorizontal();
			GUILayout.Label("Your Name: ");
			this.playerName = GUILayout.TextField(this.playerName, width200);
			
			if (GUILayout.Button("Save Score"))
			{
				// add the score...
				if (dl.publicCode == "") Debug.LogError("You forgot to set the publicCode variable");
				if (dl.privateCode == "") Debug.LogError("You forgot to set the privateCode variable");

				dl.AddScore(this.playerName, totalScore);
				
				gs = gameState.leaderboard;
			}
			GUILayout.EndHorizontal();
		}
		
		if (true)
		{
			GUILayout.Label("High Scores:");
			List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();
			
			if (scoreList == null) 
			{
				GUILayout.Label("(loading...)");
			} 
			else 
			{
				int maxToDisplay = 20;
				int count = 0;
				foreach (dreamloLeaderBoard.Score currentScore in scoreList)
				{
					count++;
					GUILayout.BeginHorizontal();
					GUILayout.Label(currentScore.playerName, width200);
					GUILayout.Label(currentScore.score.ToString(), width200);
					GUILayout.EndHorizontal();

					if (count >= maxToDisplay) break;
				}
			}
		}
		GUILayout.EndArea();

		r.y = r.y + r.height + 20;
		GUILayout.BeginArea(r, new GUIStyle("box"));
		GUILayout.BeginHorizontal();

		GUILayout.EndHorizontal();



		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
	
	
}
