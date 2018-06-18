# RestApiEngine

Wrapper of HttpClient to make calls easier. I was looking for an easy wrapper and superior implementation of HttpClient and came up with this. I'm using it daily in other projects so I will be updating it day by day.

You set up the engine pointing to a base URL and you can make calls to different endpoints, different accept and adding different URI and Query parameters.  

## Install

To install this package via Nuget:

``` powershell
Install-Package RestApiEngine -Version 0.0.1
```

## Easy Engine Set Up and First Get Call

```csharp
//Creates engine with base url and accept content type
var engine = new RestEngine("reqres.in/api/", "application/json")
    .AddHeader("User-Agent", "RestEngine .NET");
var processGetResult = engine.ProcessGetSync("users");
var result = processGetResult.Content.ReadAsStringAsync().Result;
```

## First Get Call with Query parameters

```csharp
engine.ClearUriParams() //Clears URI Params
    .ClearHeaders() // Clears Headers
    .AddAccept("application/json") //Sets Accept
    .AddHeader("User-Agent", ".NET Foundation Repository Reporter")
    .AddUriParam("users") // adss /users to URI
    .AddQueryParam("page", "3"); // adds ?page=3 to URL

// executes get call to reqres.in/api/users?page=3
var processGetResult2 = engine.ProcessGetSync();
var result2 = processGetResult2.Content.ReadAsStringAsync().Result;
```

## Post Call

```csharp
engine.ClearQueryParams();
var body="{\"name\": \"morpheus\",\"job\": \"leader\"}";
engine.AddBodyString(body, Encoding.UTF8, "application/json");
var processPostResult=engine.ProcessGetSync();
var postResult=processPostResult.Content.ReadAsStringAsync().Result;
```

## Important Methods

### Engine Initialization

#### Blank Engine

```csharp
var engine=new RestEngine();
```

#### Passing Only Base URL

```csharp
var url="http://example.com";
var engine=new RestEngine(url);
```

#### Passing baseUrl and Accept Content-Type

```csharp
var url="http://example.com";
var acceptContentType="application/json";
var engine=new RestEngine(url, acceptContentType);
```

### Headers, Uri Params and Query Params

#### Add Accept Content-Type

When we've intialized a blank engine, we'll need to add a Content-Type:

```csharp
engine.AddAccept("application/json")
```

#### Add Header

We can also add a header in a Key/value way:

```csharp
engine.AddHeader("Engine", "My Awesome Engine");
```

#### Add URI Parameter

Adds URI Param, e.g. "/users" to our base URL:

```csharp
engine.AddUriParam("users");
```

#### Add Query Parameter

Adds Query Param, e.g. "?page=5" to our base URL in a key/value way:

```csharp
engine.AddQueryParam("page", "5");
```

We can add as much query params as we want, th engine processes the ? and & automatically.

```csharp
// this is http://example.com/users?page=5&resultsPerPage=15
engine.AddQueryParam("page", "5").AddQueryParam("resultsPerPage", "15");
```

#### Add Authentication

##### Adds Bearer Token Authentication

Adds Bearer token for most .NET JWT based backend applications.

```csharp
engine.AddBearerAuthentication("myAwesomeTokenYoullNeverKnow");
```

##### Adds Custom Token Authentication

Adds custom token auth for the any backend applications.

```csharp
engine.AddBearerAuthentication("myScheme", "myNotSoAwesomeToken");
```

#### Adds Body String to make POST and PUT calls (or any call you want)

We pass the body string, the encoding type and the content-type.

```csharp
engine.AddBodyString("My Body String", Encoding.UTF8, "Accept Content Type");
```

### GET Calls

#### GET by Path

We can make calls directly to a path if we don't want to set up all the engine, we can set up an engine with a base URL and then make calls to a /path.

##### Async GET By Path

```csharp
var engine=new RestEngine("http://example.com");
var taskResult = await engine.ProcessGetAsync("/api/users");
```

##### Sync GET By Path

```csharp
var engine=new RestEngine("http://example.com");
var result = engine.ProcessGetSync("/api/users");
```

#### Process Get With everything setted up

This makes the call to the environment or context built with headers, baseUrl, Accept, uri and query params.

##### Async GET by context

```csharp
var taskResult = await engine.ProcessGetAsync();
```

##### Sync GET by context

```csharp
var result = engine.ProcessGetSync();
```

### POST Calls

#### POST by Path

We can make calls directly to a path if we don't want to set up all the engine, we can set up an engine with a base URL and then make calls to a /path.

##### Async POST By Path

```csharp
var engine=new RestEngine("http://example.com");
var taskResult = await engine.ProcessPostAsync("/api/users");
```

##### Sync POST By Path

```csharp
var engine=new RestEngine("http://example.com");
var result = engine.ProcessPostSync("/api/users");
```

#### Process POST With everything setted up

This makes the post call to the environment or context built with headers, baseUrl, Accept, body string, uri and query params.

##### Async POST by context

```csharp
var taskResult = await engine.ProcessPostAsync();
```

##### Sync POST by context

```csharp
var result = engine.ProcessPostSync();
```

### PUT Calls (Min Version 0.1)

#### PUT by Path

We can make calls directly to a path if we don't want to set up all the engine, we can set up an engine with a base URL and then make calls to a /path.

##### Async PUT By Path

```csharp
var engine=new RestEngine("http://example.com");
var taskResult = await engine.ProcessPutAsync("/api/users");
```

##### Sync PUT By Path

```csharp
var engine=new RestEngine("http://example.com");
var result = engine.ProcessPutSync("/api/users");
```

#### Process PUT With everything setted up

This makes the Put call to the environment or context built with headers, baseUrl, Accept, body string, uri and query params.

##### Async PUT by context

```csharp
var taskResult = await engine.ProcessPutAsync();
```

##### Sync PUT by context

```csharp
var result = engine.ProcessPutSync();
```

### DELETE Calls (Min Version 0.1)

#### DELETE by Path

We can make calls directly to a path if we don't want to set up all the engine, we can set up an engine with a base URL and then make calls to a /path.

##### Async DELETE By Path

```csharp
var engine=new RestEngine("http://example.com");
var taskResult = await engine.ProcessDeleteAsync("/api/users");
```

##### Sync DELETE By Path

```csharp
var engine=new RestEngine("http://example.com");
var result = engine.ProcessDeleteSync("/api/users");
```

#### Process PUT With everything setted up

This makes the Put call to the environment or context built with headers, baseUrl, Accept, body string, uri and query params.

##### Async DELETE by context

```csharp
var taskResult = await engine.ProcessDeleteAsync();
```

##### Sync DELETE by context

```csharp
var result = engine.ProcessDeleteSync();
```

## Contribute

This is the first package/library made by carjimfa. Doubts, support and more, ask me anything on twitter: [@carjimfa]("https://twitter.com/carjimfa") :)