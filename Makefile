build:
	dotnet clean
	dotnet build

release:
	dotnet clean
	dotnet build --configuration Release
	nuget pack Dadata.nuspec

test:
	dotnet test --arch=arm64 --filter=FullyQualifiedName~$(filter)

test-all:
	dotnet test --arch=arm64
