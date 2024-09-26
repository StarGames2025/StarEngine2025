PROJECT_NAME = StarEngine2025
SRC_DIR = src
BIN_DIR = bin
OUTPUT_WINDOWS = $(BIN_DIR)/$(PROJECT_NAME).exe
OUTPUT_LINUX = $(BIN_DIR)/$(PROJECT_NAME)

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

win: compile-windows run-windows
unix: compile-linux run-linux
dev: compile-windows run-windows increment-version

compile-linux:
	mcs -out:$(OUTPUT_LINUX) -r:System.Windows.Forms -r:System.Drawing $(SRC_FILES)
	chmod +x $(OUTPUT_LINUX)

compile-windows:
	mcs -out:$(OUTPUT_WINDOWS) -r:System.Windows.Forms -r:System.Drawing $(SRC_FILES)

run-linux:
	./$(OUTPUT_LINUX)

run-windows:
	mono $(OUTPUT_WINDOWS)

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
	rm -f $(OUTPUT_WINDOWS) $(OUTPUT_LINUX)

.PHONY: all compile-linux compile-windows run-linux run-windows clean increment-version

# Patch: make increment-version
# Minor: make level=MINOR increment-version
# Major: make level=MAJOR increment-version
