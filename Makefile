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

VERSION := $(shell sed -n 's:.*<version>\(.*\)</version>.*:\1:p' Dadata.nuspec)

publish:
	@test -n "$(NUGET_TOKEN)" || (echo "NUGET_TOKEN is not set" && exit 1)
	@echo "Publishing Dadata.$(VERSION).nupkg"
	@dotnet nuget push Dadata.$(VERSION).nupkg --source https://api.nuget.org/v3/index.json --api-key "$(NUGET_TOKEN)"
