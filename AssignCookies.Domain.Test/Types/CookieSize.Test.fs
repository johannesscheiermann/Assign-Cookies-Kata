module AssignCookies.Domain.Test.CookieSize.Test

open FsUnit
open Xunit
open AssignCookies.Domain.Types

[<Theory>]
[<InlineData(0)>]
[<InlineData(-1)>]
let ``A cookie size must not be less than 1`` cookieSize =
    cookieSize
    |> CookieSize.create
    |> should be (ofCase <@ Result<CookieSize, string>.Error @>)

[<Theory>]
[<InlineData(1)>]
[<InlineData(2)>]
let ``A cookie size must be greater than 0`` cookieSize =
    cookieSize
    |> CookieSize.create
    |> should be (ofCase <@ Result<CookieSize, string>.Ok @>)
