# var
MODULE = $(notdir $(CURDIR))

# dirs
CWD = $(CURDIR)

# tool
CURL     = curl -L -o
CF       = clang-format -style=file -i
DOTNET   = dotnet
FANTOMAS = $(HOME)/.dotnet/tools/fantomas

# src
F = src/AST.fs src/compiler.fs
C = $(wildcard src/*.c*)
H = $(wildcard inc/*.h*)
M = lib/$(MODULE).ini $(wildcard lib/*.m)

# all
.PHONY: all
all: $(M)
	$(DOTNET) run $^

# format
.PHONY: format
format: tmp/format_fs
tmp/format_fs: $(F)
	$(FANTOMAS) $? && touch $@

# install
.PHONY: install update ref gz
install: ref gz
	$(MAKE) update
update:
	sudo apt update
	sudo apt install -uy `cat apt.txt`
ref: $(MODULE).fsproj $(FANTOMAS)
gz:

$(MODULE).fsproj:
	$(DOTNET) new console -lang "F#"
$(FANTOMAS):
	$(DOTNET) tool install -g fantomas
