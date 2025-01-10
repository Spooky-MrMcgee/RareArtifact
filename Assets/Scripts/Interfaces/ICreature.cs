using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature
{
    void Walking();

    void Running();

    void Idle();

    void StartTask();

    void DoTask();

    void StopTask();
}
