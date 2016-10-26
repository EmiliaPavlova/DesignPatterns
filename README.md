# Creational Design Patterns

Design patterns are general reusable solutions to common problems that occurred in software designing. There are broadly 3 categories of design patterns, i.e., Creational, Behavioral and Structural. 
The purpose of creational design patterns is to create or instantiate objects.

[Abstract factory](#abstract-factory)
[Builder](#builder)
[Factory Method](#factory-method)
[Prototype](#prototype)
[Singleton](#singleton)

## Abstract factory 	

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
 
namespace DoFactory.GangOfFour.Abstract.Structural
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

#### C# examples for their use
[Animal Worlds Example](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/AbstractFactory)

The example demonstrates the creation of different animal worlds using different factories - one for African animals and one for American animals. Although the animals created by the Continent factories are different, the interactions among the animals remain the same. 

#### A UML diagram or image of the pattern
![Abstract factory diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/abstract.gif)

<!-- # <a id="builder"></a>Builder  -->
## Builder 

#### Motivation
#### Intent
#### Applicability
#### Known uses
#### Implementation
Create a factory object that contains several methods. Each method is called separately and performs a necessary step in the building process. When the client object is through, it calls a method to get the constructed object returned to it.  Derive classes from the builder object to specialize steps. 

#### Participants
#### Consequences
#### Structure
#### Related patterns
#### C# examples for their use
#### A UML diagram or image of the pattern
![Builder diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/builder.gif)

**Indicators in analysis:** Several different kinds of complex objects can be built with the same overall build process, but where there is variation in the individual construction steps.  
**Indicators in design:** You want to hide the implementation of instantiating complex object, or you want to bring together all of the rules for instantiating complex objects. 

<!-- # <a id="factory-method"></a>Factory Method -->
## Factory Method 

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
 
namespace DoFactory.GangOfFour.Factory.Structural
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

#### C# examples for their use
[Document creation](https://github.com/EmiliaPavlova/DesignPatterns/tree/master/DesignPatternsExample/FactoryMethod)

The example demonstrates the Factory method offering flexibility in creating different documents. The derived Document classes Report and Resume instantiate extended versions of the Document class. The Factory method is called in the constructor of the Document base class. 

#### A UML diagram or image of the pattern
![Factory method diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/factory.gif)

<!-- # <a id="prototype"></a>Prototype -->
## Prototype 

#### Motivation
#### Intent
#### Applicability
#### Known uses
#### Implementation
Set up concrete classes of the class needing to be cloned. Each concrete class will construct itself to the appropriate value (optionally based on input parameters). When a new object is needed, clone an instantiation of this prototypical object. 

#### Participants
#### Consequences
#### Structure
#### Related patterns
#### C# examples for their use
#### A UML diagram or image of the pattern
![Prototype diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/prototype.gif)

**Indicators in analysis:** There are prototypical instances of things. 
**Indicators in design:** When objects being instantiated need to look like a copy of a particular object.  Allows for dynamically specifying what our instantiated objects look like.  

<!-- # <a id="singleton"></a>Singleton -->
## Singleton 

#### Motivation

#### Intent
Ensure a class has only one instance and provide a global point of access to it. 

#### Applicability

#### Known uses

#### Implementation
Add a static member to the class that refers to the first instantiation of this object (initially it is null). Then, add a static method that instantiates this class if this member is null (and sets this member’s value) and then returns the value of this 
member. Finally, set the constructor to protected or private so no one can directly instantiate this class and bypass this mechanism. 

#### Participants

#### Consequences

#### Structure

#### Related patterns

#### C# examples for their use

#### A UML diagram or image of the pattern
![Singleton diagram](https://github.com/EmiliaPavlova/DesignPatterns/blob/master/imgs/singleton.gif)

**Indicators in analysis:** There exists only one entity of something in the problem domain that is used by several different things. 
**Indicators in design:** Several different client objects need to refer to the same thing and we want to make sure we don’t have more than one of them.  You only want to have one of an object but there is no higher object controlling the instantiation of the object in questions.  
**Field notes:** You can get much the same function as Singletons with static methods.  Therefore, the Singleton should be used only when statics don’t work well.  This occurs when you need to control when the class is instantiated (that is, static members are allocated).  Another case is if you want to use polymorphism on the 
Singleton.
