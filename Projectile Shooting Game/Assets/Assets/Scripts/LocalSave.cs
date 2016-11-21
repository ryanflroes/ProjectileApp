using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class LocalSave
{

	private static bool IsTopOne ()
	{
		return  (PlayerPrefs.GetInt ("int1") != 0);
	}

	private static bool IsTopTwo ()
	{
		return  (PlayerPrefs.GetInt ("int2") != 0);
	}

	private static bool IsTopThree ()
	{
		return  (PlayerPrefs.GetInt ("int3") != 0);
	}

	private static PlayerInfo TopOne {
		get {
			PlayerInfo player = new PlayerInfo ();
			player.name = PlayerPrefs.GetString ("string1");
			player.score = PlayerPrefs.GetInt ("int1");
			return player;
		}
	}

	private static PlayerInfo TopTwo {
		get {
			PlayerInfo player = new PlayerInfo ();
			player.name = PlayerPrefs.GetString ("string2");
			player.score = PlayerPrefs.GetInt ("int2");
			return player;
		}
	}

	private static PlayerInfo TopThree {
		get {
			PlayerInfo player = new PlayerInfo ();
			player.name = PlayerPrefs.GetString ("string3");
			player.score = PlayerPrefs.GetInt ("int3");
			return player;
		}
	}

	// Saving last score and load all the top 3 scores
	public static void SaveScore (string name, int score)
	{
		Debug.LogWarning ("SAVING");
		List<PlayerInfo> playerInfos = new List<PlayerInfo> ();
	
		PlayerInfo _playerInfo = new PlayerInfo ();
		_playerInfo.name = name;
		_playerInfo.score = score;
		playerInfos.Add (_playerInfo);

		if (IsTopOne ()) {
			playerInfos.Add (TopOne);
		}
		if (IsTopTwo ()) {
			playerInfos.Add (TopTwo);
		}
		if (IsTopThree ()) {
			playerInfos.Add (TopThree);
		}
			
		List<PlayerInfo> sortedPlayerInfos = Sorting (playerInfos);

		for (int i = 0; i < sortedPlayerInfos.Count; i++) {
			PlayerPrefs.SetInt ("int" + (i + 1).ToString (), sortedPlayerInfos [sortedPlayerInfos.Count - i - 1].score);
			PlayerPrefs.SetString ("string" + (i + 1).ToString (), sortedPlayerInfos [sortedPlayerInfos.Count - i - 1].name);
		}
	}

	// Load all the top scores
	public static List<PlayerInfo> LoadTop ()
	{
		List<PlayerInfo> oldPlayerInfos = new List<PlayerInfo> ();

		if (IsTopOne ()) {
			oldPlayerInfos.Add (TopOne);
		}
		if (IsTopTwo ()) {
			oldPlayerInfos.Add (TopTwo);
		}
		if (IsTopThree ()) {
			oldPlayerInfos.Add (TopThree);
		}

		return oldPlayerInfos;
	}

	// Sort score
	private static List<PlayerInfo> Sorting (List<PlayerInfo> _infos)
	{
		if (_infos.Count <= 1)
			return _infos;

		_infos = _infos.OrderBy (x => x.score).ToList ();
		return _infos;

	}

	public static void DeleteAllValues ()
	{
		PlayerPrefs.DeleteAll ();
	}

	public class PlayerInfo
	{
		public string name;
		public int score;
	}
}
