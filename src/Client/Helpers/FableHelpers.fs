module Helpers.FableHelpers
open Fable
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

let toGlobal (name:string) value : unit =
    Browser.Dom.window?(name) <- value