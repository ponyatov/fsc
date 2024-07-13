module lexer

# 1 "lib/lexer.fsl"
 
open FSharp.Text.Lexing
open parser

let lexeme lexbuf = LexBuffer<_>.LexemeString lexbuf

# 10 "lib/lexer.fs"
let trans : uint16[] array = 
    [| 
    (* State 0 *)
     [| 1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;1us;65535us;|];
    (* State 1 *)
     [| 65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;65535us;|];
    |] 
let actions : uint16[] = [|65535us;0us;|]
let _fslex_tables = FSharp.Text.Lexing.UnicodeTables.Create(trans,actions)
let rec _fslex_dummy () = _fslex_dummy() 
// Rule tokenize
and tokenize  lexbuf =
  match _fslex_tables.Interpret(0,lexbuf) with
  | 0 -> ( 
# 9 "lib/lexer.fsl"
                      lexeme lexbuf |> sprintf "Parsing error: %s" |> failwith 
# 27 "lib/lexer.fs"
          )
  | _ -> failwith "tokenize"

# 3000000 "lib/lexer.fs"
