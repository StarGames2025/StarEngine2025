PROJECT_NAME = StarEngine2025
SRC_DIR = src
BIN_DIR = bin
OUTPUT = $(BIN_DIR)/$(PROJECT_NAME).exe

SRC_FILES = $(SRC_DIR)/Program.cs \
            $(SRC_DIR)/MainForm.cs \
            $(SRC_DIR)/EditorLogic.cs \
            $(SRC_DIR)/ProjectLogic.cs \
            $(SRC_DIR)/TemplateLogic.cs \
            $(SRC_DIR)/SettingsLogic.cs \
            $(SRC_DIR)/ProjectTreeLogic.cs \
            $(SRC_DIR)/CodeExecutionLogic.cs

all: compile run

compile:
	mcs -out:$(OUTPUT) -r:System.Windows.Forms -r:System.Drawing -r:Microsoft.CodeAnalysis.CSharp.Scripting -r:Microsoft.CodeAnalysis.Scripting $(SRC_FILES)

run: 
	mono $(OUTPUT)

clean:
	rm -f $(OUTPUT)

.PHONY: all compile run clean
