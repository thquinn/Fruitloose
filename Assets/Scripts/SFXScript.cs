using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public static SFXScript instance;
    public AudioSource[] moves;
    public AudioSource chomp, buttonDown, buttonUp, slide;
    int moveIndex;

    void Start()
    {
        instance = this;
        foreach (AudioSource move in moves) {
            move.pitch *= 1.25f;
        }
        moves.Shuffle();
        moveIndex = 0;
    }

    public void Move() {
        AudioSource move = moves[moveIndex++];
        move.PlayOneShot(move.clip);
        if (moveIndex == moves.Length) {
            do {
                moves.Shuffle();
            } while (moves[0] == move);
            moveIndex = 0;
        }
    }
    public void Chomp() {
        chomp.PlayOneShot(chomp.clip);
    }
    public void ButtonDown() {
        buttonDown.PlayOneShot(buttonDown.clip);
    }
    public void ButtonUp() {
        buttonUp.PlayOneShot(buttonUp.clip);
    }
    public void Slide() {
        slide.PlayOneShot(slide.clip);
    }
}
