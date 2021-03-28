module AssignCookies.Domain.Test.GreedFactors.Test

open FsUnit
open Xunit
open AssignCookies.Domain.Types
open FsToolkit.ErrorHandling

[<Fact>]
let ``Must not be empty`` () =
    List.empty<GreedFactor>
    |> GreedFactors.create
    |> should be (ofCase <@ Result<GreedFactors, string>.Error @>)

[<Theory>]
[<InlineData(1)>]
[<InlineData(2)>]
[<InlineData(29999)>]
[<InlineData(30000)>]
let ``Must contain between 1 and 30000 greed factors`` count =
    result {
        let! list =
            seq { 1 .. count }
            |> Seq.map GreedFactor.create
            |> Seq.toList
            |> List.sequenceResultM

        let! greedFactors = list |> GreedFactors.create
        return greedFactors
    }
    |> should be (ofCase <@ Result<GreedFactors, string>.Ok @>)

[<Fact>]
let ``Must not exceed 30000 greed factors`` () =
    result {
        let! list =
            seq { 1 .. 30001 }
            |> Seq.map GreedFactor.create
            |> Seq.toList
            |> List.sequenceResultM

        let! greedFactors = list |> GreedFactors.create
        return greedFactors
    }
    |> should be (ofCase <@ Result<GreedFactors, string>.Error @>)
