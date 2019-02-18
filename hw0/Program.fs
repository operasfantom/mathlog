open System.IO
open Microsoft.FSharp.Text.Lexing
open LogicTree

type op with
        member x.format() = match x with | Conj -> "&" | Disj -> "|" | Impl -> "->"

let ExpressionToTree text =
    let rec analyse acc tree = acc + match tree with
    | Var v -> v
    | Neg t -> (sprintf "(!%s)" (analyse acc t))
    | Binop(op, t1, t2) -> (sprintf "(%s,%s,%s)" (op.format()) (analyse acc t1) (analyse acc t2))

    let lexbuf = LexBuffer<_>.FromString text
    let t = LogicParser.parse LogicLexer.tokenize lexbuf

    let mutable result = ""
    analyse result t


[<EntryPoint>]
let main argv =
  use input = File.OpenText("input.txt")
  use output = File.CreateText("output.txt")
  let s = input.ReadLine()
  ExpressionToTree s |> output.WriteLine
  0 // return an integer exit code
