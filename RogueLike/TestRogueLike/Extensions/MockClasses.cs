using RogueLike.CoreObjects;
using RogueLike.Screens;

namespace TestRogueLike.MockClasses
{
    /// <summary>
    /// A files which holds mock classes used to test abstract classes or provide a public interface to protected functions for testing
    /// </summary>
    
    /// <summary>
    /// We cannot instantiate component because it is private, so we create this test wrapper so that we can test the Component class
    /// </summary>
    public class MockComponent : Component { }

    /// <summary>
    /// A class used to test templated functions which allow returning of an inherited classes derived from Component
    /// </summary>
    public class MockInheritedComponent : MockComponent { }

    /// <summary>
    /// A class used to test the abstract base screen class
    /// </summary>
    public class MockBaseScreen : BaseScreen { }
}
