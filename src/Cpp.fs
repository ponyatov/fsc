/// formatting attributes
type A =
    | s of string
    | b of (string*bool)
    | i of (string*int)

/// generic Source code repr
type S =
    type S' =
        | bare of string
        | lst  of string list
    | tree of {
            attr: A  set
            pfx:  S' optional
            nest: S
            sfx:  S' optional
        }

sigil C"s" = Cpp.parser s

const tabsize = 4

let dump (src:S) (depth: int = 0): string = seq {
    match S with
    | S.bare s ->
        String.replicate (depth*tabsize) " " @ s
    | S.list l ->
        yield! List map l {s -> dump s depth}
    | S.tree attr pfx nest sfx ->
        yield! preDump pfx  (depth+0)
        yield! preDump nest (depth+1)
        yield! preDump sfx  (depth+0)
}

let write file src = file |> File.WriteAllLines (dump src 0)
