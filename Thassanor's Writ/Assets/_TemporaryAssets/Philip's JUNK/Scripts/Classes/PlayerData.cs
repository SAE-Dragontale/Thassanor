/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			PlayerData.cs
   Version:			0.0.1
   Description:     stores all fields required to pass through to the main scene.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using UnityEngine;
public class PlayerData : MonoBehaviour
    {
        public KeyboardHotkeys playerHotkeys;
        public NecromancerStyle playerCharacter = new NecromancerStyle();

        public int typingDifficulty;

        public int _itSeed;
        public int _columns;
        public int _rows;
        public int _waterSize;
        public int _townSpread;
        public int _maxTownCount;

    private void Start()
    {
        playerHotkeys = ScriptableObject.CreateInstance<KeyboardHotkeys>();
    }
}
