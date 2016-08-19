# Co
Coroutines in Anywhere, for Unity 5

## How to Use
- Case 1: Coroutines in plain class
```csharp
public class PlainClass {

  public void PlainFunction () {
    Co.Run(Coroutine_In_PlainClass());
  }
  
  private IEnumerator Coroutine_In_PlainClass () {
    Debug.Log("Yey!");
    yield return new WaitForSeconds(1f);
    Debug.Log("Do Something!");
  }
  
}
```

- Case 2: Running functions with delay in one line
```csharp
private void SomeFunction () {
  Co.Delay(2f, () => Debug.Log("Executed after 2 seconds!"));
}
```

## Downloads
[Co-v0.0.1.unitypackage](https://github.com/appetizermonster/Unity3D-Co/raw/master/Co-v0.0.1.unitypackage)

## License
MIT