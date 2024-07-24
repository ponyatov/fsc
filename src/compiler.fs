// https://thanos.codes/blog/using-fslexyacc-the-fsharp-lexer-and-parser/
// https://en.wikibooks.org/wiki/F_Sharp_Programming/Lexing_and_Parsing

module compiler

open AST

type VMCmd =
    // control
    /// `( -- )` do nothing
    | Nop = 0x00uy
    /// `( -- )` stop system
    | Halt = 0xFFuy
    // stack
    /// `( n -- n n )`
    | Dup = 0x10uy
    /// `( n1 n2 -- n1 )`
    | Drop = 0x11uy
    /// `( n1 n2 -- n2 n1 )`
    | Swap = 0x12uy
    /// `( n1 n2 -- n1 n2 n1 )`
    | Over = 0x13uy
    // math
    /// `( n1 n2 -- n1+n2 )` sum
    | Add = 0x20uy
    /// `( n1 n2 -- n1-n2 )` subtract
    | Sub = 0x21uy
    /// `( n1 n2 -- n1*n2 )` multiply
    | Mul = 0x22uy
    /// `( n1 n2 -- n1/n2 )` integer division
    | Div = 0x23uy

type ByteCode =
    /// single command
    | Cmd of VMCmd
    /// single-path block
    | Block of ByteCode array

let dumblock = Block([| Cmd VMCmd.Nop; Cmd VMCmd.Halt |])

let test = //
    assert ($"{dumblock}" = "Block [|Cmd Nop; Cmd Halt|]")

[<EntryPoint>]
let main (argv: string array) =
    for arg in argv do
        printfn "%s" arg

    test
    AST.test
