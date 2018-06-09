# RestApiEngine

Wrapper of HttpClient to make calls easier. I was looking for an easy wrapper and superior implementation of HttpClient and came up with this. I'm using it daily in other projects so I will be updating it day by day.

You set up the engine pointing to a base URL and you can make calls to different endpoints, different accept and adding different URI and Query parameters.  

## Easy Engine Set Up and First Get Call

```csharp
//Creates engine with base url and accept content type
var engine = new RestEngine("reqres.in/api/", "application/json")
    .AddHeader("User-Agent", "RestEngine .NET");
var processGetResult = engine.ProcessGetSync("users");
var result = task.Content.ReadAsStringAsync().Result;
```

## First Get Call with

```csharp
engine.ClearUriParams() //Clears URI Param
    .ClearHeaders() // Clears Headers
    .AddAccept("application/json") //Sets Accept
    .AddHeader("User-Agent", ".NET Foundation Repository Reporter")
    .AddUriParam("users") // adss /users to URI
    .AddQueryParam("page", "3"); // adds ?page=3 to URL

// executes get call to reqres.in/api/users?page=3
var processGetResult2 = engine.ProcessGetSync();
var result2 = task.Content.ReadAsStringAsync().Result;
```

Made by carjimfa. Doubts, supportand more, ask me anything on twitter: @carjimfa :)