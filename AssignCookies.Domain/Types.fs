module AssignCookies.Domain.Types

type GreedFactor = private GreedFactor of int
type CookieSize = private CookieSize of int

type GreedFactors = private GreedFactors of GreedFactor list
type CookieSizes = private CookieSizes of CookieSize list

module GreedFactor =
    let value (GreedFactor factor) = factor

    // Note: The constraint "g[i], s[j] <= 2^(31) - 1 is implicitly ensured by the data type int"
    let create factor =
        if factor >= 1 then GreedFactor factor |> Ok else Error "a greed factor must be >= 1"

module CookieSize =
    let value (CookieSize size) = size

    // Note: The constraint "g[i], s[j] <= 2^(31) - 1 is implicitly ensured by the data type int"
    let create size =
        if size >= 1 then CookieSize size |> Ok else Error "a cookie size must be >= 1"

module GreedFactors =
    let value (GreedFactors factors) = factors

    let create (factors: GreedFactor list) =
        if factors.Length >= 1 && factors.Length <= 30000
        then GreedFactors factors |> Ok
        else Error "the count of greed factors must be >= 1 and <= 30000"

module CookieSizes =
    let value (CookieSizes sizes) = sizes

    let create (sizes: CookieSize list) =
        if sizes.Length <= 30000
        then CookieSizes sizes |> Ok
        else Error "the count of cookie sizes must be <= 30000"
