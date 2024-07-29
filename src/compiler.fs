﻿// https://thanos.codes/blog/using-fslexyacc-the-fsharp-lexer-and-parser/
// https://en.wikibooks.org/wiki/F_Sharp_Programming/Lexing_and_Parsing

module compiler

open AST

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

    /// `( n1 n2 -- n1 << n2 )` bit shift left
    | Shl = 0x29uy
    /// `( n1 n2 -- n1 >> n2 )` bit shift right
    | Shr = 0x2Auy

type ByteCode =
    /// single command
    | Cmd0 of Cmd0
    /// single-path block
    | Block of ByteCode array

let dumblock = Block([| Cmd0 Cmd0.Nop; Cmd0 Cmd0.Halt |])

let test = //
    assert ($"{dumblock}" = "Block [|Cmd0 Nop; Cmd0 Halt|]")

[<EntryPoint>]
let main (argv: string array) =
    for arg in argv do
        printfn "%s" arg

    test
    AST.test
