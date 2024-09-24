PROJECT_NAME = StarEngine2025
SRC_DIR = src
BIN_DIR = bin
OUTPUT = $(BIN_DIR)/$(PROJECT_NAME).exe

VERSION_FILE = version.txt
VERSION = $(shell cat $(VERSION_FILE) 2>/dev/null || echo "0.0.0")

SRC_FILES = $(SRC_DIR)/Program.cs \
            $(SRC_DIR)/MainForm.cs \
            $(SRC_DIR)/EditorLogic.cs \
            $(SRC_DIR)/ProjectLogic.cs \
            $(SRC_DIR)/TemplateLogic.cs \
            $(SRC_DIR)/SettingsLogic.cs \
            $(SRC_DIR)/ProjectTreeLogic.cs \
            $(SRC_DIR)/CodeExecutionLogic.cs

all: increment-version compile run

compile:
	mcs -out:$(OUTPUT) -r:System.Windows.Forms -r:System.Drawing $(SRC_FILES)

run: 
	mono $(OUTPUT)

increment-version:
	@echo "Current version: $(VERSION)"
	@echo "Incrementing PATCH version..."
	@MAJOR=$(word 1,$(subst ., ,$(VERSION))); \
	MINOR=$(word 2,$(subst ., ,$(VERSION))); \
	PATCH=$(word 3,$(subst ., ,$(VERSION))); \
	echo "Current MAJOR: $$MAJOR, MINOR: $$MINOR, PATCH: $$PATCH"; \
	PATCH=$$((PATCH + 1)); \
	echo "$$MAJOR.$$MINOR.$$PATCH" > $(VERSION_FILE); \
	echo "New version: $$MAJOR.$$MINOR.$$PATCH"

clean:
	rm -f $(OUTPUT)

.PHONY: all compile run clean increment-version


# Patch: make increment-version
# Minor: make level=MINOR increment-version
# Major: make level=MAJOR increment-version
