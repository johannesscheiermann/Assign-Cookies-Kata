module AssignCookies.Domain.Test.CookieSizes.Test

open FsUnit
open Xunit
open AssignCookies.Domain.Types
open FsToolkit.ErrorHandling

[<Fact>]
let ``May be empty`` () =
    List.empty<CookieSize>
    |> CookieSizes.create
    |> should be (ofCase <@ Result<CookieSizes, string>.Ok @>)

[<Theory>]
[<InlineData(0)>]
[<InlineData(1)>]
[<InlineData(2)>]
[<InlineData(29999)>]
[<InlineData(30000)>]
let ``May contain up to 30000 cookie sizes`` count =
    result {
        let! list =
            seq { 1 .. count }
            |> Seq.map CookieSize.create
            |> Seq.toList
            |> List.sequenceResultM

        let! cookieSizes = list |> CookieSizes.create
        return cookieSizes
    }
    |> should be (ofCase <@ Result<CookieSizes, string>.Ok @>)

[<Fact>]
let ``Must not exceed 30000 cookie sizes`` () =
    result {
        let! list =
            seq { 1 .. 30001 }
            |> Seq.map CookieSize.create
            |> Seq.toList
            |> List.sequenceResultM

        let! cookieSizes = list |> CookieSizes.create
        return cookieSizes
    }
    |> should be (ofCase <@ Result<CookieSizes, string>.Error @>)
