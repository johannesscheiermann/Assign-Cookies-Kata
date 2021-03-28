module AssignCookies.Domain.Test.Implementation.Test

open AssignCookies.Domain.Types
open AssignCookies.Domain.Implementation
open FsToolkit.ErrorHandling
open Xunit
open FsUnit

let private asGreedFactors values =
    result {
        let! list =
            values
            |> List.map GreedFactor.create
            |> List.sequenceResultM

        let! greedFactors = list |> GreedFactors.create
        return greedFactors
    }

let private asCookieSizes values =
    result {
        let! list =
            values
            |> List.map CookieSize.create
            |> List.sequenceResultM

        let! cookieSizes = list |> CookieSizes.create
        return cookieSizes
    }

let private assignCookies greedFactors cookieSizes =
    result {
        let! gf = greedFactors |> asGreedFactors
        let! cs = cookieSizes |> asCookieSizes

        return assignCookies gf cs
    }

let private valueOrFail result =
    match result with
    | Ok value -> value
    | Error error -> failwith error

[<Fact>]
let ``Meets the expected output of example 1`` () =
    assignCookies [ 1; 2; 3 ] [ 1; 1 ]
    |> valueOrFail
    |> should equal 1

[<Fact>]
let ``Meets the expected output of example 2`` () =
    assignCookies [ 1; 2 ] [ 1; 2; 3 ]
    |> valueOrFail
    |> should equal 2

[<Fact>]
let ``Meets the expected output of example 3`` () =
    assignCookies [ 4; 1; 3; 2; 1; 1; 2; 2 ] [ 1; 1; 1; 1 ]
    |> valueOrFail
    |> should equal 3

[<Fact>]
let ``Meets the expected output of example 4`` () =
    assignCookies [ 4; 1; 3; 2; 1; 1; 2; 2 ] [ 2; 2; 2; 2 ]
    |> valueOrFail
    |> should equal 4
