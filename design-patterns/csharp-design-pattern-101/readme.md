# Projects

## proj00-simple-builder
- [Source](https://refactoring.guru/design-patterns/builder/csharp/example#example-0)

## proj01-builder
- [Source](https://code-maze.com/builder-design-pattern/)

## proj02-fluent-builder
- [Source](https://code-maze.com/builder-design-pattern/)
- The Fluent builder is a small variation of the Builder design pattern, which allows us to chain our builder calls towards different actions.

## proj03-fluent-builder-with-recursive-generic
- [Source](https://code-maze.com/fluent-builder-recursive-generics/)
- Solve the problem builders inherit from other builders. That raises a problem with chaining actions.
- Recursive Generics approach to enable the default behavior of our fluent interfaces.

## proj04-faceted-builder
- [Source](https://code-maze.com/faceted-builder/)
- Has a complex object and requires more than one builder class
- The faceted builder approach will create a facade over builders and allow us use all the builder to create a single object.

## proj05-pizza-app
- [Source](https://dev.to/kalkwst/the-builder-pattern-in-c-5bcc)
### Builder Pattern
### Fluent Builder:
- Extending the Builder pattern with a fluent API makes it more readable.
- It also allows us to chain statements for the object configuration.
### Parent-Child Builder
- Another verion of Builder pattern.
- First define a parent class tasked to create the complex object and then one or more child classes that create parts of the object.
- Define some Products which will be built by the Child builders.
- For this example, have 3 products. a Salad dish, a Slide dish and a Deal that contains some Slides and a Salad
- DealBuilder as a the parent Builder and calls the SaladBuilder and SideBuilder that act as the child Builders
### Progressive Builder
- Another version of Builder pattern
- It sequentially uses multiple builders to define a fixed sequence of method-chaining calls.
- The advantage of this implementation is that ensures that the object is built in the correct order.

## proj06-build-order
- [Source](https://www.youtube.com/watch?v=qCIr30WxJQw)
- Fluent Builder
- Use action to implement Nest Builder

## proj07-factorymethod
- [Source](https://refactoring.guru/design-patterns/factory-method/csharp/example)

## proj08-airconditioner
- [Source](https://code-maze.com/factory-method/)

## proj09-singleton
- [Source](https://code-maze.com/singleton/)

## proj10-prototype, proj11-protypeRegistry
- [Source](https://dev.to/kalkwst/prototype-pattern-in-c-2fnh)