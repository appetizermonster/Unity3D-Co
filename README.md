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
  Co.Delay(() => Debug.Log("Executed after 2 seconds!"), 2f);
}
```

## Install
Please use [unity-packman](https://github.com/appetizermonster/unity-packman)

## License
MIT