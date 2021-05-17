using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public interface IBulletCreatedInvoker
{
    void AddBulletCreatedListener(UnityAction<GameObject> listener);
}
