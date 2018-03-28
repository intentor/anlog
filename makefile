# Project settings.
PROJECT_ID=Anlog
MAIN_PROJECT_FOLDER=src/Anlog
MAIN_PROJECT_FILE=$(MAIN_PROJECT_FOLDER)/Anlog.csproj

# Build variables.
PROJECT_VERSION=$(shell cat $(MAIN_PROJECT_FILE) | grep -oP '(?<=<Version>).+(?=<\/Version>)')

build:
	dotnet build

test:
	dotnet test