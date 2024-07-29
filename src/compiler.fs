// https://thanos.codes/blog/using-fslexyacc-the-fsharp-lexer-and-parser/
// https://en.wikibooks.org/wiki/F_Sharp_Programming/Lexing_and_Parsing

module compiler

open AST
open System.IO

/// command with argument
type Cmd1 =
    // control
    /// `( -- )` unconditional jump
    | Jmp = 0x01uy
    /// `( zero -- )` jump if zero
    | qJmp = 0x02uy
    /// `R:( -- addr )` nested call
    | Call = 0x03uy
    /// `( -- n )` literal
    | Lit = 0x05uy

/// no-arg commands
type Cmd0 =
    // control
    /// `( -- )` do nothing
    | Nop = 0x00uy
    /// `( -- )` stop system
    | Halt = 0xFFuy
    /// `R:( addr -- )` return from nested call
    | Ret = 0x04uy
    // stack
    /// `( n -- n n )`
    | Dup = 0x10uy
    /// `( n1 n2 -- n1 )`
    | Drop = 0x11uy
    /// `( n1 n2 -- n2 n1 )`
    | Swap = 0x12uy
    /// `( n1 n2 -- n1 n2 n1 )`
    | Over = 0x13uy
    /// `( n1 n2 n3 -- n2 n3 n1)`
    | Rot = 0x14uy
    /// `( n1 n2 n3 -- n3 n1 n2 )`
    | mRot = 0x15uy
    /// `( ... i -- ... ni)
    | Pick = 0x16uy
    /// `( ... -- ... n ) stack depth
    | Depth = 0x17uy
    // math
    /// `( n1 n2 -- n1+n2 )` sum
    | Add = 0x20uy
    /// `( n1 n2 -- n1-n2 )` subtract
    | Sub = 0x21uy
    /// `( n1 n2 -- n1*n2 )` multiply
    | Mul = 0x22uy
    /// `( n1 n2 -- n1/n2 )` integer division
    | Div = 0x23uy
    /// `( n1 n2 -- n1 % n2 )`
    | Mod = 0x24uy
    /// bit operators
    /// `( n -- !n)` invert bits
    | Not = 0x25uy
    /// `( n1 n2 -- n1|n2 )`
    | Or = 0x26uy
    /// `( n1 n2 -- n1&n2 )`
    | And = 0x27uy
    /// `( n1 n2 -- n1^n2 )`
    | Xor = 0x28uy
    /// `( n1 n2 -- n1<<n2 )` bit shift left
    | Shl = 0x29uy
    /// `( n1 n2 -- n1>>n2 )` bit shift right
    | Shr = 0x2Auy

type ByteCode =
    /// single command
    | Cmd0 of Cmd0
    /// single argument
    | Cmd1 of Cmd1 * int
    /// single-path block
    | Block of ByteCode array

let dumblock = Block([| Cmd0 Cmd0.Nop; Cmd0 Cmd0.Halt |])

let test = //
    assert ($"{dumblock}" = "Block [|Cmd0 Nop; Cmd0 Halt|]")

[<Literal>]
/// memory size, bytes
let Msz = 0x10000

[<Literal>]
/// return stack size, ucells
let Rsz = 0x100

[<Literal>]
/// data stack size, cells
let Dsz = 0x10

let vmconfig =
    [ //
      ""
      $"#define Msz 0x%x{Msz}"
      $"#define Rsz 0x%x{Rsz}"
      $"#define Dsz 0x%x{Dsz}" ]

let vmtypes =
    [ //
      ""
      "#define byte   uint8_t"
      "#define cell   int16_t"
      "#define ucell uint16_t" ]

let memh =
    [ //
      ""
      "extern  byte M[Msz];"
      "extern ucell Cp;"
      "extern ucell Ip;"
      "extern ucell R[Rsz];"
      "extern ucell Rp;"
      "extern  cell D[Dsz];"
      "extern ucell Dp;" ]

let memc =
    [ //
      ""
      "uint8_t M[Msz];"
      "uint16_t Cp = 0;"
      "uint16_t Ip = 0;" ]

let vmh =
    [ //
      ""
      "extern void vm();"
      ""
      "enum Cmd { nop = 0x00, halt = 0xFF };"
      ""
      "extern void bc(uint8_t b);" ]

let vmc =
    [ //
      ""
      "void vm() {"
      "\twhile (true) {"
      "\t\tassert(Ip < Cp); uint8_t op = M[Ip++];"
      "\t\tfprintf(stderr, \"%.4X: %.2X \", Ip - 1, op);"
      "\t\tswitch (op) {"
      "\t\t\tdefault:"
      "\t\t\t\tfprintf(stderr, \"???\\n\", op); abort();"
      "\t\t}"
      "\t}"
      "}"
      ""
      "void bc(uint8_t b) {"
      "\tassert(Cp < Msz); M[Cp++] = b;"
      "}"

      ]

let mainh =
    [ //
      ""
      "extern int main(int argc, char *argv[]);"
      "extern void arg(int argc, char *argv);" ]

let hpp = //
    File.WriteAllLines(
        @"inc/fsc.hpp",
        [ //
          "#pragma once"
          ""
          "#include <stdio.h>"
          "#include <stdlib.h>"
          "#include <assert.h>"
          ""
          "#include <iostream>"
          "#include <map>"
          "#include <vector>" ]
        @ mainh
        @ vmconfig
        @ vmtypes
        @ memh
        @ vmh
    )
    |> ignore


let mainc = //
    [ //
      ""
      "int main(int argc, char *argv[]) {  //"
      "\targ(0, argv[0]);"
      "\tbc(nop); bc(halt);"
      "\tvm();"
      "}" ]

let argc = //
    [ ""
      "void arg(int argc, char *argv) {  //"
      "\tfprintf(stderr, \"argv[%i] = <%s>\\n\", argc, argv);"
      "}" ]

let cpp = //
    File.WriteAllLines(
        @"src/fsc.cpp",
        [ //
          "#include \"fsc.hpp\"" ]
        @ mainc
        @ argc
        @ memc
        @ vmc

    )
    |> ignore

let cgen = //
    hpp
    cpp

[<EntryPoint>]
let main (argv: string array) =
    for arg in argv do
        printfn "%s" arg

    test
    AST.test
