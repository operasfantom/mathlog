module Processing

open Axiom
open LogicTree

open System.Collections.Generic

type Proof = | Axiom of int | Assumption of int | ModusPones of Proof * Proof

let mutable assumptions_set = new Dictionary<LogicTree.tree, int>()
let mutable axioms_set = new Dictionary<LogicTree.tree, int>()
let mutable modus_pones_set = new Dictionary<LogicTree.tree, Proof>()
let mutable have_met = new Dictionary<LogicTree.tree, Proof>()

let get_or_none (dict: Dictionary<_, _>, key) =
    let (exists, value) = dict.TryGetValue(key)
    match exists with
        | true -> Some(value)
        | false -> None

let proof_by_expression expression =
    get_or_none (have_met, expression)

let preprocess header =
    match header with
        | (assumptions, result) ->
            assumptions |> List.iter (fun a -> assumptions_set.Add(a, 0)) //todo


let process_expression expression index: Proof option =
    //axiom
    let axiom_id = verify_expression expression
    match axiom_id with
        | Some(_) ->
            axioms_set.Add(expression, index)
            Some(Axiom(index))
        | None ->

    //assumption
    if (assumptions_set.ContainsKey(expression)) then
        Some(Assumption(index))
    else

    //modus pones
    let (exists, value) = modus_pones_set.TryGetValue(expression)
    match exists with
        | true ->
            match value with
            | ModusPones(a, b) as m -> Some(m)
            | _ -> None //todo ???
        | false -> None


let post_process expression proof index =
    match proof with
        | Some(p) ->
            have_met.TryAdd(expression, p) |> ignore
            match expression with
                | Binop(Impl, a, b) ->
                    let proof_a = proof_by_expression a
                    match proof_a with
                        | Some(q) -> modus_pones_set.TryAdd(b, ModusPones(p, q)) |> ignore
                        | None -> ignore(0)                                        
                | _ -> ignore(0)            
        | None -> ignore(0)

let expand_proof (proofs: seq<_ option>) =
    if (Seq.contains None proofs)
    then
        "Proof is incorrect"
    else
        "Win"

