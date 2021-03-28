module AssignCookies.Domain.Implementation

open AssignCookies.Domain.Types

type AssignCookies = GreedFactors -> CookieSizes -> int

let rec private assignForSorted (greedFactors, cookieSizes) =
    match greedFactors with
    | [] -> []
    | greed :: remainingGreeds ->
        match cookieSizes with
        | [] -> []
        | size :: remainingSizes when size >= greed -> size :: assignForSorted (remainingGreeds, remainingSizes)
        | _ :: remainingSizes -> assignForSorted (remainingGreeds, remainingSizes)

let assignCookies: AssignCookies =
    fun greedFactors cookieSizes ->
        assignForSorted
            (greedFactors
             |> GreedFactors.value
             |> List.map GreedFactor.value
             |> List.sort,
             cookieSizes
             |> CookieSizes.value
             |> List.map CookieSize.value
             |> List.sort)
        |> List.length
