# Project settings.
PROJECT_ID=Anlog
MAIN_PROJECT_FOLDER=src/Anlog
MAIN_PROJECT_FILE=$(MAIN_PROJECT_FOLDER)/Anlog.csproj

# Build variables.
PROJECT_VERSION=$(shell cat $(MAIN_PROJECT_FILE) | grep -oP '(?<=<Version>).+(?=<\/Version>)')

build:
	dotnet build

test:
	find tests/**/*.csproj -exec dotnet test '{}' \;
	
local:
	dotnet msbuild "$(MAIN_PROJECT_FOLDER)" -t:PublishLocal
	
publish:
	dotnet msbuild "$(MAIN_PROJECT_FOLDER)" -t:PublishNuget