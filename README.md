## About
Unity really-lightweight dependency injection framework.
## Installation
Add this line to your **Packages/manifest.json** file in your Unity project:
```
"com.unity.dependeasy": "https://github.com/pawelsalwa/DependEasy.git?path=Assets/DependEasy"
```
## Example
Example usage:
```csharp
using DependEasy;

public class SomeBehaviour : MonoBehaviour
{
    [Inject] private SomeDependency dependency;

    private void Awake() {
        // Throws NullReferenceException - Awake() is used for internal state
        Debug.Log(dependency.ToString());
    }

    private void Start() {
        // works just fine - at Start(), all dependencies are already injected
        Debug.Log(dependency.ToString());
    }
}
```

For this example to work you just need to have SomeDependency in your scene marked with [Service] attribute:
```csharp
using DependEasy;

[Service]
public class SomeDependency : MonoBehaviour
{
    
}
```
You can also use injection with interfaces:
```csharp
using DependEasy;

[Service(typeof(ISomeInterface)]
public class SomeDependency : MonoBehaviour, ISomeInterface
{
    
}

public class SomeBehaviour : MonoBehaviour
{
    [Inject] private ISomeInterface dependency;
}
```
To fill dependencies for objects created at runtime use **Factory.Instantiate** just similary to **Object.Instantiate**:
```csharp
public void SpawnUnityObject(SomeBehaviour someBehaviour) {
    Factory.Instantiate(someBehaviour);
}
```

Note: This package doesn't suppot injection of objects that aren't **MonoBehaviours**. It's specifically designed for Unity and small indie projects with little tech stack and complexity overhead. Although it definitely could be added if needed.
