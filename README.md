# Design patterns

Design patterns are general reusable solutions to common problems that occurred in software designing. There are broadly 3 categories of design patterns:
[Creational](#creational-design-patterns), [Structural](#structural-design-patterns) and [Behavioral](#behavioral-design-patterns)

<!-- # <a id="creational"></a>Creational Design Patterns -->
## Creational Design Patterns

The purpose of creational design patterns is to create or instantiate objects.

[Abstract factory](#abstract-factory)	

[Builder](#builder)	

[Factory Method](#factory-method)	

[Prototype](#prototype)	

[Singleton](#singleton)

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

<!-- # <a id="builder"></a>Builder  -->
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

<!-- # <a id="structural"></a>Structural Design Patterns -->
## Structural Design Patterns

<!-- # <a id="behavioral"></a>Behavioral Design Patterns -->
## Behavioral Design Patterns


http://www.oodesign.com/structural-patterns/
http://www.dofactory.com/net/adapter-design-pattern
