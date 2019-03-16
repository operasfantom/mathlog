module Axiom

open LogicTree

let axioms = [
    (*1*) "A->B->A",
    (*2*) "(A->B)->(A->B->C)->(A->C)",
    (*3*) "A->B->A&B",
    (*4*) "A&B->A",
    (*5*) "A&B->B",
    (*6*) "A->B|A",
    (*7*) "B->A|B",
    (*8*) "(A->C)->(B->C)->(A|B->C)",
    (*9*) "(A->B)->(A->!B)->!A",
    (*10*) "!!A->A"
 ]

let verify_expression e =
    match e with
        | Binop(Impl, a1, Binop(Impl, b1, a2))
            when (a1 = a2)
             -> Some(1)
        | Binop(Impl, Binop(Impl, a1, b1), Binop(Impl, Binop(Impl, a2, Binop(Impl, b2, c1)), Binop(Impl, a3, c2)))
            when (a1 = a2 && a2 = a3 && b1 = b2 && c1 = c2)
             -> Some(2)
        | Binop(Impl, a1, Binop(Impl, b1, Binop(And, a2, b2)))
            when (a1 = a2 && b1 = b2)
             -> Some(3)
        | Binop(Impl, Binop(And, a1, b1), a2)
            when (a1 = a2)
             -> Some(4)
        | Binop(Impl, Binop(And, a1, b1), b2)
            when (b1 = b2)
             -> Some(5)
        | Binop(Impl, a1, Binop(Or, a2, b1))
            when (a1 = a2)
             -> Some(6)
        | Binop(Impl, b1, Binop(Or, a1, b2))
            when (b1 = b2)
             -> Some(7)
        | Binop(Impl, Binop(Impl, a1, c1), Binop(Impl, Binop(Impl, b1, c2), Binop(Impl, Binop(Or, a2, b2), c3)))
            when (a1 = a2 && b1 = b2 && c1 = c2 && c2 = c3)
             -> Some(8)
        | Binop(Impl, Binop(Impl, a1, b1), Binop(Impl, Binop(Impl, a2, Neg(b2)), Neg(a3)))
            when (a1 = a2 && a2 = a3 && b1 = b2)
             -> Some(9)
        | Binop(Impl, Neg(Neg(a1)), a2)
            when (a1 = a2)
             -> Some(10)
        | _
             -> None
