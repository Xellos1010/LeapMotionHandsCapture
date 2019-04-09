﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Attributes;

namespace Leap.Unity.Recording {

  [RecordingFriendly]
  public class PropertyCompression : MonoBehaviour {

    public NamedCompression[] compressionOverrides;

    [Serializable]
    public class NamedCompression {
      public string propertyName;

      [MinValue(0)]
      public float maxError;
    }
  }

}
