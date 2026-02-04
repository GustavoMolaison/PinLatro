using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


public abstract class Upgrade : MonoBehaviour
{
  public abstract void apply(Ball ballRef);

  public abstract string UpgradeName { get; }

}

