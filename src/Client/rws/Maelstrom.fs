module Rws.Maelstrom

open Fable.Core
open Fable.Core.JS
open Fable.React
open Fable.React.Props


let view () =
    div [Id "MS"; Class "tabcontent"][
        br []
        div [Id "maelstromMessage"][]
        br []
        div [Id "maelstromForecast"][]
    ]