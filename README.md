DataUmpa
==========

A library for comparing and syncrhonizing dictionaries.

## Build and publish

Compile the library with:
```
dotnet build
```

### NuGet package
Create the package with:

```
dotnet pack
```

Then publish it with:

```
nuget push src\dataumpa\bin\Debug\DataUmpa.1.0.0.nupkg -k {api-key} -s https://api.nuget.org/v3/index.json
```
