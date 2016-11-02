# Design patterns

Design patterns are general reusable solutions to common problems that occurred in software designing. There are broadly 3 categories of design patterns:
[Creational](#creational-design-patterns), [Structural](#structural-design-patterns) and [Behavioral](#behavioral-design-patterns)

----------

<!-- # <a id="creational"></a>Creational Design Patterns -->
## Creational Design Patterns

The purpose of creational design patterns is to create or instantiate objects.

[Abstract factory](#abstract-factory)	

[Builder](#builder)	

[Factory Method](#factory-method)	

[Prototype](#prototype)	

[Singleton](#singleton)


----------

### Abstract factory 	

#### Motivation
The global trend in programming is to avoid the idea of adding code to existing classes in order to make them support encapsulating more general information for preventing too complicated changes in the code of the application. This is where the Abstract Factory pattern comes. The Abstract Factory pattern is used to create families of objects.	By using this pattern a framework is defined, which produces objects that follow a general pattern. This factory is paired with any concrete factory to produce objects that follow the similar pattern.  In other words, the Abstract Factory is a super-factory which creates other factories (Factory of factories).

#### Intent
Provide an interface for creating families of related or dependent objects without specifying their concrete classes. 

#### Applicability
Abstract Factory design pattern is to be used when the system is or should be configured to work with multiple families of products or when the system needs to be independent from the way the products it works with are created. As a specific case - when a family of products is designed to work only all together.

#### Implementation
Define an abstract class that specifies which objects are to be made. Then implement one concrete class for each family. Tables or files can also be used to essentially accomplish the same thing. Names of the desired classes can be kept in a database and then switches or run-time type identification (RTTI) can be used to instantiate the 
correct objects. 

#### Participants
 - AbstractFactory - declares a interface for operations that create
   abstract products. 
 - ConcreteFactory - implements operations to create
   concrete products. 
 - AbstractProduct - declares an interface for a type
   of product objects. 
 - Product - defines a product to be created by the
   corresponding ConcreteFactory; it implements the AbstractProduct
   interface. 
 - Client - uses the interfaces declared by the AbstractFactory and AbstractProduct classes.

The AbstractFactory class is the one that determines the actual type of the concrete object and creates it, but it returns an abstract pointer to the concrete object just created. This determines the behavior of the client that asks the factory to create an object of a certain abstract type and to return the abstract pointer to it, keeping the client from knowing anything about the actual creation of the object.
The second implication of this way of creating objects is that when the adding new concrete types is needed, all we have to do is modify the client code and make it use a different factory, which is far easier than instantiating a new type, which requires changing the code wherever a new object is created.

#### Consequences
The Abstract Factory pattern isolates the creation of objects from the client that needs them, giving the client only the possibility of accessing them through an interface, which makes the manipulation easier. The exchanging of product families is easier, as the class of a concrete factory appears in the code only where it is instantiated. Also if the products of a family are meant to work together, the Abstract Factory makes it easy to use the objects from only one family at a time. On the other hand, adding new products to the existing factories is difficult, because the Abstract Factory interface uses a fixed set of products that can be created. That is why adding a new product would mean extending the factory interface, which involves changes in the AbstractFactory class and all its subclasses. This section will discuss ways of implementing the pattern in order to avoid the problems that may appear.

#### Structural code in C# 

```
using System;
 
namespace Abstract.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Abstract Factory Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    public static void Main()
    {
      // Abstract factory #1
      AbstractFactory factory1 = new ConcreteFactory1();
      Client client1 = new Client(factory1);
      client1.Run();
 
      // Abstract factory #2
      AbstractFactory factory2 = new ConcreteFactory2();
      Client client2 = new Client(factory2);
      client2.Run();
 
      // Wait for user input
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'AbstractFactory' abstract class
  /// </summary>
  abstract class AbstractFactory
  {
    public abstract AbstractProductA CreateProductA();
    public abstract AbstractProductB CreateProductB();
  }
 
  /// <summary>
  /// The 'ConcreteFactory1' class
  /// </summary>
  class ConcreteFactory1 : AbstractFactory
  {
    public override AbstractProductA CreateProductA()
    {
      return new ProductA1();
    }
    public override AbstractProductB CreateProductB()
    {
      return new ProductB1();
    }
  }
 
  /// <summary>
  /// The 'ConcreteFactory2' class
  /// </summary>
  class ConcreteFactory2 : AbstractFactory
  {
    public override AbstractProductA CreateProductA()
    {
      return new ProductA2();
    }
    public override AbstractProductB CreateProductB()
    {
      return new ProductB2();
    }
  }
 
  /// <summary>
  /// The 'AbstractProductA' abstract class
  /// </summary>
  abstract class AbstractProductA
  {
  }
 
  /// <summary>
  /// The 'AbstractProductB' abstract class
  /// </summary>
  abstract class AbstractProductB
  {
    public abstract void Interact(AbstractProductA a);
  }
 
  /// <summary>
  /// The 'ProductA1' class
  /// </summary>
  class ProductA1 : AbstractProductA
  {
  }
 
  /// <summary>
  /// The 'ProductB1' class
  /// </summary>
  class ProductB1 : AbstractProductB
  {
    public override void Interact(AbstractProductA a)
    {
      Console.WriteLine(this.GetType().Name +
        " interacts with " + a.GetType().Name);
    }
  }
 
  /// <summary>
  /// The 'ProductA2' class
  /// </summary>
  class ProductA2 : AbstractProductA
  {
  }
 
  /// <summary>
  /// The 'ProductB2' class
  /// </summary>
  class ProductB2 : AbstractProductB
  {
    public override void Interact(AbstractProductA a)
    {
      Console.WriteLine(this.GetType().Name +
        " interacts with " + a.GetType().Name);
    }
  }
 
  /// <summary>
  /// The 'Client' class. Interaction environment for the products.
  /// </summary>
  class Client
  {
    private AbstractProductA _abstractProductA;
    private AbstractProductB _abstractProductB;
 
    // Constructor
    public Client(AbstractFactory factory)
    {
      _abstractProductB = factory.CreateProductB();
      _abstractProductA = factory.CreateProductA();
    }
 
    public void Run()
    {
      _abstractProductB.Interact(_abstractProductA);
    }
  }
}
```
Output
```
ProductB1 interacts with ProductA1
ProductB2 interacts with ProductA2 
```

#### Related patterns
An application usually needs only one instance of the ConcreteFactory class per family product. This means that it is best to implement it as a **Singleton**.	

For simplifying the code and increase the performance the **Prototype design pattern** can be used instead of Factory method, especially when there are many product families. In this case the concrete factory is initiated with a prototypical instance of each product in the family and when a new one is needed instead of creating it, the existing prototype is cloned. This approach eliminates the need for a new concrete factory for each new family of products.

#### C# example
[Animal Worlds Example](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/AbstractFactory)

The example demonstrates the creation of different animal worlds using different factories - one for African animals and one for American animals. Although the animals created by the Continent factories are different, the interactions among the animals remain the same. 

#### A UML diagram or image of the pattern
![Abstract factory diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/abstract.gif)

----------

### Builder 

#### Motivation
This pattern allows a client object to construct a complex object by specifying only its type and content, being shielded from the details related to the object's representation. This way the construction process can be used to create different representations. The logic of this process is isolated form the actual steps used in creating the complex object, so the process can be used again to create a different object form the same set of simple objects as the first one.

#### Intent
Separate the construction of a complex object from its representation so that the same construction process can create different representations. 

#### Applicability
Builder Pattern is used when the creation algorithm of a complex object is independent from the parts that actually compose the object and when the system needs to allow different representations for the objects that are being built.

#### Implementation

The Builder design pattern uses the Factory Builder pattern to decide which concrete class to initiate in order to build the desired type of object. The client, that may be either another object or the actual client that calls the main() method of the application, initiates the Builder and Director class. The Builder represents the complex object that needs to be built in terms of simpler objects and types. The constructor in the Director class receives a Builder object as a parameter from the Client and is responsible for calling the appropriate methods of the Builder class. In order to provide the Client with an interface for all concrete Builders, the Builder class should be an abstract one. This way you can add new types of complex objects by only defining the structure and reusing the logic for the actual construction process. The Client is the only one that needs to know about the new types, the Director needing to know which methods of the Builder to call.

#### Participants

 - The Builder class - specifies an abstract interface for creating parts of a Product object.
 - The ConcreteBuilder - constructs and puts together parts of the product by implementing the Builder interface. It defines and keeps track of the representation it creates and provides an interface for saving the product.
 - The Director class - constructs the complex object using the Builder interface.
 - The Product represents the complex object that is being built.

#### Structural code in C#
```
using System;
using System.Collections.Generic;
 
namespace Builder.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Builder Design Pattern.
  /// </summary>
  public class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    public static void Main()
    {
      // Create director and builders
      Director director = new Director();
 
      Builder b1 = new ConcreteBuilder1();
      Builder b2 = new ConcreteBuilder2();
 
      // Construct two products
      director.Construct(b1);
      Product p1 = b1.GetResult();
      p1.Show();
 
      director.Construct(b2);
      Product p2 = b2.GetResult();
      p2.Show();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Director' class
  /// </summary>
  class Director
  {
    // Builder uses a complex series of steps
    public void Construct(Builder builder)
    {
      builder.BuildPartA();
      builder.BuildPartB();
    }
  }
 
  /// <summary>
  /// The 'Builder' abstract class
  /// </summary>
  abstract class Builder
  {
    public abstract void BuildPartA();
    public abstract void BuildPartB();
    public abstract Product GetResult();
  }
 
  /// <summary>
  /// The 'ConcreteBuilder1' class
  /// </summary>
  class ConcreteBuilder1 : Builder
  {
    private Product _product = new Product();
 
    public override void BuildPartA()
    {
      _product.Add("PartA");
    }
 
    public override void BuildPartB()
    {
      _product.Add("PartB");
    }
 
    public override Product GetResult()
    {
      return _product;
    }
  }
 
  /// <summary>
  /// The 'ConcreteBuilder2' class
  /// </summary>
  class ConcreteBuilder2 : Builder
  {
    private Product _product = new Product();
 
    public override void BuildPartA()
    {
      _product.Add("PartX");
    }
 
    public override void BuildPartB()
    {
      _product.Add("PartY");
    }
 
    public override Product GetResult()
    {
      return _product;
    }
  }
 
  /// <summary>
  /// The 'Product' class
  /// </summary>
  class Product
  {
    private List<string> _parts = new List<string>();
 
    public void Add(string part)
    {
      _parts.Add(part);
    }
 
    public void Show()
    {
      Console.WriteLine("\nProduct Parts -------");
      foreach (string part in _parts)
        Console.WriteLine(part);
    }
  }
}
```
Output
```
Product Parts -------
PartA
PartB

Product Parts -------
PartX
PartY
```

#### Related patterns
The Builder design pattern is very similar, at some extent, to the Abstract Factory pattern. That's why it is important to be able to make the difference between the situations when one or the other is used. 
In the case of the Abstract Factory, the client uses the factory's methods to create its own objects. In the Builder's case, the Builder class is instructed on how to create the object and then it is asked for it, but the way that the class is put together is up to the Builder class, this detail making the difference between the two patterns.

#### C# example
[Vehicle builder](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Builder)

The example demonstates the Builder pattern in which different vehicles are assembled in a step-by-step fashion. The Shop uses VehicleBuilders to construct a variety of Vehicles in a series of sequential steps. 

#### A UML diagram or image of the pattern
![Builder diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/builder.gif)

----------

<!-- # <a id="factory-method"></a>Factory Method -->
### Factory Method 

#### Motivation
The Factory method defines an interface for creating an object, but leaves the choice of its type to the subclasses, creation being deferred at run-time. It is related to the idea on which libraries work - a library uses abstract classes for defining and maintaining relations between objects. One type of responsibility is creating such objects. The library knows when an object needs to be created, but not what kind of object it should create, this being specific to the application using the library. 

#### Intent
Define an interface for creating an object, but let subclasses decide which class to instantiate. Factory method allows a derived class to make the decision. Refers to the newly created object through a common interface.

#### Applicability
The need for implementing the Factory method is very frequent: when a class can't anticipate the type of the objects it is supposed to create or when a class wants its subclasses to be the ones to specific the type of a newly created object

#### Known uses
The Factory method is one of the most used and one of the more robust design patterns. It is often used with frameworks. Or when the different implementations of one class hierarchy requires a specific implementation of another class hierarchy. 

When designing an application it should be thought if there is really a need of factory to create objects. Maybe using it will bring unnecessary complexity in theapplication. If there are many object of the same base type and they are to be manipulated mostly as abstract objects, then there is a need of factory. 

#### Implementation
Have a method in the abstract class that is abstract (pure virtual). The abstract class’s code will refer to this method when it needs to instantiate a contained object. Note, however, that it doesn’t know which one it needs. That is why all classes derived from this one must implement this method with the appropriate new command to instantiate the proper object. 

#### Participants

 - Product defines the interface for objects the factory method creates.
 - ConcreteProduct implements the Product interface.
 - Creator (also refered as Factory because it creates the Product objects) declares the method FactoryMethod, which returns a Product object. May call the generating method for creating Product objects.
 - ConcreteCreator overrides the generating method for creating ConcreteProduct objects.

All concrete products are subclasses of the Product class, so all of them have the same basic implementation, at some extent. The Creator class specifies all standard and generic behavior of the products and when a new product is needed, it sends the creation details that are supplied by the client to the ConcreteCreator.

#### Structural code in C# 
```
using System;
 
namespace Factory.Structural
{
  /// <summary>
  /// MainApp startup class for Structural 
  /// Factory Method Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // An array of creators
      Creator[] creators = new Creator[2];
 
      creators[0] = new ConcreteCreatorA();
      creators[1] = new ConcreteCreatorB();
 
      // Iterate over creators and create products
      foreach (Creator creator in creators)
      {
        Product product = creator.FactoryMethod();
        Console.WriteLine("Created {0}",
          product.GetType().Name);
      }
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Product' abstract class
  /// </summary>
  abstract class Product
  {
  }
 
  /// <summary>
  /// A 'ConcreteProduct' class
  /// </summary>
  class ConcreteProductA : Product
  {
  }
 
  /// <summary>
  /// A 'ConcreteProduct' class
  /// </summary>
  class ConcreteProductB : Product
  {
  }
 
  /// <summary>
  /// The 'Creator' abstract class
  /// </summary>
  abstract class Creator
  {
    public abstract Product FactoryMethod();
  }
 
  /// <summary>
  /// A 'ConcreteCreator' class
  /// </summary>
  class ConcreteCreatorA : Creator
  {
    public override Product FactoryMethod()
    {
      return new ConcreteProductA();
    }
  }
 
  /// <summary>
  /// A 'ConcreteCreator' class
  /// </summary>
  class ConcreteCreatorB : Creator
  {
    public override Product FactoryMethod()
    {
      return new ConcreteProductB();
    }
  }
}
```
Output
```
Created ConcreteProductA
Created ConcreteProductB
```

#### C# example
[Document creation](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/FactoryMethod)

The example demonstrates the Factory method offering flexibility in creating different documents. The derived Document classes Report and Resume instantiate extended versions of the Document class. The Factory method is called in the constructor of the Document base class. 

#### A UML diagram or image of the pattern
![Factory method diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/factory.gif)

----------

<!-- # <a id="prototype"></a>Prototype -->
### Prototype 

#### Motivation
The Prototype design pattern allows an object to create customized objects without knowing their class or any details of how to create them. 

#### Intent
Specify the kind of objects to create using a prototypical instance, and create new objects by copying this prototype.

#### Applicability
Use Prototype Pattern when a system should be independent of how its products are created, composed, and represented, and classes to be instantiated are specified at run-time or avoiding the creation of a factory hierarchy is needed or it is more convenient to copy an existing instance than to create a new one.

#### Known uses
When objects being instantiated need to look like a copy of a particular object.  Allows for dynamically specifying what our instantiated objects look like.  
If the cost of creating a new object is large and creation is resource intensive, it is better to clone the object.

#### Implementation
Set up concrete classes of the class needing to be cloned. Each concrete class will construct itself to the appropriate value (optionally based on input parameters). When a new object is needed, clone an instantiation of this prototypical object. 

#### Participants

 - Client - creates a new object by asking a prototype to clone itself.
 - Prototype - declares an interface for cloning itself.
 - ConcretePrototype - implements the operation for cloning itself.

The process of cloning starts with an initialized and instantiated class. The Client asks for a new object of that type and sends the request to the Prototype class. A ConcretePrototype, depending of the type of object is needed, will handle the cloning through the Clone() method, making a new instance of itself.

#### Structural code in C# 
```
using System;
 
namespace Prototype.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Prototype Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Create two instances and clone each
 
      ConcretePrototype1 p1 = new ConcretePrototype1("I");
      ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
      Console.WriteLine("Cloned: {0}", c1.Id);
 
      ConcretePrototype2 p2 = new ConcretePrototype2("II");
      ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
      Console.WriteLine("Cloned: {0}", c2.Id);
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Prototype' abstract class
  /// </summary>
  abstract class Prototype
  {
    private string _id;
 
    // Constructor
    public Prototype(string id)
    {
      this._id = id;
    }
 
    // Gets id
    public string Id
    {
      get { return _id; }
    }
 
    public abstract Prototype Clone();
  }
 
  /// <summary>
  /// A 'ConcretePrototype' class 
  /// </summary>
  class ConcretePrototype1 : Prototype
  {
    // Constructor
    public ConcretePrototype1(string id)
      : base(id)
    {
    }
 
    // Returns a shallow copy
    public override Prototype Clone()
    {
      return (Prototype)this.MemberwiseClone();
    }
  }
 
  /// <summary>
  /// A 'ConcretePrototype' class 
  /// </summary>
  class ConcretePrototype2 : Prototype
  {
    // Constructor
    public ConcretePrototype2(string id)
      : base(id)
    {
    }
 
    // Returns a shallow copy
    public override Prototype Clone()
    {
      return (Prototype)this.MemberwiseClone();
    }
  }
}
```
Output
```
Cloned: I
Cloned: II
```

#### Related patterns
The Prototype design pattern sounds a lot like the Factory Method pattern. The difference is the fact that for the Factory the palette of prototypical objects never contains more than one object.

#### C# example
[Color objects](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Prototype)

The example demonstrates the Prototype pattern in which new Color objects are created by copying pre-existing, user-defined Colors of the same type. 

#### A UML diagram or image of the pattern
![Prototype diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/prototype.gif)

----------

<!-- # <a id="singleton"></a>Singleton -->
### Singleton 

#### Motivation
The Singleton pattern is one of the simplest design patterns. It involves only one class which is responsible to instantiate itself, to make sure it creates not more than one instance. In the same time it provides a global point of access to that instance. In this case the same instance can be used from everywhere, being impossible to invoke directly the constructor each time.

#### Intent
Ensure a class has only one instance and provide a global point of access to it. 

#### Applicability
The Singleton pattern should be used when there must be exactly one instance of a class, and when it must be accessible to clients from a global access point. 

#### Known uses
Sometimes it's important to have only one instance for a class. For example, in a system there should be only one window manager, or only a file system or only a print spooler. 
Usually Singletons are used for centralized management of internal or external resources and they provide a global point of access to themselves.

#### Implementation
Add a static member to the class that refers to the first instantiation of this object (initially it is null). Then, add a static method that instantiates this class if this member is null (and sets this member’s value) and then returns the value of this 
member. Finally, set the constructor to protected or private so no one can directly instantiate this class and bypass this mechanism. 

#### Participants

 - Singleton - defines an Instance operation that lets clients access its unique instance (instance is a class operation) and is responsible for creating and maintaining its own unique instance.

#### Structural code in C# 
```
using System;
 
namespace Singleton.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Singleton Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Constructor is protected -- cannot use new
      Singleton s1 = Singleton.Instance();
      Singleton s2 = Singleton.Instance();
 
      // Test for same instance
      if (s1 == s2)
      {
        Console.WriteLine("Objects are the same instance");
      }
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Singleton' class
  /// </summary>
  class Singleton
  {
    private static Singleton _instance;
 
    // Constructor is 'protected'
    protected Singleton()
    {
    }
 
    public static Singleton Instance()
    {
      // Uses lazy initialization.
      // Note: this is not thread safe.
      if (_instance == null)
      {
        _instance = new Singleton();
      }
 
      return _instance;
    }
  }
}
```
Output
```
Objects are the same instance
```

#### C# example
[Server requests](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Singleton)

The example demonstrates the Singleton pattern as a LoadBalancing object. Only a single instance (the Singleton) of the class can be created because servers may dynamically come on- or off-line and every request must go throught the one object that has knowledge about the state of the webfarm. 

#### A UML diagram or image of the pattern
![Singleton diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/singleton.gif)

----------

<!-- # <a id="structural"></a>Structural Design Patterns -->
## Structural Design Patterns

A structural design pattern serves as a blueprint for how different classes and objects are combined to form larger structures. They ease the design by identifying a simple way to realize relationships between entities. 
Unlike creational patterns, which are mostly different ways to fulfill the same fundamental purpose, each structural pattern has a different purpose. 

[Adapter](#adapter)	

[Bridge](#bridge)	

[Composite](#composite)	

[Decorator](#decorator)	

[Facade](#facade)

[Flyweight](#flyweight)

[Proxy](#proxy)

----------

### Adapter

#### Motivation
The adapter pattern is adapting between classes and objects. It is used to be an interface, a bridge between two objects. 

#### Intent
Convert the interface of a class into another interface clients expect. Adapter lets classes work together, that could not otherwise because of incompatible interfaces.

#### Applicability
Used when there is a class (Target) that invokes methods defined in an interface and there is another class (Adapter) that doesn't implement the interface but implements the operations that should be invoked from the first class through the interface. None of the existing code can be changed. The adapter will implement the interface and will be the bridge between the 2 classes.
Adapter is used also when there is a class (Target) for a generic use relying on some general interfaces and there are some implemented classes, not implementing the interface, that needs to be invoked by the Target class.

#### Implementation
Objects Adapters - based on Delegation. It uses composition, the Adaptee delegates the calls to Adaptee (opossed to class adapters which extends the Adaptee). This behaviour gives a few advantages over the class adapters. The main advantage is that the Adapter adapts not only the Adpatee but all its subclasses, with one "small" restriction: all the subclasses which don't add new methods, because the used mechanism is delegation.
Class Adapters - based on (Multiple) Inheritance. Class adapters can be implemented in languages supporting multiple inheritance .Java, C# or PHP does not support multiple inheritance, thus, such adapters can not be easy implemented in Java, C# or VB.NET. Class adapter uses inheritance instead of composition. It means that instead of delegating the calls to the Adaptee, it subclasses it. It must subclass both the Target and the Adaptee. 

#### Participants

 - Target - defines the domain-specific interface that Client uses.
 - Adapter - adapts the interface Adaptee to the Target interface.
 - Adaptee - defines an existing interface that needs adapting.
 - Client - collaborates with objects conforming to the Target interface.

#### Structural code in C# 
```
using System;
 
namespace Adapter.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Adapter Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Create adapter and place a request
      Target target = new Adapter();
      target.Request();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Target' class
  /// </summary>
  class Target
  {
    public virtual void Request()
    {
      Console.WriteLine("Called Target Request()");
    }
  }
 
  /// <summary>
  /// The 'Adapter' class
  /// </summary>
  class Adapter : Target
  {
    private Adaptee _adaptee = new Adaptee();
 
    public override void Request()
    {
      // Possibly do some other work
      //  and then call SpecificRequest
      _adaptee.SpecificRequest();
    }
  }
 
  /// <summary>
  /// The 'Adaptee' class
  /// </summary>
  class Adaptee
  {
    public void SpecificRequest()
    {
      Console.WriteLine("Called SpecificRequest()");
    }
  }
}
```
Output
```
Called SpecificRequest()
```

#### C# examples for their use
[Chemical databank](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Adapter)

The example demonstrates the use of a legacy chemical databank. Chemical compound objects access the databank through an Adapter interface. 

#### A UML diagram or image of the pattern
![Adapter diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/adapter.gif)

----------

### Bridge

#### Motivation
Sometimes an abstraction should have different implementations; consider an object that handles persistence of objects over different platforms using either relational databases or file system structures (files and folders). A simple implementation might choose to extend the object itself to implement the functionality for both file system and RDBMS. However this implementation would create a problem; Inheritance binds an implementation to the abstraction and thus it would be difficult to modify, extend, and reuse abstraction and implementation independently.

#### Intent
Decouple an abstraction from its implementation so that the two can vary independently.

#### Applicability
The bridge pattern applies when there is a need to avoid permanent binding between an abstraction and an implementation and when the abstraction and implementation need to vary independently. Using the Bridge pattern would leave the client code unchanged with no need to recompile the code.

#### Known uses
Graphical User Interface Frameworks use the bridge pattern to separate abstractions from platform specific implementation. For example GUI frameworks separate a Window abstraction from a Window implementation for Linux or Mac OS using the bridge pattern.
Object Persistence API can have many implementations depending on the presence or absence of a relational database, a file system, as well as on the underlying operating system.

#### Implementation
An Abstraction can be implemented by an abstraction implementation, and this implementation does not depend on any concrete implementers of the Implementor interface. Extending the abstraction does not affect the Implementor. Also extending the Implementor has no effect on the Abstraction.

#### Participants

 - Abstraction - Abstraction defines abstraction interface.
 - AbstractionImpl - Implements the abstraction interface using a reference to an object of type Implementor.
 - Implementor - Implementor defines the interface for implementation classes. This interface does not need to correspond directly to abstraction interface and can be very different. Abstraction imp provides an implementation in terms of operations provided by Implementor interface.
 - ConcreteImplementor1, ConcreteImplementor2 - Implements the Implementor interface.

#### Structural code in C# 
```
using System;
 
namespace Bridge.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Bridge Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      Abstraction ab = new RefinedAbstraction();
 
      // Set implementation and call
      ab.Implementor = new ConcreteImplementorA();
      ab.Operation();
 
      // Change implemention and call
      ab.Implementor = new ConcreteImplementorB();
      ab.Operation();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Abstraction' class
  /// </summary>
  class Abstraction
  {
    protected Implementor implementor;
 
    // Property
    public Implementor Implementor
    {
      set { implementor = value; }
    }
 
    public virtual void Operation()
    {
      implementor.Operation();
    }
  }
 
  /// <summary>
  /// The 'Implementor' abstract class
  /// </summary>
  abstract class Implementor
  {
    public abstract void Operation();
  }
 
  /// <summary>
  /// The 'RefinedAbstraction' class
  /// </summary>
  class RefinedAbstraction : Abstraction
  {
    public override void Operation()
    {
      implementor.Operation();
    }
  }
 
  /// <summary>
  /// The 'ConcreteImplementorA' class
  /// </summary>
  class ConcreteImplementorA : Implementor
  {
    public override void Operation()
    {
      Console.WriteLine("ConcreteImplementorA Operation");
    }
  }
 
  /// <summary>
  /// The 'ConcreteImplementorB' class
  /// </summary>
  class ConcreteImplementorB : Implementor
  {
    public override void Operation()
    {
      Console.WriteLine("ConcreteImplementorB Operation");
    }
  }
}
```
Output
```
ConcreteImplementorA Operation
ConcreteImplementorB Operation
```

#### Related patterns
Abstract Factory Pattern - An Abstract Factory pattern can be used to create and configure a particular Bridge, for example a factory can choose the suitable concrete implementor at runtime. 

#### C# examples for their use
[Customers data](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Bridge)

The example demonstrates the Bridge pattern in which a BusinessObject abstraction is decoupled from the implementation in DataObject. The DataObject implementations can evolve dynamically without changing any clients. 

#### A UML diagram or image of the pattern
![Bridge diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/bridge.gif)

----------

### Composite

#### Motivation
There are times when a program needs to manipulate a tree data structure and it is necessary to treat both Branches as well as Leaf Nodes uniformly. For example a program that manipulates a file system. A file system is a tree structure that contains Branches which are Folders as well as Leaf nodes which are Files. A folder object usually contains one or more file or folder objects and thus is a complex object where a file is a simple object. Since files and folders have many operations and attributes in common, such as moving and copying a file or a folder, listing file or folder attributes such as file name and size, it would be easier and more convenient to treat both file and folder objects uniformly by defining a File System Resource Interface.

#### Intent
Compose objects into tree structures to represent part-whole hierarchies. Composite lets clients treat individual objects and compositions of objects uniformly. 

#### Applicability
The composite pattern applies when there is a part-whole hierarchy of objects and a client needs to deal with objects uniformly regardless of the fact that an object might be a leaf or a branch.

#### Known uses
File System Implementation as discussed previously.
Graphics Editors as discussed previously.

#### Implementation
Graphics Drawing Editor: In graphics editors a shape can be basic or complex. An example of a simple shape is a line, where a complex shape is a rectangle which is made of four line objects. Since shapes have many operations in common such as rendering the shape to screen, and since shapes follow a part-whole hierarchy, composite pattern can be used to enable the program to deal with all shapes uniformly.

#### Participants
 - Component - Component is the abstraction for leafs and composites. It defines the interface that must be implemented by the objects in the composition. 
 - For example a file system resource defines move, copy, rename, and getSize methods for files and folders.
 - Composite - A Composite stores child components in addition to implementing methods defined by the component interface. Composites implement methods defined in the Component interface by delegating to child components. In addition composites provide additional methods for adding, removing, as well as getting components.
 - Client - The client manipulates objects in the hierarchy using the component interface.

A client has a reference to a tree data structure and needs to perform operations on all nodes independent of the fact that a node might be a branch or a leaf. The client simply obtains reference to the required node using the component interface, and deals with the node using this interface; it doesn't matter if the node is a composite or a leaf.

#### Consequences
 - The composite pattern defines class hierarchies consisting of primitive objects and composite objects. Primitive objects can be composed into more complex objects, which in turn can be composed.
 - Clients treat primitive and composite objects uniformly through a component interface which makes client code simple.
 - Adding new components can be easy and client code does not need to be changed since client deals with the new components through the component interface.

#### Structural code in C# 
```
using System;
using System.Collections.Generic;
 
namespace Composite.Structural
{
  /// <summary>
  /// MainApp startup class for Structural 
  /// Composite Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Create a tree structure
      Composite root = new Composite("root");
      root.Add(new Leaf("Leaf A"));
      root.Add(new Leaf("Leaf B"));
 
      Composite comp = new Composite("Composite X");
      comp.Add(new Leaf("Leaf XA"));
      comp.Add(new Leaf("Leaf XB"));
 
      root.Add(comp);
      root.Add(new Leaf("Leaf C"));
 
      // Add and remove a leaf
      Leaf leaf = new Leaf("Leaf D");
      root.Add(leaf);
      root.Remove(leaf);
 
      // Recursively display tree
      root.Display(1);
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Component' abstract class
  /// </summary>
  abstract class Component
  {
    protected string name;
 
    // Constructor
    public Component(string name)
    {
      this.name = name;
    }
 
    public abstract void Add(Component c);
    public abstract void Remove(Component c);
    public abstract void Display(int depth);
  }
 
  /// <summary>
  /// The 'Composite' class
  /// </summary>
  class Composite : Component
  {
    private List<Component> _children = new List<Component>();
 
    // Constructor
    public Composite(string name)
      : base(name)
    {
    }
 
    public override void Add(Component component)
    {
      _children.Add(component);
    }
 
    public override void Remove(Component component)
    {
      _children.Remove(component);
    }
 
    public override void Display(int depth)
    {
      Console.WriteLine(new String('-', depth) + name);
 
      // Recursively display child nodes
      foreach (Component component in _children)
      {
        component.Display(depth + 2);
      }
    }
  }
 
  /// <summary>
  /// The 'Leaf' class
  /// </summary>
  class Leaf : Component
  {
    // Constructor
    public Leaf(string name)
      : base(name)
    {
    }
 
    public override void Add(Component c)
    {
      Console.WriteLine("Cannot add to a leaf");
    }
 
    public override void Remove(Component c)
    {
      Console.WriteLine("Cannot remove from a leaf");
    }
 
    public override void Display(int depth)
    {
      Console.WriteLine(new String('-', depth) + name);
    }
  }
}
```
Output
```
-root
---Leaf A
---Leaf B
---Composite X
-----Leaf XA
-----Leaf XB
---Leaf C
```

#### Related patterns
Decorator Pattern - Decorator is often used with Composite. When decorators and composites are used together, they will usually have a common parent class. So decorators will have to support the Component interface with operations like Add, Remove, and GetChild. 

#### C# examples for their use
[Drawing elements](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Composite)

The example demonstrates the Composite pattern used in building a graphical tree structure made up of primitive nodes (lines, circles, etc) and composite nodes (groups of drawing elements that make up more complex elements). 

#### A UML diagram or image of the pattern
![Composite diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/composite.gif)

----------

### Decorator

#### Motivation
Extending an object's functionality can be done statically (at compile time) by using inheritance. However it might be necessary to extend an object's functionality dynamically (at runtime) as an object is used.
Consider the typical example of a graphical window. To extend the functionality of the graphical window for example by adding a frame to the window, would require extending the window class to create a FramedWindow class. To create a framed window it is necessary to create an object of the FramedWindow class. However it would be impossible to start with a plain window and to extend its functionality at runtime to become a framed window.

#### Intent
Attach additional responsibilities to an object dynamically. Decorators provide a flexible alternative to subclassing for extending functionality. 

#### Applicability
The Decorator pattern applies when there is a need to dynamically add as well as remove responsibilities to a class, and when subclassing would be impossible due to the large number of subclasses that could result.

#### Known uses
In Graphical User Interface toolkits windows behaviors can be added dynamically by using the Decorator design pattern.

#### Participants

 - Component - Interface for objects that can have responsibilities added to them dynamically.
 - ConcreteComponent - Defines an object to which additional responsibilities can be added.
 - Decorator - Maintains a reference to a Component object and defines an interface that conforms to Component's interface.
 - Concrete Decorators - Concrete Decorators extend the functionality of the component by adding state or adding behavior.

#### Consequences
Decoration is more convenient for adding functionalities to objects instead of entire classes at runtime. With Decoration it is also possible to remove the added functionalities dynamically.
Decoration adds functionality to objects at runtime which would make debugging system functionality harder.

#### Structural code in C# 
```
using System;

namespace Decorator.Structural
{
  /// <summary>
  /// MainApp startup class for Structural 
  /// Decorator Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Create ConcreteComponent and two Decorators
      ConcreteComponent c = new ConcreteComponent();
      ConcreteDecoratorA d1 = new ConcreteDecoratorA();
      ConcreteDecoratorB d2 = new ConcreteDecoratorB();
 
      // Link decorators
      d1.SetComponent(c);
      d2.SetComponent(d1);
 
      d2.Operation();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Component' abstract class
  /// </summary>
  abstract class Component
  {
    public abstract void Operation();
  }
 
  /// <summary>
  /// The 'ConcreteComponent' class
  /// </summary>
  class ConcreteComponent : Component
  {
    public override void Operation()
    {
      Console.WriteLine("ConcreteComponent.Operation()");
    }
  }
 
  /// <summary>
  /// The 'Decorator' abstract class
  /// </summary>
  abstract class Decorator : Component
  {
    protected Component component;
 
    public void SetComponent(Component component)
    {
      this.component = component;
    }
 
    public override void Operation()
    {
      if (component != null)
      {
        component.Operation();
      }
    }
  }
 
  /// <summary>
  /// The 'ConcreteDecoratorA' class
  /// </summary>
  class ConcreteDecoratorA : Decorator
  {
    public override void Operation()
    {
      base.Operation();
      Console.WriteLine("ConcreteDecoratorA.Operation()");
    }
  }
 
  /// <summary>
  /// The 'ConcreteDecoratorB' class
  /// </summary>
  class ConcreteDecoratorB : Decorator
  {
    public override void Operation()
    {
      base.Operation();
      AddedBehavior();
      Console.WriteLine("ConcreteDecoratorB.Operation()");
    }

    void AddedBehavior()
    {
    }
  }
}
```

#### Related patterns
Adapter Pattern - A Decorator is different from an Adapter in that a Decorator changes object's responsibilities, while an Adapter changes an object interface.
Composite Pattern - A Decorator can be viewed as a degenerate composite with only one component. However, a Decorator adds additional responsibilities.

#### C# examples for their use
[Borrow from a library](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Decorator)

The example demonstrates the Decorator pattern in which 'borrowable' functionality is added to existing library items (books and videos). 

#### A UML diagram or image of the pattern
![Decorator diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/decorator.gif)

----------

### Facade

#### Motivation
Facade pattern hides the complexities of the system and provides an interface to the client using which the client can access the system. This type of design pattern comes under structural pattern as this pattern adds an interface to existing system to hide its complexities.
This pattern involves a single class which provides simplified methods required by client and delegates calls to methods of existing system classes.

#### Intent
Provide a unified interface to a set of interfaces in a subsystem. Façade defines a higher-level interface that makes the subsystem easier to use. 

#### Applicability

#### Known uses

#### Implementation
The Façade pattern is simple to implement. It uses the C# concept of namespaces. Classes in namespaces have the facility to define accessibility as internal or public. If accessibility is defined as internal, the member is visible only in the assembly in which the namespace is compiled. In a very large system, the client's GUI will be in a different namespace from the library, so we can enforce the Façade. 
Alternative implementations of the Façade pattern will be discussed shortly.

#### Participants

 - Facade - knows which subsystem classes are responsible for a request and delegates client requests to appropriate subsystem objects.
 - Subsystem classes - implement subsystem functionality, handle work assigned by the Facade object and have no knowledge of the facade and keep no reference to it. 

#### Structural code in C# 
```
using System;
 
namespace Facade.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Facade Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    public static void Main()
    {
      Facade facade = new Facade();
 
      facade.MethodA();
      facade.MethodB();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Subsystem ClassA' class
  /// </summary>
  class SubSystemOne
  {
    public void MethodOne()
    {
      Console.WriteLine(" SubSystemOne Method");
    }
  }
 
  /// <summary>
  /// The 'Subsystem ClassB' class
  /// </summary>
  class SubSystemTwo
  {
    public void MethodTwo()
    {
      Console.WriteLine(" SubSystemTwo Method");
    }
  }
 
  /// <summary>
  /// The 'Subsystem ClassC' class
  /// </summary>
  class SubSystemThree
  {
    public void MethodThree()
    {
      Console.WriteLine(" SubSystemThree Method");
    }
  }
 
  /// <summary>
  /// The 'Subsystem ClassD' class
  /// </summary>
  class SubSystemFour
  {
    public void MethodFour()
    {
      Console.WriteLine(" SubSystemFour Method");
    }
  }
 
  /// <summary>
  /// The 'Facade' class
  /// </summary>
  class Facade
  {
    private SubSystemOne _one;
    private SubSystemTwo _two;
    private SubSystemThree _three;
    private SubSystemFour _four;
 
    public Facade()
    {
      _one = new SubSystemOne();
      _two = new SubSystemTwo();
      _three = new SubSystemThree();
      _four = new SubSystemFour();
    }
 
    public void MethodA()
    {
      Console.WriteLine("\nMethodA() ---- ");
      _one.MethodOne();
      _two.MethodTwo();
      _four.MethodFour();
    }
 
    public void MethodB()
    {
      Console.WriteLine("\nMethodB() ---- ");
      _two.MethodTwo();
      _three.MethodThree();
    }
  }
}
```
Output
```
MethodA() ----
SubSystemOne Method
SubSystemTwo Method
SubSystemFour Method

MethodB() ----
SubSystemTwo Method
SubSystemThree Method
```

#### Related patterns

#### C# examples for their use

#### A UML diagram or image of the pattern
![Facade diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/facade.gif)

----------

### Flyweight

#### Motivation
Some programs require a large number of objects that have some shared state among them. Consider for example a game of war, were there is a large number of soldier objects; a soldier object maintain the graphical representation of a soldier, soldier behavior such as motion, and firing weapons, in addition soldier's health and location on the war terrain. Creating a large number of soldier objects is a necessity however it would incur a huge memory cost. Note that although the representation and behavior of a soldier is the same their health and location can vary greatly. 

#### Intent
The intent of this pattern is to use sharing to support a large number of objects that have part of their internal state in common where the other part of state can vary.

#### Applicability
The flyweight pattern applies to a program using a huge number of objects that have part of their internal state in common where the other part of state can vary. The pattern is used when the larger part of the object's state can be made extrinsic (external to that object).

#### Known uses
Games with graphics as discussed with the War Game Example.
Text Editors: Object oriented text editors need to create Character Objects to represent each character that is in the document. A Character object maintains information about what is the character, what is its font, what is the size of the character, as well as character location inside the document. A document typically consists of extremely large number of character objects which requires large memory. The number of characters in general (Digits, Letters, Other special characters) is known and is fixed, and the fonts that can be applied to each character are also known, thus by creating a Letter flyweight that maintains Character Type (letter, digit, etc.), as well as font, and by creating a Letter Client object that only maintains each character's location inside the document, we have reduced the editor's memory requirements drastically. 

#### Implementation
A client needs a flyweight object; it calls the factory to get the flyweight object. The factory checks a pool of flyweights to determine if a flyweight object of the requested type is in the pool, if there is, the reference to that object is returned. If there is no object of the required type, the factory creates a flyweight of the requested type, adds it to the pool, and returns a reference to the flyweight. The flyweight maintains intrinsic state (state that is shared among the large number of objects that we have created the flyweight for) and provides methods to manipulate external state (State that vary from object to object and is not common among the objects we have created the flyweight for).

#### Participants
 - Flyweight - Declares an interface through which flyweights can receive and act on extrinsic state.
 - ConcreteFlyweight - Implements the Flyweight interface and stores intrinsic state. A ConcreteFlyweight object must be sharable. The Concrete flyweight object must maintain state that it is intrinsic to it, and must be able to manipulate state that is extrinsic. In the war game example graphical representation is an intrinsic state, where location and health states are extrinsic. Soldier moves, the motion behavior manipulates the external state (location) to create a new location.
 - FlyweightFactory - The factory creates and manages flyweight objects. In addition the factory ensures sharing of the flyweight objects. The factory maintains a pool of different flyweight objects and returns an object from the pool if it is already created, adds one to the pool and returns it in case it is new.
 - In the war example a Soldier Flyweight factory can create two types of flyweights : a Soldier flyweight, as well as a Colonel Flyweight. When the Client asks the Factory for a soldier, the factory checks to see if there is a soldier in the pool, if there is, it is returned to the client, if there is no soldier in pool, a soldier is created, added to pool, and returned to the client, the next time a client asks for a soldier, the soldier created previously is returned, no new soldier is created.
    Client - A client maintains references to flyweights in addition to computing and maintaining extrinsic state

#### Structural code in C# 
```
using System;
using System.Collections;
 
namespace Flyweight.Structural
{
  /// <summary>
  /// MainApp startup class for Structural 
  /// Flyweight Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Arbitrary extrinsic state
      int extrinsicstate = 22;
 
      FlyweightFactory factory = new FlyweightFactory();
 
      // Work with different flyweight instances
      Flyweight fx = factory.GetFlyweight("X");
      fx.Operation(--extrinsicstate);
 
      Flyweight fy = factory.GetFlyweight("Y");
      fy.Operation(--extrinsicstate);
 
      Flyweight fz = factory.GetFlyweight("Z");
      fz.Operation(--extrinsicstate);
 
      UnsharedConcreteFlyweight fu = new UnsharedConcreteFlyweight();
      fu.Operation(--extrinsicstate);
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'FlyweightFactory' class
  /// </summary>
  class FlyweightFactory
  {
    private Hashtable flyweights = new Hashtable();
 
    // Constructor
    public FlyweightFactory()
    {
      flyweights.Add("X", new ConcreteFlyweight());
      flyweights.Add("Y", new ConcreteFlyweight());
      flyweights.Add("Z", new ConcreteFlyweight());
    }
 
    public Flyweight GetFlyweight(string key)
    {
      return ((Flyweight)flyweights[key]);
    }
  }
 
  /// <summary>
  /// The 'Flyweight' abstract class
  /// </summary>
  abstract class Flyweight
  {
    public abstract void Operation(int extrinsicstate);
  }
 
  /// <summary>
  /// The 'ConcreteFlyweight' class
  /// </summary>
  class ConcreteFlyweight : Flyweight
  {
    public override void Operation(int extrinsicstate)
    {
      Console.WriteLine("ConcreteFlyweight: " + extrinsicstate);
    }
  }
 
  /// <summary>
  /// The 'UnsharedConcreteFlyweight' class
  /// </summary>
  class UnsharedConcreteFlyweight : Flyweight
  {
    public override void Operation(int extrinsicstate)
    {
      Console.WriteLine("UnsharedConcreteFlyweight: " + extrinsicstate);
    }
  }
}
```
Output
```
ConcreteFlyweight: 21
ConcreteFlyweight: 20
ConcreteFlyweight: 19
UnsharedConcreteFlyweight: 18
```

#### Related patterns
Factory and Singleton patterns - Flyweights are usually created using a factory and the Singleton is applied to that factory so that for each type or category of Flyweights a single instance is returned.
State and Strategy Patterns - State and Strategy objects are usually implemented as Flyweights.

#### C# examples for their use
[Pointsizes](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Flyweight)

The example demonstrates the Flyweight pattern in which a relatively small number of Character objects is shared many times by a document that has potentially many characters. 

#### A UML diagram or image of the pattern
![Flyweight diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/flyweight.gif)

----------

### Proxy

#### Motivation
Sometimes we need the ability to control the access to an object. For example if we need to use only a few methods of some costly objects we'll initialize those objects when we need them entirely. Until that point we can use some light objects exposing the same interface as the heavy objects. These light objects are called proxies and they will instantiate those heavy objects when they are really need and by then we'll use some light objects instead.
This ability to control the access to an object can be required for a variety of reasons: controlling when a costly object needs to be instantiated and initialized, giving different access rights to an object, as well as providing a sophisticated means of accessing and referencing objects running in other processes, on other machines.

#### Intent
The intent of this pattern is to provide a 'Placeholder' for an object to control access to it. 

#### Applicability
The Proxy design pattern is applicable when there is a need to control access to an Object, as well as when there is a need for a sophisticated reference to an Object. Common Situations where the proxy pattern is applicable are:

 - Virtual Proxies: delaying the creation and initialization of expensive objects until needed, where the objects are created on demand. For example creating the RealSubject object only when the doSomething method is invoked.
 - Remote Proxies: providing a local representation for an object that is in a different address space. A common example is Java RMI stub objects. The stub object acts as a proxy where invoking methods on the stub would cause the stub to communicate and invoke methods on a remote object (called skeleton) found on a different machine.
 - Protection Proxies: where a proxy controls access to RealSubject methods, by giving access to some objects while denying access to others.
 - Smart References: providing a sophisticated access to certain objects such as tracking the number of references to an object and denying access if a certain number is reached, as well as loading an object from database into memory on demand.

#### Known uses
In java Remote Method Invocation (RMI) an object on one machine (executing in one JVM) called a client can invoke methods on an object in another machine (another JVM). The second object is called a remote object. The proxy (also called a stub) resides on the client machine and the client invokes the proxy in as if it is invoking the object itself (remember that the proxy implements the same interface that RealSubject implements). The proxy itself will handle communication to the remote object, invoke the method on that remote object, and would return the result if any to the client. The proxy in this case is a Remote proxy.
Security Proxies that controls access to objects can be found in many object oriented languages including java, C#, C++.

#### Implementation
A client obtains a reference to a Proxy, the client then handles the Proxy in the same way it handles RealSubject and thus invoking the method doSomething(). At that point the proxy can do different things prior to invoking RealSubject's doSomething() method. The client might create a RealSubject object at that point, perform initialization, check permissions of the client to invoke the method, and then invoke the method on the object. The client can also do additional tasks after invoking the doSomething() method, such as incrementing the number of references to the object.

#### Participants

 - Subject - Interface implemented by the RealSubject and representing its services. The interface must be implemented by the proxy as well so that the proxy can be used in any location where the RealSubject can be used.
 - Proxy
	 - Maintains a reference that allows the Proxy to access the RealSubject.
	 - Implements the same interface implemented by the RealSubject so that the Proxy can be substituted for the RealSubject.
	 - Controls access to the RealSubject and may be responsible for its creation and deletion.
	 - Other responsibilities depend on the kind of proxy.
 - RealSubject - the real object that the proxy represents.

#### Structural code in C# 
```
using System;

namespace Proxy.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Proxy Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Create proxy and request a service
      Proxy proxy = new Proxy();
      proxy.Request();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Subject' abstract class
  /// </summary>
  abstract class Subject
  {
    public abstract void Request();
  }
 
  /// <summary>
  /// The 'RealSubject' class
  /// </summary>
  class RealSubject : Subject
  {
    public override void Request()
    {
      Console.WriteLine("Called RealSubject.Request()");
    }
  }
 
  /// <summary>
  /// The 'Proxy' class
  /// </summary>
  class Proxy : Subject
  {
    private RealSubject _realSubject;
 
    public override void Request()
    {
      // Use 'lazy initialization'
      if (_realSubject == null)
      {
        _realSubject = new RealSubject();
      }
 
      _realSubject.Request();
    }
  }
}
```
Output
```
Called RealSubject.Request()
```

#### Related patterns
Adapter Design Pattern - The Adapter implements a different interface to the object it adapts where a proxy implements the same interface as its subject.
Decorator Design Pattern - A Decorator implementation can be the same as the proxy however a decorator adds responsibilities to an object while a proxy controls access to it.

#### C# examples for their use
[Do some math](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Proxy)

The example demonstrates the Proxy pattern for a Math object represented by a MathProxy object. 

#### A UML diagram or image of the pattern
![Proxy diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/proxy.gif)

----------

## Behavioral Design Patterns

Behavioral design patterns are design patterns that identify common communication patterns between objects and realize these patterns. By doing so, these patterns increase flexibility in carrying out this communication.

[Chain of responsibility](#chain-of-responsibility)

[Command](#command)	

[Interpreter](#interpreter)	

[Iterator](#iterator)	

[Mediator](#mediator)	

[Memento](#memento)

[Null Object](#null-object)

[Observer](#observer)

[State](#state)

[Strategy](#strategy)

[Template method](#template-method)

[Visitor](#visitor)

----------

### Chain of responsibility
A way of passing a request between a chain of objects

#### Motivation
In writing an application of any kind, it often happens that the event generated by one object needs to be handled by another one. And we also happen to be denied access to the object which needs to handle the event. In this case there are two possibilities: there is the beginner/lazy approach of making everything public, creating reference to every object and continuing from there and then there is the expert approach of using the Chain of Responsibility.

The Chain of Responsibility design pattern allows an object to send a command without knowing what object will receive and handle it. The request is sent from one object to another making them parts of a chain and each object in this chain can handle the command, pass it on or do both. 

#### Intent
It avoids coupling the sender of a request to its receiver, giving this way other objects the possibility of handling the request too. Chain the receiving objects and pass the request along the chain until an object handles it. 

#### Applicability
A few situations when using the Chain of Responsibility is more effective:

 - More than one object can handle a command
 - The handler is not known in advance
 - The handler should be determined automatically
 - It’s wished that the request is addressed to a group of objects without explicitly specifying its receiver
 - The group of objects that may handle the command must be specified in a dynamic way

#### Known uses
The most usual example of a machine using the Chain of Responsibility is the vending machine coin slot: rather than having a slot for each type of coin, the machine has only one slot for all of them. The dropped coin is routed to the appropriate storage place that is determined by the receiver of the command.

#### Implementation
The Client in need of a request to be handled sends it to the chain of handlers, which are classes that extend the Handler class. Each of the handlers in the chain takes its turn at trying to handle the request it receives from the client. If ConcreteHandler_i can handle it, then the request is handled, if not it is sent to the handler ConcreteHandler_i+1, the next one in the chain.

#### Participants
Handler - defines an interface for handling requests

 - RequestHandler - handles the requests it is responsible for
 - If it can handle the request it does so, otherwise it sends the request to its successor
 - Client - sends commands to the first object in the chain that may handle the command
 - 
#### Structural code in C# 
```
using System;
 
namespace Chain.Structural
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Chain of Responsibility Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Setup Chain of Responsibility
      Handler h1 = new ConcreteHandler1();
      Handler h2 = new ConcreteHandler2();
      Handler h3 = new ConcreteHandler3();
      h1.SetSuccessor(h2);
      h2.SetSuccessor(h3);
 
      // Generate and process request
      int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };
 
      foreach (int request in requests)
      {
        h1.HandleRequest(request);
      }
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Handler' abstract class
  /// </summary>
  abstract class Handler
  {
    protected Handler successor;
 
    public void SetSuccessor(Handler successor)
    {
      this.successor = successor;
    }
 
    public abstract void HandleRequest(int request);
  }
 
  /// <summary>
  /// The 'ConcreteHandler1' class
  /// </summary>
  class ConcreteHandler1 : Handler
  {
    public override void HandleRequest(int request)
    {
      if (request >= 0 && request < 10)
      {
        Console.WriteLine("{0} handled request {1}",
          this.GetType().Name, request);
      }
      else if (successor != null)
      {
        successor.HandleRequest(request);
      }
    }
  }
 
  /// <summary>
  /// The 'ConcreteHandler2' class
  /// </summary>
  class ConcreteHandler2 : Handler
  {
    public override void HandleRequest(int request)
    {
      if (request >= 10 && request < 20)
      {
        Console.WriteLine("{0} handled request {1}",
          this.GetType().Name, request);
      }
      else if (successor != null)
      {
        successor.HandleRequest(request);
      }
    }
  }
 
  /// <summary>
  /// The 'ConcreteHandler3' class
  /// </summary>
  class ConcreteHandler3 : Handler
  {
    public override void HandleRequest(int request)
    {
      if (request >= 20 && request < 30)
      {
        Console.WriteLine("{0} handled request {1}",
          this.GetType().Name, request);
      }
      else if (successor != null)
      {
        successor.HandleRequest(request);
      }
    }
  }
}
```
Output
```
ConcreteHandler1 handled request 2
ConcreteHandler1 handled request 5
ConcreteHandler2 handled request 14
ConcreteHandler3 handled request 22
ConcreteHandler2 handled request 18
ConcreteHandler1 handled request 3
ConcreteHandler3 handled request 27
ConcreteHandler3 handled request 20
```

#### C# examples for their use
[Responsibility chained lectors](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/ChainOfResponsibility)

The example demonstrates the Chain of Responsibility pattern in which several linked lectors can rate exams according the exam result or hand it off to a superior.

#### A UML diagram or image of the pattern
![Chain of Responsibility](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/chain.gif)

----------

### Command

#### Intent
Encapsulate a request as an object. Allows the parameterization of clients with different requests. Allows saving the requests in a queue. Supports undoable operations. 

#### Applicability
The applicability of the Command design pattern can be found in these cases below:

- parameterizes objects depending on the action they must perform
- specifies or adds in a queue and executes requests at different moments in time
- offers support for undoable actions (the Execute method can memorize the state and allow going back to that state)
- structures the system in high level operations that based on primitive operations
- decouples the object that invokes the action from the object that performs the action. Due to this usage it is also known as Producer - Consumer design pattern.

#### Implementation
The Client asks for a command to be executed. The Invoker takes the command, encapsulates it and places it in a queue, in case there is something else to do first, and the ConcreteCommand that is in charge of the requested command, sending its result to the Receiver.

#### Participants

- Command - declares an interface for executing an operation;
- ConcreteCommand - extends the Command interface, implementing the Execute method by invoking the corresponding operations on Receiver. It defines a link between the Receiver and the action.
- Client - creates a ConcreteCommand object and sets its receiver;
- Invoker - asks the command to carry out the request;
- Receiver - knows how to perform the operations;

#### Structural code in C# 
```
using System;
 
namespace Command.Structural
{
  /// <summary>
  /// MainApp startup class for Structural 
  /// Command Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Create receiver, command, and invoker
      Receiver receiver = new Receiver();
      Command command = new ConcreteCommand(receiver);
      Invoker invoker = new Invoker();
 
      // Set and execute command
      invoker.SetCommand(command);
      invoker.ExecuteCommand();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Command' abstract class
  /// </summary>
  abstract class Command
  {
    protected Receiver receiver;
 
    // Constructor
    public Command(Receiver receiver)
    {
      this.receiver = receiver;
    }
 
    public abstract void Execute();
  }
 
  /// <summary>
  /// The 'ConcreteCommand' class
  /// </summary>
  class ConcreteCommand : Command
  {
    // Constructor
    public ConcreteCommand(Receiver receiver) :
      base(receiver)
    {
    }
 
    public override void Execute()
    {
      receiver.Action();
    }
  }
 
  /// <summary>
  /// The 'Receiver' class
  /// </summary>
  class Receiver
  {
    public void Action()
    {
      Console.WriteLine("Called Receiver.Action()");
    }
  }
 
  /// <summary>
  /// The 'Invoker' class
  /// </summary>
  class Invoker
  {
    private Command _command;
 
    public void SetCommand(Command command)
    {
      this._command = command;
    }
 
    public void ExecuteCommand()
    {
     _command.Execute();
    }
  }
}
```
Output
```
Called Receiver.Action()
```

#### C# examples for their use
[Undo and Redo calculations](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Command)

The example demonstrates the Command pattern used in a simple calculator with unlimited number of undo's and redo's.

#### A UML diagram or image of the pattern
![Command](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/command.gif)

----------

### Interpreter
A way to include language elements in a program

----------

### Iterator

#### Motivation
The idea of the iterator pattern is to take the responsibility of accessing and passing trough the objects of the collection and put it in the iterator object. The iterator object will maintain the state of the iteration, keeping track of the current item and having a way of identifying what elements are next to be iterated.

#### Intent
Provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation.

The abstraction provided by the iterator pattern allows modifying the collection implementation without making any changes outside of collection. It enables creating a general purpose GUI component that will be able to iterate through any collection of the application. 

#### Applicability
The iterator pattern allow us to:

 - access contents of a collection without exposing its internal structure.
 - support multiple simultaneous traversals of a collection.
 - provide a uniform interface for traversing different collection. 

#### Participants

 - IIterator - This interface represent the AbstractIterator, defining the iterator
 - BookIterator - This is the implementation of Iterator(implements the IIterator interface)
 - IContainer - This is an interface defining the Agregate
 - BooksCollection - An implementation of the collection

#### Structural code in C# 
```
using System;
using System.Collections;
 
namespace Iterator.Structural
{
    /// <summary>
    /// MainApp startup class for Structural 
    /// Iterator Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            ConcreteAggregate a = new ConcreteAggregate();
            a[0] = "Item A";
            a[1] = "Item B";
            a[2] = "Item C";
            a[3] = "Item D";
 
            // Create Iterator and provide aggregate
            Iterator i = a.CreateIterator();
 
            Console.WriteLine("Iterating over collection:");
 
            object item = i.First();
            while (item != null)
            {
                Console.WriteLine(item);
                item = i.Next();
            }
 
            // Wait for user
            Console.ReadKey();
        }
    }
 
    /// <summary>
    /// The 'Aggregate' abstract class
    /// </summary>
    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }
 
    /// <summary>
    /// The 'ConcreteAggregate' class
    /// </summary>
    class ConcreteAggregate : Aggregate
    {
        private ArrayList _items = new ArrayList();
 
        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }
 
        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }
 
        // Indexer
        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }
 
    /// <summary>
    /// The 'Iterator' abstract class
    /// </summary>
    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }
 
    /// <summary>
    /// The 'ConcreteIterator' class
    /// </summary>
    class ConcreteIterator : Iterator
    {
        private ConcreteAggregate _aggregate;
        private int _current = 0;
 
        // Constructor
        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this._aggregate = aggregate;
        }
 
        // Gets first iteration item
        public override object First()
        {
            return _aggregate[0];
        }
 
        // Gets next iteration item
        public override object Next()
        {
            object ret = null;
            if (_current < _aggregate.Count - 1)
            {
                ret = _aggregate[++_current];
            }
 
            return ret;
        }
 
        // Gets current iteration item
        public override object CurrentItem()
        {
            return _aggregate[_current];
        }
 
        // Gets whether iterations are complete
        public override bool IsDone()
        {
            return _current >= _aggregate.Count;
        }
    }
}
```
Output
```
Iterating over collection:
Item A
Item B
Item C
Item D
```

#### C# examples for their use
[Iterating an object](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Iterator)

The example demonstrates the Iterator pattern which provides for a way to iterate over a collection of items without detailing the underlying structure of the collection.

#### A UML diagram or image of the pattern
![Iterator](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/iterator.gif)

----------

### Mediator
Defines simplified communication between classes

----------

### Memento
Capture and restore an object's internal state

----------

### Null Object
Designed to act as a default value of an object

----------

### Observer

#### Motivation
The Observer Design Pattern can be used whenever a subject has to be observed by one or more observers.

#### Intent
Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically. 

#### Applicability
The observer pattern is used when the change of a state in one object must be reflected in another object without keeping the objects tight coupled and when the framework we are writing needs to be enhanced in future with new observers with minimal changes.

#### Known uses
Model View Controller Pattern - The observer pattern is used in the model view controller (MVC) architectural pattern. In MVC the this pattern is used to decouple the model from the view. View represents the Observer and the model is the Observable object.
Event management - This is one of the domains where the Observer patterns is extensively used. Swing and .Net are extensively using the Observer pattern for implementing the events mechanism.

#### Implementation
The main framework instantiate the ConcreteObservable object. Then it instantiate and attaches the concrete observers to it using the methods defined in the Observable interface. Each time the state of the subject it's changing it notifies all the attached Observers using the methods defined in the Observer interface. When a new Observer is added to the application, all we need to do is to instantiate it in the main framework and to add attach it to the Observable object. The classes already created will remain unchanged.

#### Participants

 - Observable - interface or abstract class defining the operations for attaching and de-attaching observers to the client. In the GOF book this class/interface is known as Subject.
 - ConcreteObservable - concrete Observable class. It maintain the state of the object and when a change in the state occurs it notifies the attached Observers.
 - Observer - interface or abstract class defining the operations to be used to notify this object.
 - ConcreteObserverA, ConcreteObserver2 - concrete Observer implementations.


#### Structural code in C# 
```
using System;
using System.Collections.Generic;
 
namespace Observer.Structural
{
  /// <summary>
  /// MainApp startup class for Structural 
  /// Observer Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Configure Observer pattern
      ConcreteSubject s = new ConcreteSubject();
 
      s.Attach(new ConcreteObserver(s, "X"));
      s.Attach(new ConcreteObserver(s, "Y"));
      s.Attach(new ConcreteObserver(s, "Z"));
 
      // Change subject and notify observers
      s.SubjectState = "ABC";
      s.Notify();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Subject' abstract class
  /// </summary>
  abstract class Subject
  {
    private List<Observer> _observers = new List<Observer>();
 
    public void Attach(Observer observer)
    {
      _observers.Add(observer);
    }
 
    public void Detach(Observer observer)
    {
      _observers.Remove(observer);
    }
 
    public void Notify()
    {
      foreach (Observer o in _observers)
      {
        o.Update();
      }
    }
  }
 
  /// <summary>
  /// The 'ConcreteSubject' class
  /// </summary>
  class ConcreteSubject : Subject
  {
    private string _subjectState;
 
    // Gets or sets subject state
    public string SubjectState
    {
      get { return _subjectState; }
      set { _subjectState = value; }
    }
  }
 
  /// <summary>
  /// The 'Observer' abstract class
  /// </summary>
  abstract class Observer
  {
    public abstract void Update();
  }
 
  /// <summary>
  /// The 'ConcreteObserver' class
  /// </summary>
  class ConcreteObserver : Observer
  {
    private string _name;
    private string _observerState;
    private ConcreteSubject _subject;
 
    // Constructor
    public ConcreteObserver(
      ConcreteSubject subject, string name)
    {
      this._subject = subject;
      this._name = name;
    }
 
    public override void Update()
    {
      _observerState = _subject.SubjectState;
      Console.WriteLine("Observer {0}'s new state is {1}",
        _name, _observerState);
    }
 
    // Gets or sets subject
    public ConcreteSubject Subject
    {
      get { return _subject; }
      set { _subject = value; }
    }
  }
}
```
Output
```
Observer X's new state is ABC
Observer Y's new state is ABC
Observer Z's new state is ABC
```

#### Related patterns
Factory pattern - It's very likely to use the factory pattern to create the Observers so no changes will be required even in the main framework. The new observers can be added directly in the configuration files.
Template Method - The observer pattern can be used in conjunction with the Template Method Pattern to make sure that Subject state is self-consistent before notification
Mediator Pattern - The mediator pattern can be used when we have cases of complex cases of many subjects an many observers

#### C# examples for their use
[Notifying on value changes](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/Observer)

The example demonstrates the Observer pattern in which registered investors are notified every time a stock changes value. 

#### A UML diagram or image of the pattern
![Observer](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/observer.gif)

----------

### State
Alter an object's behavior when its state changes

----------

### Strategy
Encapsulates an algorithm inside a class

![Strategy](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/strategy.gif)

----------

### Template method
Defer the exact steps of an algorithm to a subclass

----------

### Visitor
Defines a new operation to a class without change

----------

http://www.oodesign.com/structural-patterns/
http://www.dofactory.com/net/adapter-design-pattern
