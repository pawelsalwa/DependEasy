Unity lightweight dependency injection framework.
## Installation
Via upm git dependency.
## Example
Example usage:
```csharp
using DependEasy;

public class SomeBehaviour : MonoBehaviour
{
    [Inject] private SomeDependency dependency;

    private void Awake() {
        Debug.Log(dependency.ToString()); // Throws NullReferenceException - Awake() is used for internal state
    }

    private void Start() {
        Debug.Log(dependency.ToString()); // works just fine - at Start(), all dependencies are already injected
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
