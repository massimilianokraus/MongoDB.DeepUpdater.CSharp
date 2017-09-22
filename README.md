# MongoDB.DeepUpdater.CSharp
An extension of the official framework of MongoDB for C#.

## Intro

Updating certain elements of a nested array in MongoDB is difficult.
Some tecniques exist, like the use of the `$` operator.

But updating *multiple inner properties nested inside multiple arrays* is a very intricate and long task in MongoDB, whatever language you're using. Usually you have to calculate all the array indexes by hand, and concatenate the field-definition strings by hand, etc. etc.

See for example [this question on StackOverflow](https://stackoverflow.com/questions/10522347/mongodb-update-objects-in-a-documents-array-nested-updating) for a wider discussion.

The only alternative is to use the command `Replace`, sending the entire document to the server every single time.

This framework fills the gap and allows you to create deep update operations with a very short Linq-like sintax.


## Examples

Let's pretend that you have:
- a collection of `Person` documents;
- every `Person` has a collection of type `Pet`;
- you want to update a certain `Person` this way: every `Pet` of type 'Dog' must be given a new `Toy`: a ball.

Using the official framework you'd search for all the animal of type 'Dog', then you'd get the indexes of those `Pet`s inside the collection, and then you'd add the new `Toy`.
Then you'd build by hand all the UpdateDefinitions needed using the appropriate `Builder`

See instead what can you do with this framework:

    var addToyOperation = Builders<Person>.Update
        .Deep(mike) // Suppose that 'mike' is the Person to update
        .SelectArray(x => x.Pets)
        .Where(x => x.Type == "Dog")
        .SelectArray(x => x.Toys)
        .AddToSet(new Toy { Name = "Ball" });

    _mongoCollection.UpdateOne(filter, addToyOperation);

And that's it.


## Architecture
This framework is built on the top of the official one. No new low-level function has been implemented.

The framework is built with the concept of *fluent*. This allows to concatenate one query after the other to reach the point to be updated.

Also, the names `Select` and `Where` were chosen to keep them similar to the Linq's ones, making more natural for a C# developer to use them.


### Root - how to start

All must begin with a call to the `Deep<TDocument>(TDocument doc)` method, passing the entire document. This method builds the root of the fluent updates.
It has been created as extension method of the builder `Builders\<TDocument\>.Update`, in order to maintain the familiarity with the official framework that is well-known. It is also smooth to think in English:

`Builders<TDoc>.Update.Deep(doc)` --> _"with the builder, update deep this document..."_


### Select and SelectArray
If you want to select a single property, use `Select`. For example:

    .Select(settings => settings.TimeoutMillis)

With `Select` you can then use all the operators that apply to a single property, like `Set`, `Inc`, etc.

If you want to reach an array, use `SelectArray`. For example:

    .SelectArray(chip => chip.Pins)

In this case you can use all the array-operators, like `AddToSet`, or `PushEach`, etc.

If you use `Select` with an array, and not `SelectArray`, that property is not seen as a collection, but as a "single-property", so you cannot use the array-operators, just the single-property ones. Thus, you can re-set the entire array, but you can't add/remove elements.

You can select a nested property or array:

    .Select(house => house.LivingRoom.Table.Color)

(but a `NullReferenceException` will be thrown if one of the properties in the chain is null, so be careful).


### Where
When you use `SelectArray`, then you can filter the elements of the array that you want to update. For example:

    .SelectArray(company => company.Developers)
    .Where(dev => dev.StackOverflowPoints > 2000)

The `Where` operator calculates automatically the indexes of the wanted elements, and if multiple items match, they are all kept (this was not possible with the simple `$` operator, that keeps track only of the first element found).


### Update-operators
Whether the last fluent used is either of type 'single-property' or of type 'array', you have different update-operators callable: `Mul`, `BitwiseAnd`, etc. in the former case; `PullAll`, `PopFirst` etc in the latter.
All update-operators are mapped against the official operators that you create from the builders, so for example:

    .Select(canvas => canvas.Origin)
    .Set(new Point { X = 3, Y = 4 });

is like:

    Builders<Canvas>.Update.Set(canvas => canvas.Origin, new Point { X = 3, Y = 4 }); 


### Workflow - under the hood
Every fluent calculates the current update-operations based on the current information. Based on the method that you call on the current fluent, `Select`, `SelectArray` or `Where`, the new fluent concatenates to the current field-definition strings the new property/array name or index.

When you have reached the point of update, you can then call an update-operator. Every operator takes the field-definition strings generated by the fluents, generates the consequent update-definitions through the right builder, and combines them into a single update-operation that is ready to be sent to the database.

All the fluents work with deferred execution. The actual evaluation of the nested properties of the document and the generation of the update-definitions are done when you call an update-operation. See the class `DeferredExecutionTest` to have a demonstration of this.


## Errors and Exceptions
The update-operations that are generated have the same behavior of the normal ones. The same Exceptions are thrown. That's because this framework doesn't deal with the encountered Exceptions, but let them to pass through.

This means for example that if your fluent-query returns zero update-operations and you try to perform an update against the collection, you'll get an Exception, as usual with the official framework.



## Notes
This framework is available as NuGet Package. See https://www.nuget.org/packages/MongoDB.DeepUpdater/
