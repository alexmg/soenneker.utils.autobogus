﻿[![](https://img.shields.io/nuget/v/soenneker.utils.autobogus.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.autobogus/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.autobogus/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.autobogus/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.autobogus.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.autobogus/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.AutoBogus
### The .NET Autogenerator 

This project is an automatic creator and populator for the fake data generator [Bogus](https://github.com/bchavez/Bogus).

It's a replacement for the abandoned [AutoBogus](https://github.com/nickdodd79/AutoBogus) library.

The goals are to be *fast*, and support the latest types in .NET.

.NET 6+ is supported.

## Installation

```
dotnet add package Soenneker.Utils.AutoBogus
```

## Usage

- Create an AutoFaker instance:
```csharp 
var autoFaker = new AutoFaker();
```

- Call `Generate<>()` on any type you want:

```csharp
var someRandomWord = autoFaker.Generate<string>();
var dictionary = autoFaker.Generate<Dictionary<int, string>>();
var order = autoFaker.Generate<Order>();
```

- Set a faker, configuration, rulesets, etc:

```csharp
autoFaker.Config.Faker = new Faker("de");
autoFaker.Config.RecursiveDepth = 3;
...
```

- `AutoFakerOverride` can be used for type customization:

```csharp
public class OrderOverride : AutoFakerOverride<Order>
{
    public override void Generate(AutoFakerOverrideContext context)
    {
        var target = (context.Instance as Order)!;
        target.Id = 123;
        
        // Faker is available
        target.Name = context.Faker.Random.Word();

        // AutoFaker is also available
        target.Customer = context.AutoFaker.Generate<Customer>();
     }
}
```

- Configuring `AutoFakerOverride` on the `AutoFaker`:

```csharp
autoFaker.Configure(builder =>
{
    builder.WithOverride(new OrderOverride());
});
```

## Tips
  - ⚠️ Instantiating a Bogus `Faker` takes a substantial amount of time (almost 1ms). An instance of `AutoFaker` will create one `Faker`. Thus, it's recommended that a single instance be used if possible.
- `AutoFaker.GenerateStatic<>()` is available, but should be avoided (as it creates a new `AutoFaker`/`Faker` on each call).

## Notes
- This is a work in progress. Contribution is welcomed.

## Benchmarks

### Soenneker.Utils.AutoBogus

| Method           | Mean         | Error      | StdDev     |
|----------------- |-------------:|-----------:|-----------:|
| Generate_int     |     73.86 ns |   0.843 ns |   0.747 ns |
| Generate_string  |    227.31 ns |   2.601 ns |   2.433 ns |
| Generate_complex | 17,681.91 ns | 128.889 ns | 120.563 ns |

### AutoBogus

| Method           | Mean      | Error    | StdDev   |
|----------------- |----------:|---------:|---------:|
| Generate_int     |   1.17 ms | 0.033 ms | 0.026 ms |
| Generate_complex |  10.91 ms | 0.181 ms | 0.236 ms |

### Bogus

| Method       | Mean          | Error         | StdDev       |
|------------- |--------------:|--------------:|-------------:|
| Bogus_int    |      19.58 ns |      0.150 ns |     0.133 ns |
| Bogus_string |     172.25 ns |      2.510 ns |     2.347 ns |
| Bogus_ctor   | 717,799.56 ns | 10,086.875 ns | 9,435.269 ns |