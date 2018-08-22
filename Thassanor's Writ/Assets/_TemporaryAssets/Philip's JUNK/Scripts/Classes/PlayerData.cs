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
    public NecromancerStyle playerCharacter;
    public Spell[] playerSpells = new Spell[2];
    public bool isHost;
}
