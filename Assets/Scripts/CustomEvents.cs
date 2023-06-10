using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomBoolEvent: UnityEvent<bool> {}

[System.Serializable]
public class CustomIntEvent: UnityEvent<int> {}

[System.Serializable]
public class CustomListEvent: UnityEvent<List<Block>> {}

[System.Serializable]
public class CustomVector2Event: UnityEvent<int, int> {}
