﻿namespace HtmlParser.Tags.Tests

open NUnit.Framework
open FParsec
open TestUtilities
open HtmlParser.Types
open Attributes
open Tags

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.ItShouldParseBodyTagWithClassAttribute () =
        let body = "<body class=\"hs-home\"></body>"
        let actual =
            match run pbody body with
            | Success(result, _, _)   -> result 
            | Failure(_, _, _) -> HtmlBody { Attributes = []; Tags = [] }

        let expected = 
            HtmlBody { 
                Attributes = [
                    Class ["hs-home"]
                    ];
                Tags = [] 
            }

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.ItShouldParseBodyTagWithClassAndIdAttributes () =
        let body = "<body class=\"hs-home\" id=\"hs_cos_wrapper_module_154623948344039\"></body>"
        let actual =
            match run pbody body with
            | Success(result, _, _)   -> result 
            | Failure(_, _, _) -> HtmlBody { Attributes = []; Tags = [] }

        let expected = 
            HtmlBody { 
                Attributes = [
                    Class ["hs-home"] 
                    Id "hs_cos_wrapper_module_154623948344039"
                    ]; 
                Tags = [] 
            }

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.ItShouldParseBodyTagWithClassAndIdAndTitleAttributes () =
        let body = "<body title=\"Download the Case Study\" class=\"hs-home   hs-content-id-10345721341 hs-site-page page \" id=\"hs_cos_wrapper_module_154623948344039\"></body>"
        let actual =
            match run pbody body with
            | Success(result, _, _)   -> result 
            | Failure(_, _, _) -> HtmlBody { Attributes = []; Tags = [] }

        let expected = 
            HtmlBody { 
                Attributes = [
                    Title "Download the Case Study"
                    Class [
                        "hs-home"
                        "hs-content-id-10345721341"
                        "hs-site-page"
                        "page"
                        ]
                    Id "hs_cos_wrapper_module_154623948344039"
                ]; 
                Tags = [] 
            }

        Assert.AreEqual(expected, actual)

    [<Test>]
    member this.ItShouldParseDivTagWithClassAttribute() =
        let div = "<div class=\"row-fluid \"></div>"
        let actual =
            match run pdiv div with
            | Success(result, _, _)   -> result 
            | Failure(_, _, _) -> HtmlDiv { Attributes = []; Tags = [] }

        let expected = 
            HtmlDiv { 
                Attributes = [ Class ["row-fluid"] ]; 
                Tags = [] 
            }

        Assert.AreEqual(expected, actual)

    [<Test>] 
    member this.ItShouldParseNestedDivTags() =
        let divs = "<div><div></div></div>"
        let actual =
            match run pdiv divs with
            | Success(result, _, _)   -> result 
            | Failure(_, _, _) -> HtmlDiv { Attributes = []; Tags = [] }

        let expected =
            HtmlDiv {
                Attributes = []
                Tags = [
                    HtmlDiv {
                        Attributes = []
                        Tags = []
                        }
                    ]
                }

        Assert.AreEqual(expected, actual)

    [<Test>] 
    member this.ItShouldParseDivTagInsideBodyTag() =
        let tags = "<body><div></div></body>"
        let actual =
            match run pbody tags with
            | Success(result, _, _)   -> result 
            | Failure(_, _, _) -> HtmlBody { Attributes = []; Tags = [] }

        let expected =
            HtmlBody {
                Attributes = []
                Tags = [
                    HtmlDiv {
                        Attributes = []
                        Tags = []
                        }
                    ]
                }

        Assert.AreEqual(expected, actual)

