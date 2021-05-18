using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public interface IEnemyCreatedInvoker
{
    void AddEnemyCreatedListener(UnityAction<GameObject> listener);
}
