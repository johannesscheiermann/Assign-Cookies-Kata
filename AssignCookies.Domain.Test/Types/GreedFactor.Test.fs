module AssignCookies.Domain.Test.GreedFactor.Test

open FsUnit
open Xunit
open AssignCookies.Domain.Types

[<Theory>]
[<InlineData(0)>]
[<InlineData(-1)>]
let ``A greed factor must not be less than 1`` greedFactor =
    greedFactor
    |> GreedFactor.create
    |> should be (ofCase <@ Result<GreedFactor, string>.Error @>)

[<Theory>]
[<InlineData(1)>]
[<InlineData(2)>]
let ``A greed factor must be greater than 0`` greedFactor =
    greedFactor
    |> GreedFactor.create
    |> should be (ofCase <@ Result<GreedFactor, string>.Ok @>)
